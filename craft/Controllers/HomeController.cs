namespace craft.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Services;

    public class HomeController : Controller
    {
        private readonly IPostCodeService _postCodeService;
        private readonly IDistanceCalculatorService _distanceCalculatorService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPostCodeService postCodeService, IDistanceCalculatorService distanceCalculatorService)
        {
            _logger = logger;
            _postCodeService = postCodeService;
            _distanceCalculatorService = distanceCalculatorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PostCode(string searchString)
        {
            double heathrowAirportLatitude = 51.470020;
            double heathrowAirportLongitude = -0.454295;

            PostCodeResult result = new PostCodeResult();
            if (!String.IsNullOrEmpty(searchString))
            {
                var postCodeResponse = await _postCodeService.GetPostCodes(searchString);
                var sourceCoordLat = postCodeResponse.Value.Result.Latitude;
                var sourceCoordLong = postCodeResponse.Value.Result.Longitude;

                result.PostCode = postCodeResponse.Value;
                result.Distance = _distanceCalculatorService.GetDistance(
                    sourceCoordLat,
                    sourceCoordLong,
                    heathrowAirportLatitude,
                    heathrowAirportLongitude).Value;

                var record = new PostCodeRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    Postcode = result.PostCode.Result.Postcode,
                    Region =  result.PostCode.Result.Region,
                    Country = result.PostCode.Result.Country,
                    AdminWard = result.PostCode.Result.AdminWard,
                    Ccg = result.PostCode.Result.Ccg,
                    Nuts = result.PostCode.Result.Nuts,
                    Km = result.Distance.Km,
                    Miles = result.Distance.Miles,
                    ProcessedOn = DateTime.UtcNow
                };

                var savedRecord = await _postCodeService.CreatePostCodeRecordAsync(record);
                var last3Records = await _postCodeService.GetPostCodeRecords();

                result.Last3Records = last3Records.ToList();
            }

            ViewBag.Message = result;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

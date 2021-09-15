namespace craft.Tests.Services
{
    using System.Threading.Tasks;
    using craft.Services;
    using NUnit.Framework;
    using Shouldly;

    public class DistanceCalculatorServiceTests
    {
        [TestCase(53.478612, 6.250578, 50.752342, 5.916981, 304)]
        [TestCase(-23.939607, 113.585605, -28.293166, 153.718989, 4021)]
        public async Task ShouldReturnDistanceIfGetDistanceSucceeds(double slat, double slong, double dlat, double dlong, int res)
        {
            var result = GetSut().GetDistance(slat,slong,dlat,dlong);

            result.IsSuccess.ShouldBeTrue();
            int aproxValue = (int)result.Value.Km;
            aproxValue.ShouldBe(res);
        }

        private DistanceCalculatorService GetSut() => new DistanceCalculatorService();
    }
}

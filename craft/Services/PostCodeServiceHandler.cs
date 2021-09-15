namespace craft.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class PostCodeServiceHandler : IPostCodeServiceHandler
    {
        private const string Endpoint = "postcodes";
        private readonly HttpClient _httpClient;

        private readonly DefaultContractResolver _contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };

        public PostCodeServiceHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<PostCodeResponse>> GetPostCodes(string postCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Endpoint}/{postCode}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var responsePostCode = JsonConvert.DeserializeObject<PostCodeResponse>(responseBody, new JsonSerializerSettings
                    {
                        ContractResolver = _contractResolver,
                        Formatting = Formatting.Indented
                    }
                );

                return response.StatusCode == HttpStatusCode.OK
                    ? Result.Ok(responsePostCode)
                    : Result.Fail<PostCodeResponse>($"Error: {nameof(response.StatusCode)} - {response.StatusCode}.");
            }
            catch (Exception ex)
            {
                return Result.Fail<PostCodeResponse>(ex.Message);
            }
        }
    }
}

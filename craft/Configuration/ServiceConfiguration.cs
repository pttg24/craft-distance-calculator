namespace craft.Configuration
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public ServiceConfiguration()
        {
            PostCodesApi = new ApiConfiguration();
        }

        public IApiConfiguration PostCodesApi { get; set; }
    }
}
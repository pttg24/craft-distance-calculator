namespace craft
{
    using System;
    using System.Net.Http;
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Microsoft.Extensions.Options;

    public static class IoC
    {
        public static void RegisterPostCodesServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IServiceConfiguration serviceConfiguration)
        {

            services.AddWithHttpClient<IPostCodeServiceHandler, PostCodeServiceHandler>(serviceConfiguration.PostCodesApi);
            services.AddSingleton<IPostCodeService, PostCodeService>();
            services.AddSingleton<IDistanceCalculatorService, DistanceCalculatorService>();
        }

        public static void RegisterDatabaseServices(this IServiceCollection services, IConfiguration configs)
        {
            services.Configure<DatabaseConfiguration>(
                configs.GetSection(nameof(DatabaseConfiguration)));

            services.AddSingleton<IDatabaseConfiguration>(sp =>
                sp.GetRequiredService<IOptions<DatabaseConfiguration>>().Value);
        }

        public static IServiceConfiguration RegisterServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceConfiguration = new ServiceConfiguration();
            configuration.GetSection(nameof(ServiceConfiguration)).Bind(serviceConfiguration);
            services.AddSingleton((IServiceConfiguration)serviceConfiguration);
            return serviceConfiguration;
        }

        private static void AddWithHttpClient<TInt, TImpl>(this IServiceCollection services, IApiConfiguration apiConfiguration) where TImpl : class, TInt where TInt : class
            => services.AddHttpClient<TInt, TImpl>(ConfigureClient(apiConfiguration));

        private static Action<HttpClient> ConfigureClient(IApiConfiguration apiConfiguration) => client =>
        {
            client.BaseAddress = new Uri($"{apiConfiguration.BaseAddress}{apiConfiguration.BasePath}");
            client.DefaultRequestHeaders.Add(
                "Accept",
                "application/json");
        };
    }
}

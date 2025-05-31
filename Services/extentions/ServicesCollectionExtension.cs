using DatabaseFirstAproach.Services.abstractions;

namespace DatabaseFirstAproach.Services.extentions;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IService, Service>();
        return services;
    }
}
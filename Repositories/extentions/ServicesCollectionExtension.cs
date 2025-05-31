using DatabaseFirstAproach.Repositories.abstractions;

namespace DatabaseFirstAproach.Repositories.extentions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ItripRepository, tripRepository>();

        return services;
    }
}
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SportsExplorer.Providers.Football;

namespace SportsExplorer.Providers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProvidersDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        string apiUrl = configuration["FootballApi:Url"];

        services
            .AddRefitClient<IFootballApi>(
                new RefitSettings(
                    new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)))
                {
                    UrlParameterKeyFormatter = new CamelCaseUrlParameterKeyFormatter()
                })
            .ConfigureHttpClient((provider, client) =>
            {
                client.BaseAddress = new Uri(apiUrl);
            });

        services.AddScoped<IFootballProvider, FootballProvider>();

        return services;
    }
}

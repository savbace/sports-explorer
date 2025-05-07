using System;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SportsExplorer.Providers.Football;

namespace SportsExplorer.Providers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProvidersDependencies(this IServiceCollection services)
    {
        services
            .AddRefitClient<IFootballApi>(
                new RefitSettings(
                    new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)))
                {
                    UrlParameterKeyFormatter = new CamelCaseUrlParameterKeyFormatter()
                })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://footballapi.pulselive.com"));

        services.AddScoped<IFootballProvider, FootballProvider>();

        return services;
    }
}

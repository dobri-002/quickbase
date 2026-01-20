using Backend.PopulationAggregator;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.EFCore;

/// <summary>
/// Helper class that uses EF Core to query country populations.
/// Used for comparison testing against ADO.NET implementation.
/// </summary>
public static class EFCorePopulationAggregator
{
    public static async Task<IReadOnlyList<CountryPopulation>> GetCountryPopulationsAsync()
    {
        await using var context = new PopulationDbContext();

        var countryPopulations = await context.Countries
            .Select(country => new CountryPopulation(
                country.CountryName,
                country.States
                    .SelectMany(state => state.Cities)
                    .Sum(city => city.Population)
            ))
            .AsNoTracking()
            .ToListAsync();

        return countryPopulations;
    }
}

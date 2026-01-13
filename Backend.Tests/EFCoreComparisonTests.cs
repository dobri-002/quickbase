using Backend.PopulationAggregator;
using Backend.Tests.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests;

/// <summary>
/// Tests that verify the ADO.NET SQL implementation against EF Core LINQ queries.
/// This ensures SQL correctness by comparing two independent implementations.
/// </summary>
public class EFCoreComparisonTests
{
    [Fact]
    public async Task ADONet_ShouldMatchEFCore_ExactResults()
    {
        var dbManager = new SqliteDbManager();

        var adoResults = SqlitePopulationAggregator
            .GetCountryPopulations(dbManager)
            .OrderBy(x => x.Country)
            .ToList();

        var efResults = (await EFCorePopulationAggregator
            .GetCountryPopulationsAsync())
            .OrderBy(x => x.Country)
            .ToList();

        adoResults.Should().BeEquivalentTo(
            efResults,
            options => options.WithStrictOrdering(),
            "Both implementations should return identical country populations"
        );
    }
}

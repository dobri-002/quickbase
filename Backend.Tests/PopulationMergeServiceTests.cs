using Backend.PopulationAggregator;

namespace Backend.Tests;

public class PopulationMergeServiceTests
{
    [Fact]
    public void MergePopulations_WithNoConflicts_ReturnsAllPopulations()
    {
        var source1 = new List<CountryPopulation>
        {
            new("USA", 331_000_000),
            new("Canada", 38_000_000)
        };

        var source2 = new List<CountryPopulation>
        {
            new("Mexico", 128_000_000),
            new("Brazil", 213_000_000)
        };

        var result = PopulationMergeService.MergePopulations(source1, source2,
            conflictResolver: (source1Item, source2Item) => source1Item).ToList();

        result.Should().HaveCount(4);
        result.Should().Contain(cp => cp.Country == "USA" && cp.Population == 331_000_000);
        result.Should().Contain(cp => cp.Country == "Canada" && cp.Population == 38_000_000);
        result.Should().Contain(cp => cp.Country == "Mexico" && cp.Population == 128_000_000);
        result.Should().Contain(cp => cp.Country == "Brazil" && cp.Population == 213_000_000);
    }

    [Fact]
    public void MergePopulations_WithConflicts_Source1Wins()
    {
        var source1 = new List<CountryPopulation>
        {
            new("USA", 300_000_000), // Conflict - should win
            new("Canada", 35_000_000)
        };

        var source2 = new List<CountryPopulation>
        {
            new("USA", 331_000_000),
            new("Mexico", 128_000_000)
        };

        var result = PopulationMergeService.MergePopulations(
                source1,
                source2,
                conflictResolver: (source1Item, source2Item) => source1Item
            ).ToList();

        result.Should().HaveCount(3);
        result.Should().Contain(cp => cp.Country == "USA" && cp.Population == 300_000_000);
        result.Should().Contain(cp => cp.Country == "Canada" && cp.Population == 35_000_000);
        result.Should().Contain(cp => cp.Country == "Mexico" && cp.Population == 128_000_000);
    }

    [Fact]
    public void MergePopulations_WithConflicts_Source2Wins()
    {
        var source1 = new List<CountryPopulation>
        {
            new("USA", 300_000_000),
            new("Canada", 35_000_000)
        };

        var source2 = new List<CountryPopulation>
        {
            new("USA", 331_000_000),  // Conflict - should win
            new("Mexico", 128_000_000)
        };

        var result = PopulationMergeService.MergePopulations(
                source1,
                source2,
                conflictResolver: (source1Item, source2Item) => source2Item
            ).ToList();

        result.Should().HaveCount(3);
        result.Should().Contain(cp => cp.Country == "USA" && cp.Population == 331_000_000);
        result.Should().Contain(cp => cp.Country == "Canada" && cp.Population == 35_000_000);
        result.Should().Contain(cp => cp.Country == "Mexico" && cp.Population == 128_000_000);
    }
}
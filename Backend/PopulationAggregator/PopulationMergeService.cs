namespace Backend.PopulationAggregator;

public static class PopulationMergeService
{
    public static IEnumerable<CountryPopulation> MergePopulations(
        IEnumerable<CountryPopulation> source1,
        IEnumerable<CountryPopulation> source2,
        Func<CountryPopulation, CountryPopulation, CountryPopulation> conflictResolver)
    {
        var merged = source1.ToDictionary(
            cp => cp.Country,
            cp => cp,
            StringComparer.OrdinalIgnoreCase);

        foreach (var source2Item in source2)
        {
            if (merged.TryGetValue(source2Item.Country, out var existing))
            {
                merged[source2Item.Country] = conflictResolver(existing, source2Item);
            }
            else
            {
                merged[source2Item.Country] = source2Item;
            }
        }

        return merged.Values;
    }
}
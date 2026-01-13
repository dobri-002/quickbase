namespace Backend.PopulationAggregator;

internal static class StatServicePopulationAggregator
{
    public static IEnumerable<CountryPopulation> GetCountryPopulations(IStatService statService)
    {
        var data = statService.GetCountryPopulations();
        return data.Select(tuple => new CountryPopulation(tuple.Item1, tuple.Item2));
    }
}
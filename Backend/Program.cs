using Backend;
using Backend.PopulationAggregator;

Console.WriteLine("Started");
Console.WriteLine("Aggregating population data...");

var dbManager = new SqliteDbManager();
var statService = new ConcreteStatService();

// Get aggregated data from each source
var statServicePopulations = StatServicePopulationAggregator.GetCountryPopulations(statService);
var sqlitePopulations = SqlitePopulationAggregator.GetCountryPopulations(dbManager);

var finalPopulations = PopulationMergeService.MergePopulations(
    statServicePopulations, 
    sqlitePopulations,
    conflictResolver: (statServiceItem, sqliteItem) => sqliteItem // In case of conflict, prefer database value
);

Console.WriteLine();
Console.WriteLine("Merged Country Populations:");
foreach (var data in finalPopulations.OrderBy(cp => cp.Country))
{
    Console.WriteLine($"{data.Country}: {data.Population:N0}");
}
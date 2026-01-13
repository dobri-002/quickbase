namespace Backend.PopulationAggregator;

public static class SqlitePopulationAggregator
{
    public static IEnumerable<CountryPopulation> GetCountryPopulations(IDbManager dbManager)
    {

        using var connection = dbManager.GetConnection();
        if (connection == null)
            throw new InvalidOperationException("Cannot initialize dbManager connection");

        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Country.CountryName, SUM(population) as total_population
            FROM Country
            INNER JOIN State ON Country.CountryId = State.CountryId
            INNER JOIN City ON State.StateId = City.StateId
            GROUP BY Country.CountryName";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var country = reader.GetString(0);
            var population = reader.GetInt32(1);
            yield return new CountryPopulation(country, population);
        }
    }
}
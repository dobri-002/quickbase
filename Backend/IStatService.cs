namespace Backend;

interface IStatService
{
    List<Tuple<string, int>> GetCountryPopulations();
    Task<List<Tuple<string, int>>> GetCountryPopulationsAsync();
}

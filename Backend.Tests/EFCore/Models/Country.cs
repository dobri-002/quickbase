namespace Backend.Tests.EFCore.Models;

public class Country
{
    public int CountryId { get; set; }
    public string CountryName { get; set; } = string.Empty;

    public ICollection<State> States { get; set; } = new List<State>();
}

namespace Backend.Tests.EFCore.Models;

public class City
{
    public int CityId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public int Population { get; set; }
    public int StateId { get; set; }

    public State State { get; set; } = null!;
}

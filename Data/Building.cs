using System.ComponentModel.DataAnnotations;

namespace NotAnotherBasePlanner.Data;

public class Building
{
	[Key] public string Ticker { get; set; }

	public string Name { get; set; }
	public string? Expertise { get; set; }
	public int Pioneers { get; set; }
	public int Settlers { get; set; }
	public int Technicians { get; set; }
	public int Engineers { get; set; }
	public int Scientists { get; set; }
	public int AreaCost { get; set; }
	public List<BuildingCost> BuildingCosts { get; set; }
	public List<Recipe> Recipes { get; set; }
}
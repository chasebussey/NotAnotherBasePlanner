using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class BuildingService
{
	public BuildingService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	private PlannerContext DbContext { get; }

	public async void UpdateAllBuildingsFromFIO()
	{
		var buildings = new List<Building>();

		var fioClient = new HttpClient();
		var response  = await fioClient.GetAsync("https://rest.fnar.net/building/allbuildings");

		if (response.IsSuccessStatusCode)
		{
			using var contentStream = await response.Content.ReadAsStreamAsync();

			buildings = await JsonSerializer.DeserializeAsync<List<Building>>(contentStream);
		}

		foreach (var building in buildings)
		{
			if (!DbContext.Buildings.Contains(building)) DbContext.Buildings.Add(building);
			foreach (var buildingCost in building.BuildingCosts)
			{
				buildingCost.BuildingTicker = building.Ticker;
				DbContext.BuildingCosts.Add(buildingCost);
			}
		}

		DbContext.SaveChanges();
	}

	public async Task<Building[]> GetBuildingsAsync()
	{
		return await DbContext.Buildings.ToArrayAsync();
	}

	public async Task<Building> GetBuildingByTickerAsync(string ticker)
	{
		// safe since Ticker should be unique
		return await DbContext.Buildings.FirstAsync(x => x.Ticker.Equals(ticker));
	}

	// TODO: Determine if there is any need for update, create, delete
	//       I don't think there is

	// Okay, BuildingCost Methods probably don't belong here
	public async Task<BuildingCost[]> GetBuildingCostsAsync()
	{
		return await DbContext.BuildingCosts.ToArrayAsync();
	}

	public BuildingCost[] GetBuildingCostsByTicker(string ticker)
	{
		return DbContext.BuildingCosts.Where(x => x.BuildingTicker == ticker).ToArray();
	}

	public void LoadBuildingCosts(Building b)
	{
		DbContext.Entry(b).Collection(x => x.BuildingCosts).Load();
	}
}
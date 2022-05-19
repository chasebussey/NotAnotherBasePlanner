using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class BuildingService
{
    private PlannerContext DbContext { get; set; }

    public BuildingService(PlannerContext dbContext)
    {
        this.DbContext = dbContext;
    }

    public async void UpdateAllBuildingsFromFIO()
    {
        List<Building>? buildings = new List<Building>();

        HttpClient fioClient = new HttpClient();
        HttpResponseMessage response = await fioClient.GetAsync("https://rest.fnar.net/building/allbuildings");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();

            buildings = await JsonSerializer.DeserializeAsync<List<Building>>(contentStream);
        }

        foreach (Building building in buildings)
        {
            if (!DbContext.Buildings.Contains(building))
            {

                DbContext.Buildings.Add(building);
            }
            foreach (BuildingCost buildingCost in building.BuildingCosts)
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
        return await DbContext.Buildings.FirstAsync(x => x.Ticker.Equals(ticker, StringComparison.InvariantCultureIgnoreCase));
    }

    // TODO: Determine if there is any need for update, create, delete
    //       I don't think there is
}
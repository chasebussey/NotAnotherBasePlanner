using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class PlanetService
{
    private PlannerContext DbContext { get; set; }
    private MaterialService MaterialService { get; set; }
    public PlanetService(PlannerContext dbContext)
    {
        this.DbContext = dbContext;
    }

    public PlanetService(PlannerContext dbContext, MaterialService materialService)
    {
        this.DbContext = dbContext;
        this.MaterialService = materialService;
    }

    public async void UpdateAllPlanetsFromFIO()
    {
        List<Planet>? planets = new List<Planet>();

        HttpClient fioClient = new HttpClient();
        HttpResponseMessage response = await fioClient.GetAsync("https://rest.fnar.net/planet/allplanets/full");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();

            planets = await JsonSerializer.DeserializeAsync<List<Planet>>(contentStream);
        }

        foreach (Planet planet in planets)
        {
            foreach (Resource resource in planet.Resources)
            {
                resource.Material = MaterialService.GetMaterialByFIOId(resource.FIOId);
                resource.MaterialTicker = resource.Material.Ticker;
            }
            if (!DbContext.Planets.Select(p => p.Designation).ToList().Contains(planet.Designation))
            {
                DbContext.Planets.Add(planet);
            }
            // TODO: We'll need an update eventually, but likely not till the universe resets
        }

        DbContext.SaveChanges();
    }
}
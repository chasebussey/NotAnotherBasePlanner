using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class PlanetService
{
    private PlannerContext DbContext { get; set; }
    public PlanetService(PlannerContext dbContext)
    {
        this.DbContext = dbContext;
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
            if (!DbContext.Planets.Contains(planet))
            {

                DbContext.Planets.Add(planet);
            }
        }

        DbContext.SaveChanges();
    }
}
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class PlanetService
{
	public PlanetService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	public PlanetService(PlannerContext dbContext, MaterialService materialService)
	{
		DbContext       = dbContext;
		MaterialService = materialService;
	}

	private PlannerContext DbContext { get; }
	private MaterialService MaterialService { get; }

	public async void UpdateAllPlanetsFromFIO()
	{
		var planets = new List<Planet>();

		var fioClient = new HttpClient();
		var response  = await fioClient.GetAsync("https://rest.fnar.net/planet/allplanets/full");

		if (response.IsSuccessStatusCode)
		{
			using var contentStream = await response.Content.ReadAsStreamAsync();

			planets = await JsonSerializer.DeserializeAsync<List<Planet>>(contentStream);
		}

		foreach (var planet in planets)
		{
			foreach (var resource in planet.Resources)
			{
				resource.Material       = MaterialService.GetMaterialByFIOId(resource.FIOId);
				resource.MaterialTicker = resource.Material.Ticker;
			}

			if (!DbContext.Planets.Select(p => p.Designation).ToList().Contains(planet.Designation))
				DbContext.Planets.Add(planet);
			// TODO: We'll need an update eventually, but likely not till the universe resets
		}

		DbContext.SaveChanges();
	}

	public async Task<Planet> GetPlanetByDesignationOrDisplayName(string searchString)
	{
		return await DbContext.Planets.FirstOrDefaultAsync(
			x => x.Designation.Equals(searchString, StringComparison.InvariantCultureIgnoreCase) ||
			     (x.DisplayName != null &&
			      x.DisplayName.Equals(searchString, StringComparison.InvariantCultureIgnoreCase)));
	}

	public async Task<Planet[]> GetPlanetsByFactionCode(string factionCode)
	{
		return await DbContext.Planets.Where(x =>
			                                     x.FactionCode != null &&
			                                     x.FactionCode.Equals(factionCode,
			                                                          StringComparison.InvariantCultureIgnoreCase))
		                      .ToArrayAsync();
	}

	public async Task<Planet[]> GetPlanetsLikeDesignationOrDisplayNameAsync(string searchString)
	{
		return await DbContext.Planets.Where(x =>
			                                     x.Designation.Contains(searchString) ||
			                                     (x.DisplayName != null && x.DisplayName.Contains(searchString)))
		                      .ToArrayAsync();
	}

	public async Task<Planet[]> GetPlanetsLikeDesignationOrDisplayName(string searchString)
	{
		return await DbContext.Planets.Where(x =>
			                               x.Designation.Contains(searchString) ||
			                               (x.DisplayName != null && x.DisplayName.Contains(searchString))).ToArrayAsync();
	}

	public Planet LoadPlanetResources(Planet planet)
	{
		DbContext.Entry(planet).Collection(x => x.Resources).Load();
		return planet;
	}
}
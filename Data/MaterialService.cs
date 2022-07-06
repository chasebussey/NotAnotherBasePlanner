using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class MaterialService
{
	public MaterialService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	private PlannerContext DbContext { get; }

	public async void UpdateAllMaterialsFromFIO()
	{
		var materials = new List<Material>();

		var fioClient = new HttpClient();
		var response  = await fioClient.GetAsync("https://rest.fnar.net/material/allmaterials");

		if (response.IsSuccessStatusCode)
		{
			using var contentStream = await response.Content.ReadAsStreamAsync();

			materials = await JsonSerializer.DeserializeAsync<List<Material>>(contentStream);
		}

		foreach (var material in materials)
			if (DbContext.Materials.Contains(material))
			{
				var curr = DbContext.Materials.Where(x => x.Ticker == material.Ticker).First();
				curr.CategoryName = material.CategoryName;
				curr.FIOId        = material.FIOId;
				curr.Name         = material.Name;
				curr.Volume       = material.Volume;
				curr.Weight       = material.Weight;
			}
			else
			{
				DbContext.Materials.Add(material);
			}

		DbContext.SaveChanges();
	}

	public async Task<Material[]> GetMaterialsAsync()
	{
		return await DbContext.Materials.ToArrayAsync();
	}

	public async Task<Material> GetMaterialByTickerAsync(string ticker)
	{
		return await DbContext.Materials.FirstAsync(x => x.Ticker == ticker);
	}

	public Material GetMaterialByTicker(string ticker)
	{
		return DbContext.Materials.First(x => x.Ticker == ticker);
	}

	public async Task<Material> GetMaterialByFIOIdAsync(string FIOId)
	{
		return await DbContext.Materials.FirstAsync(x => x.FIOId == FIOId);
	}

	public Material GetMaterialByFIOId(string FIOId)
	{
		return DbContext.Materials.First(x => x.FIOId == FIOId);
	}

	public async Task<Material[]> GetMaterialsByCategoryAsync(string category)
	{
		return await DbContext.Materials.Where(x =>
			                                       !string.IsNullOrEmpty(x.CategoryName) &&
			                                       x.CategoryName.Equals(
				                                       category, StringComparison.InvariantCultureIgnoreCase))
		                      .ToArrayAsync();
	}
}
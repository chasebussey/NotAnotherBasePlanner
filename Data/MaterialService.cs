using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class MaterialService
{
    private PlannerContext DbContext { get; set; }

    public MaterialService(PlannerContext dbContext)
    {
        this.DbContext = dbContext;
    }
    public async void UpdateAllMaterialsFromFIO()
    {
        List<Material>? materials = new List<Material>();

        HttpClient fioClient = new HttpClient();
        HttpResponseMessage response = await fioClient.GetAsync("https://rest.fnar.net/material/allmaterials");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();

            materials = await JsonSerializer.DeserializeAsync<List<Material>>(contentStream);
        }

        foreach (Material material in materials)
        {
            if (DbContext.Materials.Contains(material))
            {
                Material curr = DbContext.Materials.Where(x => x.Ticker == material.Ticker).First();
                curr.CategoryName = material.CategoryName;
                curr.FIOId = material.FIOId;
                curr.Name = material.Name;
                curr.Volume = material.Volume;
                curr.Weight = material.Weight;
            }
            else
            {
                DbContext.Materials.Add(material);
            }
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
            x.CategoryName.Equals(category, StringComparison.InvariantCultureIgnoreCase))
            .ToArrayAsync();
    }
}
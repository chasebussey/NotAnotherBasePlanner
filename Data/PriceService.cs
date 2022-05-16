using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class PriceService
{
    private PlannerContext DbContext { get; set; }

    public PriceService(PlannerContext dbContext)
    {
        this.DbContext = dbContext;
    }
    public async void UpdateAllPricesFromFIO()
    {
        List<Price>? prices = new List<Price>();

        HttpClient fioClient = new HttpClient();
        HttpResponseMessage response = await fioClient.GetAsync("https://rest.fnar.net/exchange/all");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            prices = await JsonSerializer.DeserializeAsync<List<Price>>(contentStream);
        }

        foreach (Price price in prices)
        {
            DbContext.Prices.Add(price);
        }

        DbContext.SaveChanges();
    }

    public async Task<Price[]> GetPricesAsync()
    {
        return await DbContext.Prices.ToArrayAsync();
    }

    public async Task<Price[]> GetPricesByTickerAsync(string materialTicker)
    {
        return await DbContext.Prices.Where(x =>
            x.MaterialTicker.Equals(materialTicker, StringComparison.InvariantCultureIgnoreCase))
            .ToArrayAsync();
    }

    public async Task<Price[]> GetMarketMakerPrices()
    {
        return await DbContext.Prices.Where(x => x.MarketMakerBuy != null).ToArrayAsync();
    }

    public async Task<Price[]> GetPricesByExchangeCodeAsync(string exchangeCode)
    {
        return await DbContext.Prices.Where(x =>
            x.ExchangeCode.Equals(exchangeCode, StringComparison.InvariantCultureIgnoreCase))
            .ToArrayAsync();
    }
}
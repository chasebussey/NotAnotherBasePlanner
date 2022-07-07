using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class PriceService
{
	public PriceService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	private PlannerContext DbContext { get; }

	public async void UpdateAllPricesFromFIO()
	{
		var prices = new List<Price>();

		var fioClient = new HttpClient();
		var response  = await fioClient.GetAsync("https://rest.fnar.net/exchange/all");

		if (response.IsSuccessStatusCode)
		{
			using var contentStream = await response.Content.ReadAsStreamAsync();
			prices = await JsonSerializer.DeserializeAsync<List<Price>>(contentStream);
		}

		foreach (var price in prices) DbContext.Prices.Add(price);

		DbContext.SaveChanges();
	}

	public async Task<Price[]> GetPricesAsync()
	{
		return await DbContext.Prices.ToArrayAsync();
	}

	public Price[] GetPrices()
	{
		return DbContext.Prices.ToArray();
	}

	public async Task<Price[]> GetPricesByTickerAsync(string materialTicker)
	{
		return await DbContext.Prices.Where(x =>
			                                    x.MaterialTicker.Equals(
				                                    materialTicker, StringComparison.InvariantCultureIgnoreCase))
		                      .ToArrayAsync();
	}

	public async Task<Price[]> GetMarketMakerPrices()
	{
		return await DbContext.Prices.Where(x => x.MarketMakerBuy != null).ToArrayAsync();
	}

	public async Task<Price[]> GetPricesByExchangeCodeAsync(string exchangeCode)
	{
		return await DbContext.Prices.Where(x =>
			                                    x.ExchangeCode.Equals(exchangeCode,
			                                                          StringComparison.InvariantCultureIgnoreCase))
		                      .ToArrayAsync();
	}
}
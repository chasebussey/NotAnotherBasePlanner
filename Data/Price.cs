using System.Text.Json.Serialization;

namespace NotAnotherBasePlanner.Data;

public class Price
{
	public string MaterialTicker { get; set; }
	public string ExchangeCode { get; set; }

	[JsonPropertyName("MMBuy")] public double? MarketMakerBuy { get; set; }

	[JsonPropertyName("MMSell")] public double? MarketMakerSell { get; set; }

	public double? PriceAverage { get; set; }
	public double? Ask { get; set; }
	public int? Supply { get; set; }
	public int? BidCount { get; set; }
	public double? Bid { get; set; }
	public int? Demand { get; set; }
}
namespace NotAnotherBasePlanner.Data;

public class MaterialPrice
{
    public string MaterialTicker { get; set; }
    public Material Material { get; set; }
    public string ExchangeCode { get; set; }
    public Price Price { get; set; }
}
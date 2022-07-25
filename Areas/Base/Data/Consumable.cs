namespace NotAnotherBasePlanner.Data;

public class Consumable
{
	public int Id { get; set; }
	public string Ticker { get; set; }
	public double Amount { get; set; }
	public string WorkforceType { get; set; }
	public bool Luxury { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace NotAnotherBasePlanner.Data;

public class UserPrice
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string MaterialTicker { get; set; }
    public Material Material { get; set; }

    public double Price { get; set; }
    public string CurrencyCode { get; set; }
}
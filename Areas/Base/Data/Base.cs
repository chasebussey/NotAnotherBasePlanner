using System.ComponentModel.DataAnnotations;

namespace NotAnotherBasePlanner.Data;

public class Base
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public List<BaseBuilding> Buildings { get; set; }
    public int AgricultureExperts { get; set; }
    public int ChemistryExperts { get; set; }
    public int ConstructionExperts { get; set; }
    public int ElectronicsExperts { get; set; }
    public int FoodExperts { get; set; }
    public int FuelExperts { get; set; }
    public int ManufacturingExperts { get; set; }
    public int MetallurgyExperts { get; set; }
    public int ExtractionExperts { get; set; }
    public int Permits { get; set; }
    public int AvailableArea { get; set; }
    public int UsedArea { get; set; }

    public Planet Planet { get; set; }
}
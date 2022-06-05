using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotAnotherBasePlanner.Data;

public class CorporateProjectItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string MaterialTicker { get; set; }
    public Material Material { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public string UserID { get; set; }
    public ApplicationUser User { get; set; }
    public string CorporateProjectId { get; set; }
    public CorporateProject CorporateProject { get; set; }

    // Hate using double here, but for consistency with FIO API, do it
    public double Price { get; set; }
}
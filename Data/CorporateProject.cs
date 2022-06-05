using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotAnotherBasePlanner.Data;

public class CorporateProject
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public ApplicationUser Owner { get; set; }
    public string UserID { get; set; }

    public List<CorporateProjectItem> ProjectItems { get; set; }
    public Corporation Corp { get; set; }
    public string CorpCode { get; set; }
    public List<CorporateProjectUser> Participants { get; set; }
}
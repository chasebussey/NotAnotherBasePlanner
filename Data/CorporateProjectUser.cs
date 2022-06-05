using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotAnotherBasePlanner.Data;

public class CorporateProjectUser
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string CorporateProjectId { get; set; }
    public CorporateProject CorporateProject { get; set; }
}
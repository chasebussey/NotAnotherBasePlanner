using System.ComponentModel.DataAnnotations;

namespace NotAnotherBasePlanner.Data;

public class Corporation
{
    [Key]
    public string Code { get; set; }
    public List<ApplicationUser> Users { get; set; }
    public List<CorporateProject> Projects { get; set; }
    public string Name { get; set; }

}
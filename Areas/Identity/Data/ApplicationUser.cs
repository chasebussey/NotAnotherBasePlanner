using Microsoft.AspNetCore.Identity;
using NotAnotherBasePlanner.Data;

public class ApplicationUser : IdentityUser
{
	public string? FIOApiKey { get; set; }

	public List<Base>? Bases { get; set; }
}
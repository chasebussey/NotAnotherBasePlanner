using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FIOApiKey { get; set; }
}
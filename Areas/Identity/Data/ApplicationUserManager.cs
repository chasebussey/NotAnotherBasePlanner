using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class ApplicationUserManager : UserManager<ApplicationUser>
{
	public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
	                              IPasswordHasher<ApplicationUser> passwordHasher,
	                              IEnumerable<IUserValidator<ApplicationUser>> userValidators,
	                              IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
	                              ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
	                              IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(
		store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
		logger)
	{
	}

	public async Task<string> GetFIOAPIKeyAsync(ApplicationUser user)
	{
		return user.FIOApiKey;
	}

	public async Task<IdentityResult> SetFIOAPIKeyAsync(ApplicationUser user, string fioApiKey)
	{
		user.FIOApiKey = fioApiKey;
		var result = await Store.UpdateAsync(user, new CancellationToken());

		return result;
	}

	public async Task<string> GetUserIdAsync(string username)
	{
		ApplicationUser user = await Store.FindByNameAsync(username, new CancellationToken());
		return user.Id;
	}
}
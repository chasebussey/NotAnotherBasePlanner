using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class UserPriceService
{
	private PlannerContext DbContext { get; set; }

	public UserPriceService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	public async Task<int> AddUserPrice(string materialTicker, double price, string userId)
	{
		DbContext.UserPrices.Add(new UserPrice
		{
			MaterialTicker = materialTicker,
			Price          = price,
			UserId         = userId
		});

		return await DbContext.SaveChangesAsync();
	}

	public async Task<List<UserPrice>> GetUserPrices(string userId)
	{
		return await DbContext.UserPrices.Where(x => x.UserId.Equals(userId)).ToListAsync();
	}
}
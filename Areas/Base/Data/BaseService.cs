using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class BaseService
{
	private PlannerContext DbContext { get; set; }

	public BaseService(PlannerContext dbContext)
	{
		this.DbContext = dbContext;
	}

	public async void AddBaseAsync(Base b)
	{
		DbContext.Bases.Add(b);
		await DbContext.SaveChangesAsync();
	}

	public async Task<List<Base>> GetBasesByUserIdAsync(string userId)
	{
		return await DbContext.Bases.Where(x => x.ApplicationUserId.Equals(userId))
		                      .Include(x => x.Planet).ToListAsync();
	}
}
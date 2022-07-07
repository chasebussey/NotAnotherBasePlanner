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

	public async void UpdateBaseAsync(Base b)
	{
		DbContext.Bases.Update(b);
		await DbContext.SaveChangesAsync();
	}

	public async Task<List<Base>> GetBasesByUserIdAsync(string userId)
	{
		return await DbContext.Bases.Where(x => x.ApplicationUserId.Equals(userId))
		                      .Include(x => x.Planet).ToListAsync();
	}

	public async Task<Base> GetBaseByBaseIdAsync(string id)
	{
		return await DbContext.Bases.FirstAsync(x => x.Id == new Guid(id));
	}

	public Base GetBaseByBaseId(string id)
	{
		return DbContext.Bases.First(x => x.Id == new Guid(id));
	}
}
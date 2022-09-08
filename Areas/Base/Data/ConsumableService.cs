using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class ConsumableService
{
	private PlannerContext DbContext { get; }
	
	public ConsumableService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	public async Task<List<Consumable>> GetConsumables()
	{
		return await DbContext.Consumables.ToListAsync();
	}

	public async Task<List<Consumable>> GetConsumablesByWorkforce(string workforce)
	{
		return await DbContext.Consumables
		                      .Where(
			                      x => x.WorkforceType == workforce)
		                      .ToListAsync();
	}
}
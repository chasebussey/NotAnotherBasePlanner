using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class BaseBuildingService
{
	private PlannerContext DbContext { get; set; }
	public BaseBuildingService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	public async Task<int> UpdateBaseBuilding(BaseBuilding building)
	{
		DbContext.BaseBuildings.Update(building);
		return await DbContext.SaveChangesAsync();
	}
}
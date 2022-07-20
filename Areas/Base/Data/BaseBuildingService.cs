using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class BaseBuildingService
{
	private PlannerContext DbContext { get; set; }
	public BaseBuildingService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	public async void UpdateBaseBuilding(BaseBuilding building)
	{
		DbContext.BaseBuildings.Update(building);
		await DbContext.SaveChangesAsync();
	}
}
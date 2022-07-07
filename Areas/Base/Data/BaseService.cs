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
}
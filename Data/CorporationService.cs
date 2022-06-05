using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class CorporationService
{
    private PlannerContext DbContext { get; set; }

    public CorporationService(PlannerContext dbContext)
    {
        this.DbContext = dbContext;
    }

    #region Corporation
    public async Task<Corporation[]> GetAllCorporationsAsync()
    {
        return await DbContext.Corporations.ToArrayAsync();
    }

    public async void AddCorporationAsync(Corporation corp)
    {
        await DbContext.Corporations.AddAsync(corp);
        await DbContext.SaveChangesAsync();
    }

    public async Task<Corporation> GetCorporationByCodeAsync(string code)
    {
        return await DbContext.Corporations.Where(c =>
            c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase))
            .FirstAsync();
    }
    #endregion

    #region Corporate Project
    public async Task<CorporateProject[]> GetCorporateProjectsAsync(string code)
    {
        return await DbContext.CorporateProjects.Where(cp =>
            cp.CorpCode.Equals(code, StringComparison.InvariantCultureIgnoreCase))
            .ToArrayAsync();
    }

    public async Task<CorporateProject[]> GetCorporateProjectsByOwnerAsync(string username)
    {
        return await DbContext.CorporateProjects.Where(cp =>
            cp.Owner.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase))
            .ToArrayAsync();
    }

    //    public async Task<CorporateProject[]> GetCorporateProjectsByParticipantAsync(string username)
    //    {

    //    }

    public async void AddCorporateProjectAsync(CorporateProject corporateProject)
    {
        await DbContext.AddAsync(corporateProject);
        await DbContext.SaveChangesAsync();
    }
    #endregion
}
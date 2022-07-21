using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using NotAnotherBasePlanner.Data;

namespace NotAnotherBasePlanner.Areas.Base.Pages;

public partial class BaseDesigner
{ 
    [Parameter]
    public Planet planet { get; set; }
    [Parameter]
    public Data.Base basePlan { get; set; }
    [Parameter]
    public bool Create { get; set; }
    public string buildingTicker { get; set; }
    public bool addBuilding { get; set; }
    public List<BaseBuildingRecipe> BaseBuildingRecipes { get; set; }

    public string PlanetFullDesignation => !string.IsNullOrEmpty(planet.DisplayName) ? $"{planet.Designation} | {planet.DisplayName}" : planet.Designation;

    public string PlanetSurface => planet.Surface ? "ROCKY" : "GASEOUS";

    #region Injects
    [Inject] private PlanetService PlanetService { get; set; } 
    [Inject] private BuildingService BuildingService { get; set; }
    [Inject] private BaseBuildingService BaseBuildingService { get; set; }
    [Inject] private BaseService BaseService { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private ApplicationUserManager UserManager { get; set; }
    #endregion

    private string PlanetGravity => planet switch
    {
    { Gravity: < 0.25 } => "Low",
    { Gravity: > 2.5 } => "High",
        _ => "Normal"
        };

    private string PlanetTemperature => planet switch
    {
    {Temperature: < -25} => "Low",
    {Temperature: > 75} => "High",
        _ => "Normal"
        };

    private string PlanetPressure => planet switch
    {
    {Pressure: < 0.25} => "Low",
    {Pressure: > 2.0} => "High",
        _ => "Normal"
        };

    protected override void OnInitialized()
    {
        base.OnInitialized();
        BaseBuildingRecipes = new List<BaseBuildingRecipe>();
        planet = PlanetService.LoadPlanetResources(planet);
        if (basePlan is null)
        {
            basePlan = new Data.Base();
            basePlan.Buildings = new List<BaseBuilding>();
            AddBuilding("CM");
        }
        
    }

    public async void AddBuilding(string buildingTicker)
    {
        var building = await BuildingService.GetBuildingByTickerAsync(buildingTicker);
        var baseBuilding = new BaseBuilding
        {
            BuildingTicker = buildingTicker,
            Building = building,
            BaseId = basePlan.Id,
            Efficiency = 0.0,
            Constructed = false
        };

        basePlan.Buildings.Add(baseBuilding);
        HideAddBuildingForm();
    }

    public void DisplayAddBuildingForm()
    {
        addBuilding = true;
    }

    public void HideAddBuildingForm()
    {
        addBuilding = false;
        StateHasChanged();
    }

    public void MarkConstructed(BaseBuilding building)
    {
        building.Constructed = true;
    }

    public void SetShowCosts()
    {
    }

    public async void SaveBase()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity == null) return;
        var user = authState.User.Identity.Name;

        var userId = await UserManager.GetUserIdAsync(user);
        basePlan.ApplicationUserId = userId;
        basePlan.Planet = planet;
        if (Create)
        {
            BaseService.AddBaseAsync(basePlan);
        }
        else
        {
            BaseService.UpdateBaseAsync(basePlan);
        }
    }

    public void DeleteBuilding(BaseBuilding building)
    {
        basePlan.Buildings.Remove(building);
    }

    public void SelectedRecipeChanged(ChangeEventArgs e, BaseBuilding building)
    {
        if (e.Value is not null && int.Parse(e.Value.ToString()) != 0)
        {
            BaseBuildingRecipe baseBuildingRecipe = new BaseBuildingRecipe()
            {
                BaseBuildingId = building.Id,
                RecipeId = int.Parse(e.Value.ToString())
            };
            if (BaseBuildingRecipes.Exists(x => x.BaseBuildingId == building.Id))
            {
                BaseBuildingRecipes.Remove(BaseBuildingRecipes.First(x => x.BaseBuildingId == building.Id));
            }
            
            BaseBuildingRecipes.Add(baseBuildingRecipe);
        }
    }

    public void AddRecipe(BaseBuilding building)
    {
        if (building.Recipes is null)
        {
            building.Recipes = new List<BaseBuildingRecipe>();
        }
        building.Recipes.Add(BaseBuildingRecipes.First(x => x.BaseBuildingId == building.Id));
        BaseBuildingService.UpdateBaseBuilding(building);
    }
}
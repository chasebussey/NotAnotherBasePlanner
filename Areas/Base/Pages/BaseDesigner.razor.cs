using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using NotAnotherBasePlanner.Data;
using NotAnotherBasePlanner.Pages;

namespace NotAnotherBasePlanner.Areas.Base.Pages;

public partial class BaseDesigner
{ 
    [Parameter]
    public Planet planet { get; set; }
    [Parameter]
    public Data.Base? basePlan { get; set; }
    [Parameter]
    public bool Create { get; set; }
    public string buildingTicker { get; set; }
    public bool addBuilding { get; set; }
    public List<BaseBuildingRecipe> BaseBuildingRecipes { get; set; }

    public string PlanetFullDesignation => !string.IsNullOrEmpty(planet.DisplayName) ? $"{planet.Designation} | {planet.DisplayName}" : planet.Designation;

    public string PlanetSurface => planet.Surface ? "ROCKY" : "GASEOUS";
    
    public List<ProductionItem> ProductionItems { get; set; }
    public List<Price> Prices { get; set; }
    public List<Consumable> Consumables { get; set; }
    public List<UserPrice> UserPrices { get; set; }
    
    public double ProfitPerDay { get; set; }
    
    public Dictionary<string, int> workforceFigures { get; set; }
    public Dictionary<string, int> workforceCapacity { get; set; }

    #region Injects
    [Inject] private PlanetService PlanetService { get; set; } 
    [Inject] private BuildingService BuildingService { get; set; }
    [Inject] private BaseBuildingService BaseBuildingService { get; set; }
    [Inject] private BaseService BaseService { get; set; }
    [Inject] private MaterialService MaterialService { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private ApplicationUserManager UserManager { get; set; }
    [Inject] private PriceService PriceService { get; set; }
    [Inject] private ConsumableService ConsumableService { get; set; }
    [Inject] private UserPriceService UserPriceService { get; set; }
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

    protected override async void OnInitialized()
    {
        await base.OnInitializedAsync();
        ProductionItems     = new List<ProductionItem>();
        BaseBuildingRecipes = new List<BaseBuildingRecipe>();
        planet              = PlanetService.LoadPlanetResources(planet);
        Prices              = PriceService.GetPrices().ToList();
        if (Create)
        {
            basePlan = new Data.Base();
            basePlan.Buildings = new List<BaseBuilding>();
            await AddBuilding("CM");
            await SaveBase();
            Create = false;
        }

        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var userId    = await UserManager.GetUserIdAsync(authState.User.Identity.Name);
        
        Consumables = await ConsumableService.GetConsumables();
        UserPrices  = await UserPriceService.GetUserPrices(userId);

        if (basePlan.Buildings.Count > 1)
        {
            CalculateWorkforce();
            CalculateArea();
            await RefreshProductionItems();
            StateHasChanged();
        }
    }

    public async Task AddBuilding(string buildingTicker)
    {
        var building = await BuildingService.GetBuildingByTickerAsync(buildingTicker);
        var baseBuilding = new BaseBuilding
        {
            BuildingTicker = buildingTicker,
            Building = building,
            BaseId = basePlan.Id,
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

    public async Task SaveBase()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity == null) return;
        var user = authState.User.Identity.Name;

        var userId = await UserManager.GetUserIdAsync(user);
        basePlan.ApplicationUserId = userId;
        basePlan.Planet = planet;
        if (Create)
        {
            await BaseService.AddBaseAsync(basePlan);
        }
        else
        {
            await BaseService.UpdateBaseAsync(basePlan);
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

    public async Task AddRecipe(BaseBuilding building)
    {
        BaseBuildingRecipe recipe = BaseBuildingRecipes.First(x => x.BaseBuildingId == building.Id);
        if (building.Recipes is null)
        {
            building.Recipes = new List<BaseBuildingRecipe>();
        }
        building.Recipes.Add(recipe);
        await BaseBuildingService.UpdateBaseBuilding(building);
        CalculateProductionItems(building);
    }

    public ProductionItem? GenerateProductionItem(string item, BaseBuildingRecipe recipe, int numBuilding, double efficiency, bool isInput)
    {
        if (ProductionItems.Any(x => x.Material.Ticker == item && x.Recipe == recipe.Recipe))
        {
            return null;
        }
        string   recipeStr = recipe.Recipe.RecipeName;
        Material mat       = MaterialService.GetMaterialByTicker(item);
        double   alloc     = recipe.Allocation;
        int      batchSize = int.Parse(Regex.Match(recipeStr, "([0-9]*)x" + item).Groups[1].Value);
        double   inputSize = ((double)batchSize / recipe.Recipe.TimeMs) * 86400000.0;
        
        double unitPrice;

        if (UserPrices is not null &&
            UserPrices.Any(x => x.MaterialTicker.Equals(item, StringComparison.InvariantCultureIgnoreCase)))
        {
            unitPrice = UserPrices.FirstOrDefault(x => x.MaterialTicker.Equals(item)).Price;
        }
        else
        {
            unitPrice = Prices.First(x => x.MaterialTicker.Equals(mat.Ticker) &&
                                          x.ExchangeCode.Equals("NC1", StringComparison.InvariantCultureIgnoreCase))
                              .PriceAverage ?? 0.0;
        }
        
        double amount = numBuilding * efficiency * inputSize * alloc;
        double price  = unitPrice * amount;

        return (new ProductionItem
        {
            Material = mat,
            Amount   = numBuilding * efficiency * inputSize * alloc,
            IsInput  = isInput,
            Recipe   = recipe.Recipe,
            Price    = price
        });
    }

    public void CalculateProductionItems(BaseBuilding building)
    {
        if (building.Recipes is null || building.Recipes.Count == 0)
        {
            return;
        }

        int    numBuilding = building.Quantity;
        double efficiency  = building.Efficiency;

        foreach (var recipe in building.Recipes)
        {
            foreach (string input in recipe.Recipe.Inputs)
            {
                ProductionItem? item = GenerateProductionItem(input, recipe, numBuilding, efficiency, isInput: true);
                if (item is not null)
                {
                    if (ProductionItems.Any(x => x.Material.Ticker == item.Material.Ticker))
                    {
                        ProductionItems.First(x => x.Material.Ticker == item.Material.Ticker).Amount += item.Amount;
                        ProductionItems.First(x => x.Material.Ticker == item.Material.Ticker).Price  += item.Price;
                    }
                    else
                    {
                        ProductionItems.Add(item);
                    }
                }
            }

            foreach (string output in recipe.Recipe.Outputs)
            {
                ProductionItem? item = GenerateProductionItem(output, recipe, numBuilding, efficiency, isInput: false);
                if (item is not null)
                {
                    if (ProductionItems.Any(x => x.Material.Ticker == item.Material.Ticker))
                    {
                        ProductionItems.First(x => x.Material.Ticker == item.Material.Ticker).Amount += item.Amount;
                        ProductionItems.First(x => x.Material.Ticker == item.Material.Ticker).Price  += item.Price;
                    }
                    else
                    {
                        ProductionItems.Add(item);
                    }
                }
            }
        }
    }

    // TODO: Make this the whole CalculateProductionItems method
    public async Task RefreshProductionItems()
    {
        ProductionItems = new List<ProductionItem>();
        ProfitPerDay    = 0.0;
        
        CalculateEfficiency();

        foreach (BaseBuilding building in basePlan.Buildings)
        {
            CalculateProductionItems(building);
        }

        await CalculateUpkeep();

        foreach (ProductionItem item in ProductionItems)
        {
            if (item.IsInput)
            {
                ProfitPerDay -= item.Price;
            }
            else
            {
                ProfitPerDay += item.Price;
            }
        }
        //StateHasChanged();
    }

    private async Task CalculateUpkeep()
    {
        foreach (string key in workforceFigures.Keys)
        {
            List<Consumable> consumables = await ConsumableService.GetConsumablesByWorkforce(key);

            foreach (Consumable con in consumables)
            {
                double unitPrice = unitPrice = Prices.First(x => x.MaterialTicker.Equals(con.Ticker) &&
                                                                 x.ExchangeCode.Equals("NC1", StringComparison.InvariantCultureIgnoreCase))
                                                     .PriceAverage ?? 0.0;

                double amount = (workforceFigures[key] / 100.0) * con.Amount;

                if (ProductionItems.Any(x => x.IsInput &&
                                             x.Material.Ticker == con.Ticker))
                {
                    ProductionItems.First(x => x.IsInput &&
                                               x.Material.Ticker == con.Ticker).Amount += amount;
                    ProductionItems.First(x => x.IsInput &&
                                               x.Material.Ticker == con.Ticker).Price += (unitPrice * amount);
                }
                else
                {
                    if (amount > 0)
                    {
                        ProductionItems.Add(new ProductionItem
                        {
                            Amount   = amount,
                            IsInput  = true,
                            Material = MaterialService.GetMaterialByTicker(con.Ticker),
                            Price    = unitPrice * amount
                        });
                    }
                }
            }
        }
    }

    public async Task AddExpert(int type)
    {
        var expertSum = basePlan.AgricultureExperts + basePlan.ChemistryExperts + basePlan.ConstructionExperts +
                       basePlan.ElectronicsExperts + basePlan.FoodExperts + basePlan.FuelExperts +
                       basePlan.ManufacturingExperts + basePlan.MetallurgyExperts + basePlan.ExtractionExperts;

        if (expertSum >= 6)
            return;
        
        switch (type)
        {
            case 1:
                if (basePlan.AgricultureExperts <= 4)
                    basePlan.AgricultureExperts++;
                break;
            case 2:
                if (basePlan.ChemistryExperts <= 4)
                    basePlan.ChemistryExperts++;
                break;
            case 3:
                if (basePlan.ConstructionExperts <= 4)
                    basePlan.ConstructionExperts++;
                break;
            case 4:
                if (basePlan.ElectronicsExperts <= 4)
                    basePlan.ElectronicsExperts++;
                break;
            case 5:
                if (basePlan.FoodExperts <= 4)
                    basePlan.FoodExperts++;
                break;
            case 6:
                if (basePlan.FuelExperts <= 4)
                    basePlan.FuelExperts++;
                break;
            case 7:
                if (basePlan.ManufacturingExperts <= 4)
                    basePlan.ManufacturingExperts++;
                break;
            case 8:
                if (basePlan.MetallurgyExperts <= 4)
                    basePlan.MetallurgyExperts++;
                break;
            case 9:
                if (basePlan.ExtractionExperts <= 4)
                    basePlan.ExtractionExperts++;
                break;
            default:
                break;
        }
        
        await RefreshProductionItems();
    }
    public async Task RemoveExpert(int type)
    {
        switch (type)
        {
            case 1:
                if (basePlan.AgricultureExperts >= 1)
                    basePlan.AgricultureExperts--;
                break;
            case 2:
                if (basePlan.ChemistryExperts >= 1)
                    basePlan.ChemistryExperts--;
                break;
            case 3:
                if (basePlan.ConstructionExperts >= 1)
                    basePlan.ConstructionExperts--;
                break;
            case 4:
                if (basePlan.ElectronicsExperts >= 1)
                    basePlan.ElectronicsExperts--;
                break;
            case 5:
                if (basePlan.FoodExperts >= 1)
                    basePlan.FoodExperts--;
                break;
            case 6:
                if (basePlan.FuelExperts >= 1)
                    basePlan.FuelExperts--;
                break;
            case 7:
                if (basePlan.ManufacturingExperts >= 1)
                    basePlan.ManufacturingExperts--;
                break;
            case 8:
                if (basePlan.MetallurgyExperts >= 1)
                    basePlan.MetallurgyExperts--;
                break;
            case 9:
                if (basePlan.ExtractionExperts >= 1)
                    basePlan.ExtractionExperts--;
                break;
            default:
                break;
        }
        
        await RefreshProductionItems();
    }

    public async Task IncreaseBuildingQuantity(BaseBuilding building)
    {
        building.Quantity++;
        await RefreshProductionItems();
    }

    public async Task DecreaseBuildingQuantity(BaseBuilding building)
    {
        if (building.Quantity > 0)
            building.Quantity--;
        await RefreshProductionItems();
    }

    public void CalculateWorkforce()
    {
        workforceFigures = new Dictionary<string, int>
        {
            {"PIO", 0},
            {"SET", 0},
            {"TEC", 0},
            {"ENG", 0},
            {"SCI", 0}
        };
        
        workforceCapacity = new Dictionary<string, int>
        {
            {"PIO", 0},
            {"SET", 0},
            {"TEC", 0},
            {"ENG", 0},
            {"SCI", 0}
        };
        
        foreach (BaseBuilding building in basePlan.Buildings)
        {
            workforceFigures["PIO"] += building.Building.Pioneers * building.Quantity;
            workforceFigures["SET"] += building.Building.Settlers * building.Quantity;
            workforceFigures["TEC"] += building.Building.Technicians * building.Quantity;
            workforceFigures["ENG"] += building.Building.Engineers * building.Quantity;
            workforceFigures["SCI"] += building.Building.Scientists * building.Quantity;
        }
        
        foreach (BaseBuilding building in basePlan.Buildings.Where(x => x.Building.Name.Contains("habitation")))
        {
            switch (building.Building.Ticker)
            {
                case "HB1":
                    workforceCapacity["PIO"] += 100 * building.Quantity;
                    break;
                case "HB2":
                    workforceCapacity["SET"] += 100 * building.Quantity;
                    break;
                case "HB3":
                    workforceCapacity["TEC"] += 100 * building.Quantity;
                    break;
                case "HB4":
                    workforceCapacity["ENG"] += 100 * building.Quantity;
                    break;
                case "HB5":
                    workforceCapacity["SCI"] += 100 * building.Quantity;
                    break;
                default:
                    break;
            }
        }
    }

    public void CalculateArea()
    {
        basePlan.UsedArea = 0;
        foreach (BaseBuilding building in basePlan.Buildings)
        {
            basePlan.UsedArea += (building.Building.AreaCost * building.Quantity);
        }
    }

    #region ReallyDumbExpertMethods

    public void CalculateEfficiency()
    {
        CalculateAgriculture();
        CalculateChemistry();
        CalculateConstruction();
        CalculateElectronics();
        CalculateFood();
        CalculateFuel();
        CalculateManufacturing();
        CalculateMetallurgy();
        CalculateExtraction();
    }

    public void CalculateAgriculture()
    {
        double expertBoost = ExpertBoost(basePlan.AgricultureExperts);
        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("AGRICULTURE"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("AGRICULTURE")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 1 ? 0.25 : 0.0);
        }
        
    }

    public void CalculateChemistry()
    {
        double expertBoost = ExpertBoost(basePlan.ChemistryExperts);

        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("CHEMISTRY"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("CHEMISTRY")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 2 ? 0.25 : 0.0);
        }
        
    }

    public void CalculateConstruction()
    {
        double expertBoost = ExpertBoost(basePlan.ConstructionExperts);
        
        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("CONSTRUCTION"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("CONSTRUCTION")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 3 ? 0.25 : 0.0);
        }
    }

    public void CalculateElectronics()
    {
        double expertBoost = ExpertBoost(basePlan.ElectronicsExperts);
        
        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("ELECTRONICS"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("ELECTRONICS")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 4 ? 0.25 : 0.0);
        }
    }

    public void CalculateFood()
    {
        double expertBoost = ExpertBoost(basePlan.FoodExperts);

        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("FOOD_INDUSTRIES"))) return;
        
        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("FOOD_INDUSTRIES")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 5 ? 0.25 : 0.0);
        }
    }

    public void CalculateFuel()
    {
        double expertBoost = ExpertBoost(basePlan.FuelExperts);
        
        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("FUEL_REFINING"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("FUEL_REFINING")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 6 ? 0.25 : 0.0);
        }
    }

    public void CalculateManufacturing()
    {
        double expertBoost = ExpertBoost(basePlan.ManufacturingExperts);
        
        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("MANUFACTURING"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("MANUFACTURING")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 7 ? 0.25 : 0.0);
        }
    }

    public void CalculateMetallurgy()
    {
        double expertBoost = ExpertBoost(basePlan.MetallurgyExperts);
        
        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("METALLURGY"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("METALLURGY")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 8 ? 0.25 : 0.0);
        }
    }

    public void CalculateExtraction()
    {
        double expertBoost = ExpertBoost(basePlan.FoodExperts);
        
        if (!basePlan.Buildings.Any(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("RESOURCE_EXTRACTION"))) return;

        foreach (var building in basePlan.Buildings.Where(x => x.BuildingTicker != "CM" && x.Building.Expertise.Equals("RESOURCE_EXTRACTION")))
        {
            building.Efficiency = 1.0 + expertBoost + (basePlan.CogcIndustry == 9 ? 0.25 : 0.0);
        }
    }

    private double ExpertBoost(int experts)
    {
        return experts switch
        {
            1 => 0.0306,
            2 => 0.0696,
            3 => 0.1248,
            4 => 0.1974,
            5 => 0.284,
            _ => 0
        };
    }
    #endregion
}

public class ProductionItem {
    public Material Material { get; set; }
    public double Amount { get; set; }
    public bool IsInput { get; set; }
    public Recipe Recipe { get; set; }
    
    public double Price { get; set; }
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace NotAnotherBasePlanner.Data;

public class Recipe
{
    [JsonIgnore]
    public int RecipeId { get; set; }
    public string BuildingTicker { get; set; }
    public string RecipeName { get; set; }
    [JsonConverter(typeof(IOToStringArrayConverter))]
    public string[] Inputs { get; set; }
    [JsonConverter(typeof(IOToStringArrayConverter))]
    public string[] Outputs { get; set; }
    public int TimeMs { get; set; }
    public Building Building { get; set; }
}

public class IOToStringArrayConverter : JsonConverter<string[]>
{
    public override string[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        List<string> inputList = new List<string>();

        foreach (JsonElement element in jsonDoc.RootElement.EnumerateArray())
        {
            IOItem ioItem = (IOItem)element.Deserialize(typeof(IOItem));
            inputList.Add(ioItem.Ticker);
        }

        return inputList.ToArray();
    }

    public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

internal class IOItem
{
    public string Ticker { get; set; }
    public int Amount { get; set; }
}
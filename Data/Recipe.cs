using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotAnotherBasePlanner.Data;

public class Recipe
{
	[JsonIgnore] public int RecipeId { get; set; }

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
		using var jsonDoc   = JsonDocument.ParseValue(ref reader);
		var       inputList = new List<string>();

		foreach (var element in jsonDoc.RootElement.EnumerateArray())
		{
			var ioItem = (IOItem)element.Deserialize(typeof(IOItem));
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
}
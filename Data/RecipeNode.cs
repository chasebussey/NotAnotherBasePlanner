namespace NotAnotherBasePlanner.Data;

public class RecipeNode
{
	public RecipeNode(Recipe recipe)
	{
		Recipe   = recipe;
		Children = new List<RecipeNode>();
	}

	public Recipe Recipe { get; set; }
	public List<RecipeNode> Children { get; set; }

	public void AddChild(Recipe child)
	{
		Children.Add(new RecipeNode(child));
	}

	public RecipeNode GetChild(int i)
	{
		foreach (var child in Children)
			if (--i == 0)
				return child;
		return null;
	}

	public void Traverse(RecipeNode node, Action<Recipe> visitor)
	{
		visitor(node.Recipe);
		foreach (var child in node.Children) Traverse(child, visitor);
	}
}
namespace NotAnotherBasePlanner.Data;
public class RecipeNode
{
    public Recipe Recipe { get; set; }
    public List<RecipeNode> Children { get; set; }

    public RecipeNode(Recipe recipe)
    {
        this.Recipe = recipe;
        Children = new List<RecipeNode>();
    }

    public void AddChild(Recipe child)
    {
        Children.Add(new RecipeNode(child));
    }

    public RecipeNode GetChild(int i)
    {
        foreach (RecipeNode child in Children)
        {
            if (--i == 0)
            {
                return child;
            }
        }
        return null;
    }

    public void Traverse(RecipeNode node, Action<Recipe> visitor)
    {
        visitor(node.Recipe);
        foreach (RecipeNode child in node.Children)
        {
            Traverse(child, visitor);
        }
    }
}
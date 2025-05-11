using Food;
using GameData;
public class Tree : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Tree;
    }

    public override bool Interact()
    {
        if (!IsRipe)
        {
            return false;
        }

        GameDataManager.AddTree();
        Destroy(gameObject);

        return true;
    }
}
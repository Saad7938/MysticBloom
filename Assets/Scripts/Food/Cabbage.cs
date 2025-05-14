using Food;
using GameData;

public class Cabbage : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Cabbage;
    }

    public override bool Interact()
    {
        if (!IsRipe)
        {
            return false;
        }
        GameDataManager.AddCabbage();
        Destroy(gameObject);
        return true;
    }
}
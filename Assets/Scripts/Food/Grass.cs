using Food;
using GameData;

public class Grass : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Grass;
    }

    public override bool Interact()
    {
        if (!IsRipe)
        {
            return false;
        }
        GameDataManager.AddGrass();
        Destroy(gameObject);
        return true;
    }
}
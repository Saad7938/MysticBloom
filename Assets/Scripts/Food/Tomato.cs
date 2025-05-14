using Food;
using GameData;
public class Tomato : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Tomato;
    }

    public override bool Interact()
    {
        if (!IsRipe)
        {
            return false;
        }

        GameDataManager.AddTomato();
        Destroy(gameObject);

        return true;
    }
}
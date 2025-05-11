using UnityEngine;
using GameData;
using Farm.Audio;
public class TrigerSellPanel : MonoBehaviour
{
    [SerializeField] private SellPlantsPanel sellPlantsPanel;
    private AudioManager _audioManager;
    void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        if (sellPlantsPanel != null)
        {
            sellPlantsPanel.sell1CarrotButton.onClick.AddListener(() =>
             {
                 sell1CarrotButtonClicked();
             });
            sellPlantsPanel.sell1CabbageButton.onClick.AddListener(() =>
            {
                sell1CabbageButtonClicked();
            });
            sellPlantsPanel.sell1TomatoButton.onClick.AddListener(() =>
            {
                sell1TomatoButtonClicked();
            });
            sellPlantsPanel.returnButton.onClick.AddListener(() =>
            {
                MakePanelVisibleFalse();
            });
        }
        else
        {
            Debug.LogError("WaterWindow not found in the scene.");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void sellBundleButtonClicked()
    {
       
        Debug.Log("sellBundleButtonClicked");

        Transform[] slots = sellPlantsPanel.carrotSlots;

        if (slots.Length < 3)
        {
            Debug.LogWarning("Not enough slots defined!");
          
            return;
        }

        int carrotCount = 0, cabbageCount = 0, tomatoCount = 0;

        foreach (Transform slot in slots)
        {
            foreach (Transform child in slot)
            {
                DragDrop dragDrop = child.GetComponent<DragDrop>();
                if (dragDrop != null)
                {
                    string tag = child.tag;

                    if (tag == "Carrot") carrotCount++;
                    else if (tag == "Cabbage") cabbageCount++;
                    else if (tag == "Tomato") tomatoCount++;

                    break;
                }
            }
        }

        Debug.Log($"Carrot: {carrotCount}, Cabbage: {cabbageCount}, Tomato: {tomatoCount}");

        int totalItems = carrotCount + cabbageCount + tomatoCount;
        if (totalItems < 3)
        {
            Debug.LogWarning("You need to place 3 vegetables to sell a bundle.");
     
            return;
        }

        int coinReward = 0;

        if (carrotCount == 3 || cabbageCount == 3 || tomatoCount == 3)
        {
            coinReward = 5;
        }
        else if (carrotCount == 1 && cabbageCount == 1 && tomatoCount == 1)
        {
            coinReward = 10;
        }
        else
        {
            coinReward = 7;
        }

        GameDataManager.AddCoin(coinReward);
        _audioManager.playSFX(_audioManager.Coins_Gained);
        Debug.Log($"Bundle sold! You earned {coinReward} coins.");

        foreach (Transform slot in slots)
        {
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }
    }

    public void sell1CarrotButtonClicked()
    {
        GameDataManager.ReduceCarrot();
        GameDataManager.AddCoin(1);
        _audioManager.playSFX(_audioManager.Coins_Gained);

    }
    public void sell1CabbageButtonClicked()
    {
        GameDataManager.ReduceGrass();
        GameDataManager.AddCoin(1);
        _audioManager.playSFX(_audioManager.Coins_Gained);

    }
    public void sell1TomatoButtonClicked()
    {
        GameDataManager.ReduceTree();
        GameDataManager.AddCoin(1);
        _audioManager.playSFX(_audioManager.Coins_Gained);
    }
    public void MakeSellPlantsPanelVisible()
    {
        sellPlantsPanel.gameObject.SetActive(true);
    }
    public void MakePanelVisibleFalse()
    {
        sellPlantsPanel.gameObject.SetActive(false);
    }
}

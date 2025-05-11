using UnityEngine;

public class TrigerSellPanel : MonoBehaviour
{
    [SerializeField] private SellPlantsPanel sellPlantsPanel;
    void Start()
    {
        if (sellPlantsPanel != null)
        {
           sellPlantsPanel.sell1Button.onClick.AddListener(() =>
            {
                sell1ButtonClicked();
            });
            sellPlantsPanel.sellBundleButton.onClick.AddListener(() =>
            {
                sellBundleButtonClicked();
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
    }
    public void sell1ButtonClicked(){
        Debug.Log("sell1ButtonClicked");
    }
    public void MakeSellPlantsPanelVisible()
    {
        sellPlantsPanel.gameObject.SetActive(true);
    }
}

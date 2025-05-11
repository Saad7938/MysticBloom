using UnityEngine;
using GameData;

public class TrigerWaterPanel : MonoBehaviour
{
    [SerializeField] private WaterWindow waterPanel;

    void Start()
    {
        if (waterPanel == null)
        {
            waterPanel = FindObjectOfType<WaterWindow>();
        }
        if (waterPanel != null)
        {
            waterPanel.OkButton.onClick.AddListener(OkClicked);
            waterPanel.FullButton.onClick.AddListener(FillWaterBucket);
            waterPanel.LiterButton.onClick.AddListener(FillLiterInBucket);
        }
        else
        {
            Debug.LogError("WaterWindow not found in the scene.");
        }
    }

    public void MakeWaterPanelVisible()
    {
        waterPanel.gameObject.SetActive(true);
    }

    void OkClicked()
    {
        waterPanel.gameObject.SetActive(false);
    }

    void FillWaterBucket()
    {
        if(GameDataManager.ReduceCoin(999))
            GameDataManager.AddWater(9999);
    }

    void FillLiterInBucket()
    { 
        if(GameDataManager.ReduceCoin(2))
            GameDataManager.AddWater(1);
    }


}

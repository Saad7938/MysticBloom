using UnityEngine;

public class OptionsManger : MonoBehaviour
{
    [SerializeField] private GameObject OptionsPanel;

    public void closePanel()
    {
        OptionsPanel.SetActive(false);
    }
}

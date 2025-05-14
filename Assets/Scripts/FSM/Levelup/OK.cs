using UnityEngine;

public class OK : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel;

    public void LevelUp()
    {
        levelUpPanel.SetActive(false);
    }
}

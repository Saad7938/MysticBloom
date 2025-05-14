using UnityEngine;

public class LevelUpControler : MonoBehaviour
{
    [SerializeField] public GameObject levelUpPanel;
    [SerializeField] private Insect flyIns;
    [SerializeField] private Insect nightIns;

    public void levelUpGame(){
        
        flyIns.speed += 1f;
        flyIns.damagePerSecond += 10;
        flyIns.clicksToKill += 3;
        nightIns.speed += 1f;
        nightIns.damagePerSecond += 10;
        nightIns.clicksToKill += 3;
        levelUpPanel.SetActive(false);
        Debug.Log("Level Up");
    }
}

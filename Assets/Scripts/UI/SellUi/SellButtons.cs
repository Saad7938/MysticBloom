using UnityEngine;
using UnityEngine.UI;
public class SellPlantsPanel : MonoBehaviour
{

    [SerializeField] public Button sellButton;
    [SerializeField] public Button sell1CarrotButton;
    [SerializeField] public Button sell1CabbageButton;
    [SerializeField] public Button sell1TomatoButton;
    [SerializeField] public Button sellBundleButton;

    [SerializeField] public Button returnButton;
    public Transform[] carrotSlots; // Assign in inspector (3 slots)

}

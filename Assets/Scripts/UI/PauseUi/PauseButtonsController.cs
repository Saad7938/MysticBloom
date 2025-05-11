using UnityEngine;
using Farm;
public class PauseButtonsController : MonoBehaviour
{
    [SerializeField] private PauseButtons pauseButtons;
    [SerializeField] private GameObject pauseMenu;

      [SerializeField] private MaiMenuController mainMenuController; 
    void Start()
    {
        pauseButtons.returnGameButton.onClick.AddListener(() =>
        {
            ReturnGameButtonClicked();
        });
        pauseButtons.mainMenuButton.onClick.AddListener(() =>
        {
            MainMenuButtonClicked();
        });
        pauseButtons.pauseButton.onClick.AddListener(() =>
        {
            MakePauseActive();
        });
    }

    public void ReturnGameButtonClicked()
    {
        Debug.Log("Return Game Button Clicked");
        pauseMenu.SetActive(false); // Hide the pause menu
    }
    public void MainMenuButtonClicked()
    {
        Debug.Log("Main Menu Button Clicked");
        pauseMenu.SetActive(false); // Hide the pause menu
        mainMenuController.showMainMenu(); // Show the main menu
    }
    public void MakePauseActive()
    {
        pauseMenu.SetActive(true); // Show the pause menu
    }
}

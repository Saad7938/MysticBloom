using UnityEngine;

public class MaiMenuController : MonoBehaviour
{
    [SerializeField]private MainMenuButtons mainMenuButtons;
    [SerializeField]private GameObject mainMenuPanel;
  
    void Start()
    {
        mainMenuButtons.playButton.onClick.AddListener(OpenGame);
        mainMenuButtons.OptionsButton.onClick.AddListener(OpenOptionsPanel);
        mainMenuButtons.QuitButton.onClick.AddListener(QuitGame);
    }

    public void OpenGame()
    {
        Debug.Log("Play Button Clicked");
        mainMenuPanel.SetActive(false);
    }

    public void showMainMenu()
    {
        Debug.Log("Show Main Menu");
        mainMenuPanel.SetActive(true);
    }
    public static void OpenOptionsPanel()
    {
        Debug.Log("Options Button Clicked");
        // Implement your options panel logic here
    }
    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

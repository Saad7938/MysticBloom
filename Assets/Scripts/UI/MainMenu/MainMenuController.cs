using UnityEngine;

public class MaiMenuController : MonoBehaviour
{
    [SerializeField]private MainMenuButtons mainMenuButtons;
    [SerializeField]private GameObject mainMenuPanel;
    [SerializeField]private GameObject optionsPanel;
  
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
    {//mk;ml;kmk
        Debug.Log("Show Main Menu");
        mainMenuPanel.SetActive(true);
    }
    public void OpenOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

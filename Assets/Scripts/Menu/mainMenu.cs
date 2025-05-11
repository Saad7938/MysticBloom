using UnityEngine;
using UnityEngine.SceneManagement;

namespace Farm
{
    public class mainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            // Hide menu UI first
            UIManager.GetInstance()?.HideMainUI();
            
            // Load scene and show main UI when done
            SceneManager.LoadSceneAsync(1).completed += (op) => 
            {
                UIManager.GetInstance()?.ShowMainUI();
            };
        }

        public void QuitGame()
        {
            Application.Quit(); 
        }
    }
}
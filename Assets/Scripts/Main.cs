using UnityEngine;

namespace Farm
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] public GameObject mainUI;
        private static UIManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Optional: if you want it persistent
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            if (mainUI != null)
                mainUI.SetActive(false);
        }

        public void ShowMainUI()
        {
            if (mainUI != null)
                mainUI.SetActive(true);
        }


        public void HideMainUI()
        {
            if (mainUI != null)
                mainUI.SetActive(false);
        }

        public static UIManager GetInstance()
        {
            return instance;
        }
    }
}
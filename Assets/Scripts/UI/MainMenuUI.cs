using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SS.FallUp.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("Setting Panel")]
        [SerializeField] private GameObject settingsPanel;

        [Header("Credits Panel")]
        [SerializeField] private GameObject creditsPanel;

        #region Main Menu Buttons
        public void OnPlayButton()
        {
            SceneManager.LoadScene("MainGame");
        }

        public void OnSettingsButton()
        {
            settingsPanel.SetActive(true);
        }

        public void OnQuitButton()
        {
            Application.Quit();
        }

        #endregion

        #region Settings Button

        public void OnCloseButton()
        {
            settingsPanel.SetActive(false); 
        }

        #endregion

        #region Credits Panel Buttons
        public void OnCreditsButton()
        {
            creditsPanel.SetActive(!creditsPanel.activeSelf);
        }

        public void OnCreditsCloseButton()
        {
            creditsPanel.SetActive(true);
        }

        #endregion
    }
}

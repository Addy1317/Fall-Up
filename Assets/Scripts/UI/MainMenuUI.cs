using SS.FallUp.Services;
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
            GameService.Instance.gameManager.ResumeGame();
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
            //creditsPanel.SetActive(!creditsPanel.activeSelf);
            creditsPanel.SetActive(true);
        }

        public void OnCreditsCloseButton()
        {
            creditsPanel.SetActive(false);
        }

        #endregion
    }
}

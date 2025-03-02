using SS.FallUp.Audio;
using SS.FallUp.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SS.FallUp.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("Setting Panel")]
        [SerializeField] private GameObject settingsPanel;

        [Header("Credits Panel")]
        [SerializeField] private GameObject creditsPanel;

        [Header("Audio Object")]
        [SerializeField] private AudioManager audioManager;

        #region Main Menu Buttons
        public void OnPlayButton()
        {
            SceneManager.LoadScene("MainGame");
            GameService.Instance.gameManager.ResumeGame();
            audioManager.PlaySFX(SFXType.ButtonClick);
        }

        public void OnSettingsButton()
        {
            settingsPanel.SetActive(true);
            audioManager.PlaySFX(SFXType.ButtonClick);
        }

        public void OnQuitButton()
        {
            Application.Quit();
            audioManager.PlaySFX(SFXType.ButtonClick);
        }

        #endregion

        #region Settings Button

        public void OnCloseButton()
        {
            settingsPanel.SetActive(false);
            audioManager.PlaySFX(SFXType.ButtonClick);
        }

        #endregion

        #region Credits Panel Buttons
        public void OnCreditsButton()
        {
            //creditsPanel.SetActive(!creditsPanel.activeSelf);
            audioManager.PlaySFX(SFXType.ButtonClick);
            creditsPanel.SetActive(true);
        }

        public void OnCreditsCloseButton()
        {
            audioManager.PlaySFX(SFXType.ButtonClick);
            creditsPanel.SetActive(false);
        }

        #endregion
    }
}

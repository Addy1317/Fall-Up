using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SS.FallUp
{
    public class MainGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;

        [SerializeField] private GameObject settingsPanel;

        #region Pause Button

        public void OnPauseButton()
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
        #endregion

        #region Pause Panel Button
        public void OnResumeButton()
        {
            pausePanel.SetActive(false);
        }

        public void OnRestartButton()
        {
            SceneManager.LoadScene("MainMenu");
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

        #region Setting Panel Button
        public void OnCloseButton()
        {
            settingsPanel.SetActive(false);
        }
        #endregion

    }
}

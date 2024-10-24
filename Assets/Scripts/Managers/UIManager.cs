using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using SS.FallUp.Services;
using SS.FallUp.GameOver;

namespace SS.FallUp.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;

        [SerializeField] private GameObject settingsPanel;

        [SerializeField] private GameObject gameOverPanel;

        [SerializeField] private GameOverPanel GameOverPanel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                TriggerGameOver();
            }
        }

        // Method to handle game over
        private void TriggerGameOver()
        {
            gameOverPanel.SetActive(true);
            OnGameOver();
        }

        #region Pause Button
        public void OnPauseButton()
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            GameService.Instance.pauseManager.PauseGame();
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

        public void OnPauseCloseButton()
        {
            pausePanel.SetActive(false);
            GameService.Instance.pauseManager.ResumeGame();
        }

        #endregion

        #region Setting Panel Button
        public void OnSettingsCloseButton()
        {
            settingsPanel.SetActive(false);
        }
        #endregion

        #region GameOver Panel
        internal void OnGameOver()
        {
            //gameOverPanel.SetActive(true);

            GameOverPanel.DisplayGameOverInfo();
            Debug.Log("GameOver Panel: " + GameOverPanel.gameObject.name);
            GameService.Instance.pauseManager.PauseGame();
        }
        #endregion
    }
}

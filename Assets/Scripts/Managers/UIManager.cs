using UnityEngine;
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

        private void OnEnable()
        {
            if (GameService.Instance == null)
            {
                Debug.LogError("GameService instance is null.");
                return;
            }

            if (GameService.Instance.eventManager == null)
            {
                Debug.LogError("EventManager is not initialized in GameService.");
                return;
            }

            GameService.Instance.eventManager.OnPlayerDeathEvent.AddListener(OnPlayerDeath);
        }

        private void OnDisable()
        {
            GameService.Instance.eventManager.OnPlayerDeathEvent.RemoveListener(OnPlayerDeath);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                OnPlayerDeath();    
            }
        }

        #region Pause Button
        public void OnPauseButton()
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            GameService.Instance.gameManager.PauseGame();
        }
        #endregion

        #region Pause Panel Button
        public void OnResumeButton()
        {
            pausePanel.SetActive(false);
            GameService.Instance.gameManager.ResumeGame();
        }

        public void OnRestartButton()
        {
            SceneManager.LoadScene("MainMenu");
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

        public void OnPauseCloseButton()
        {
            pausePanel.SetActive(false);
            GameService.Instance.gameManager.ResumeGame();
        }

        #endregion

        #region Setting Panel Button
        public void OnSettingsCloseButton()
        {
            settingsPanel.SetActive(false);
        }
        #endregion

        #region GameOver Panel
        internal void OnPlayerDeath()
        {      
            gameOverPanel.SetActive(true);
            OnGameOver();
            Debug.Log("Game Over! Player has died.");
        }

        internal void OnGameOver()
        {
            GameOverPanel.DisplayGameOverInfo();
            Debug.Log("GameOver Panel: " + GameOverPanel.gameObject.name);
            //GameService.Instance.gameManager.PauseGame();
        }
        #endregion
    }
}

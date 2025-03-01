using UnityEngine;

namespace SS.FallUp.MainManager
{
    public class GameManager : MonoBehaviour
    {
        private bool isPaused = false;  

        public void TogglePause()
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;  
            isPaused = true;
            Debug.Log("Game Paused");
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;  
            isPaused = false;
            Debug.Log("Game Resumed");
        }

        // Optional: Direct method to check pause state
        public bool IsGamePaused()
        {
            return isPaused;
        }
    }
}

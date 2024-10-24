using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp.Pause
{
    public class PauseManager : MonoBehaviour
    {
        private bool isPaused = false;  

        // Method to toggle pause/resume
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

        // Method to pause the game
        public void PauseGame()
        {
            Time.timeScale = 0f;  
            isPaused = true;
            Debug.Log("Game Paused");
        }

        // Method to resume the game
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

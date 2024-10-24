using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SS.FallUp.Timer
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI gameOverTimeText;

        private float elapsedTime = 0f;
        private bool isTimerRunning = false;

        private void Start()
        {
            StartTimer();
        }

        void Update()
        {
            if (isTimerRunning)
            {
                UpdateTimer();
            }
        }

        // Start the timer
        public void StartTimer()
        {
            isTimerRunning = true;
            Debug.Log("Timer started.");
        }

        // Update the timer
        private void UpdateTimer()
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time by frame time
            DisplayTimer(); // Update the UI display
            Debug.Log($"Elapsed Time: {GetFormattedDetailedTime()}");
        }

        // Stop the timer
        public void StopTimer()
        {
            isTimerRunning = false;
            Debug.Log("Timer stopped.");
        }

        // Format and display the timer
        private void DisplayTimer()
        {
            timerText.text = GetFormattedDetailedTime(); // Update timer display
        }

        // Get the time formatted as MM-SS
        public string GetFormattedTime()
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            return string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }

        // Get the time formatted as MM-SS-SSS
        private string GetFormattedDetailedTime()
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime - Mathf.Floor(elapsedTime)) * 1000);
            return string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
        }

        // Update the GameOverPanel with total time survived
        public void UpdateGameOverPanel()
        {
            string formattedTime = GetFormattedTime();
            gameOverTimeText.text = formattedTime; // Set total time survived
            Debug.Log($"Game Over! Total Time Survived: {formattedTime}");
        }
    }
}


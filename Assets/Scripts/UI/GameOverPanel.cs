using SS.FallUp.Services;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using SS.FallUp.Timer;

namespace SS.FallUp.GameOver
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private TextMeshProUGUI totalTimeText;
        [SerializeField] private TimerManager timerManager;

        public void DisplayGameOverInfo()
        {
           UpdateTotalTime();
        }

        // Update the total time text
        private void UpdateTotalTime()
        {
            totalTimeText.text = "Total Time Survived: " + timerManager.GetFormattedTime(); // Set the total time survived
            Debug.Log($"Total Time Survived displayed: {totalTimeText.text}");
        }

        public void OnRePlayButton()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

using SS.FallUp.Services;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

namespace SS.FallUp.GameOver
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private TextMeshProUGUI totalTimeText;
        //[SerializeField] private TimerManager timerManager;

        public void DisplayGameOverInfo()
        {
           UpdateTotalTime();
        }

        private void UpdateTotalTime()
        {
            totalTimeText.text = "Time Survived: " + GameService.Instance.timerManager.GetFormattedTime(); 
            Debug.Log($"Total Time Survived displayed: {totalTimeText.text}");
        }

        public void OnRePlayButton()
        {
            Debug.Log("Replay Button Pressed!");
            StartCoroutine(OnGameRestartRoutine());
        }

        private IEnumerator OnGameRestartRoutine()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            yield return new WaitForSeconds(1f);
            GameService.Instance.gameManager.ResumeGame();
            Debug.Log("ReBuilding The Game");
        }
    }
}


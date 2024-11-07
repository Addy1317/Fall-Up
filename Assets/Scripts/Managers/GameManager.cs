using SS.FallUp.GameOver;
using SS.FallUp.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SS.FallUp.Mangers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }
        internal void RestartGame()
        {
            Invoke("RestartAfterTime", 0f);
        }

        private void RestartAfterTime()
        {
            SceneManager.LoadScene(1);
        }   
    }
}

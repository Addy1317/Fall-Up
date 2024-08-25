using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlowpokeStudio.FallUp.Mangers
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
            Invoke("RestartAfterTime", 2f);
        }

        private void RestartAfterTime()
        {
            SceneManager.LoadScene(1);
        }
    }
}

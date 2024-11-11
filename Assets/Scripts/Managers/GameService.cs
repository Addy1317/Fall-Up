#region Summary
///<summary>
///Service Locator for Handling Multiple Singletons
///</summary>
#endregion
using SS.FallUp.Generic;
using SS.FallUp.Audio;
using SS.FallUp.UI;
using UnityEngine;
using SS.FallUp.Coin;
using SS.FallUp.Timer;
using SS.FallUp.Event;
using SS.FallUp.Spawner;
using SS.FallUp.MainManager;

namespace SS.FallUp.Services
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [SerializeField] internal GameManager gameManager;
        [SerializeField] internal AudioManager audioManager;
        [SerializeField] internal UIManager uiManager;
        [SerializeField] internal CoinManager coinManager;
        [SerializeField] internal TimerManager timerManager;
        [SerializeField] internal SpawnManager spawnManager;
        [SerializeField] internal EventManager eventManager;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            InitializeServices();
        }

        private void InitializeServices()
        {

            if (audioManager == null)
            {
                Debug.LogError("AudioManager failed to initialize.");
            }

            if (coinManager == null)
            {
                Debug.LogError("coinManager failed to initialize.");
            }

            if (uiManager == null)
            {
                Debug.LogError("uiManager failed to initialize.");
            }

            if (gameManager == null)
            {
                Debug.LogError("PauseManager failed to initialize.");
            }

            if (timerManager == null)
            {
                Debug.LogError("TimeManager failed to initialize.");
            }

            if (spawnManager == null)
            {
                Debug.LogError("SpawnManager failed to initialize.");
            }

            if (eventManager == null)
            {
                Debug.LogError("EventManager failed to initialize.");
            }
        }
    }
}

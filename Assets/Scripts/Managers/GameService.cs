using SS.FallUp.Mangers;
using SS.FallUp.Generic;
using SS.FallUp.Audio;
using SS.FallUp.UI;
using SS.FallUp.Pause;
using UnityEngine;
using SS.FallUp.Coin;
using SS.FallUp.Timer;

namespace SS.FallUp.Services
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public GameManager gameManager {  get; private set; }
        public AudioManager audioManager { get; private set; }
        public CoinManager coinManager { get; private set; }      
        public UIManager uiManager { get; private set; }
        public MainMenuUI mainMenuUI { get; private set; }
        public PauseManager pauseManager { get; private set; }
        public TimerManager timerManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            } 
        }

        private void Start()
        {
            gameManager = new GameManager();
            audioManager  = new AudioManager();
            coinManager = new CoinManager();
            uiManager = new UIManager();
            mainMenuUI = new MainMenuUI();
            pauseManager = new PauseManager();
            timerManager = new TimerManager();
        }

        private void InitializeServices()
        {
            if (gameManager == null)
            {
                Debug.LogError("GameManager failed to initialize.");
            }

            if (audioManager == null)
            {
                Debug.LogError("AudioManager failed to initialize.");
            }

            if (coinManager == null)
            {
                Debug.LogError("coinManager failed to initialize.");
            }

            if(uiManager == null)
            {
                Debug.LogError("uiManager failed to initialize.");
            }

            if (mainMenuUI == null)
            {
                Debug.LogError("UIManager failed to initialize.");
            }
            
            if (pauseManager == null)
            {
                Debug.LogError("PauseManager failed to initialize.");
            }

            if (timerManager == null)
            {
                Debug.LogError("TimeManager failed to initialize.");
            }
        }
    }
}

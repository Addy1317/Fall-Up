using SS.FallUp.Mangers;
using SS.FallUp.Generic;
using SS.FallUp.Audio;
using SS.FallUp.UI;
using UnityEngine;

namespace SS.FallUp.Services
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public GameManager gameManager {  get; private set; }
        public AudioManager audioManager { get; private set; }
        public MainMenuUI uiManager { get; private set; }

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
            uiManager = new MainMenuUI();
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

            if (uiManager == null)
            {
                Debug.LogError("UIManager failed to initialize.");
            }
        }
    }
}

using SS.FallUp.Event;
using SS.FallUp.Services;
using UnityEngine;
using UnityEngine.XR;

namespace SS.FallUp.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] internal Rigidbody2D rigidbody2d;
        [SerializeField] internal PlayerSO playerData;

        public PlayerSO PlayerData => playerData;
        private PlayerInput playerInput;

        private void Awake()
        {
            if (playerData == null)
            {
                Debug.LogError("PlayerScriptableObject is not assigned.");
            }
        }

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            ChaeckIfPlayerFellOff();
        }

        private void ChaeckIfPlayerFellOff()
        {
            if (transform.position.y < playerData.deathYPosition)
            {
                HandlePlayerDeath();
            }
        }

        private void HandlePlayerDeath()
        {
            GameService.Instance.eventManager.OnPlayerDeathEvent.InvokeEvent();

            Destroy(this.gameObject);
            Debug.Log("Player has fallen off screen!");
        }

        private void OnCollisionEnter(Collision collision)
        {
           
        }
    }
}


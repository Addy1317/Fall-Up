using SS.FallUp.Player;
using SS.FallUp.Services;
using System.Collections;
using UnityEngine;

namespace SS.FallUp.Platforms
{
    public class SpikesPlatform : Platform
    {
        private int _playerLayer;

        private void Awake()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
        }
        protected override void Update()
        {
            base.Update();  
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == _playerLayer)
            {
                Debug.Log("Player hit a spike! Game Over or reset logic here.");

                StartCoroutine(PlayerDeathOnSpikesRoutine(collision.gameObject));
            }
        }

        private IEnumerator PlayerDeathOnSpikesRoutine(GameObject player)
        {
            yield return new WaitForSeconds(1);

            if (player != null)
            {
                Destroy(player); 
            }

            GameService.Instance.uiManager.OnPlayerDeath();

            yield return new WaitForSeconds(1);

            ReturnToPool();
        }

        protected override PlatformType GetPlatformType()
        {
            return PlatformType.SpikesPlatform;
        }
    }
}

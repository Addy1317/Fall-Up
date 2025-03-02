using SS.FallUp.Services;
using System.Collections;
using UnityEngine;

namespace SS.FallUp.Spikes
{
    public class TopSpikes : MonoBehaviour
    {
        private int _playerLayer;

        private void Awake()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == _playerLayer)
            {
                Debug.Log("Player hit the Top Spikes! Destroying Player and showing Game Over.");

                StartCoroutine(PlayerDeathRoutine(collision.gameObject));
            }
        }

        private IEnumerator PlayerDeathRoutine(GameObject player)
        {
            GameService.Instance.uiManager.OnPlayerDeath();

            yield return new WaitForSeconds(1);

            if (player != null)
            {
                Destroy(player);
            }
        }
    }
}

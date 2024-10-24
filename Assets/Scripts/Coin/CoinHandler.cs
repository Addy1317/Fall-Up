using SS.FallUp.Services;
using SS.FallUp.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.FallUp.Coin
{
    public class CoinHandler : MonoBehaviour
    {
        [SerializeField] private CoinSO coinData; // ScriptableObject holding coin properties
        [SerializeField] private Transform coinParent; // Parent object to hold coins in the hierarchy

        private Queue<GameObject> coinPool = new Queue<GameObject>(); // Object pool for coin instances

        private void Start()
        {
            InitializeCoinPool();
        }

        // Initialize object pool based on coinData's initial pool size
        private void InitializeCoinPool()
        {
            for (int i = 0; i < coinData.initialPoolSize; i++)
            {
                GameObject coin = Instantiate(coinData.coinPrefab, coinParent);
                coin.SetActive(false); // Deactivate coin initially
                coinPool.Enqueue(coin); // Add to the pool
            }
        }

        // Spawn a coin at a specific position
        public void SpawnCoin(Vector3 position)
        {
            if (coinPool.Count > 0)
            {
                GameObject coin = coinPool.Dequeue();
                coin.transform.position = position;
                coin.SetActive(true);
            }
            else
            {
                // If the pool is empty, consider creating new coins if needed (optional logic)
                Debug.LogWarning("Coin pool is empty! Consider increasing the pool size.");
            }
        }

        // Return coin back to the pool after it's collected
        public void ReturnCoinToPool(GameObject coin)
        {
            coin.SetActive(false);
            coinPool.Enqueue(coin); // Re-enqueue the coin
        }

        // Spawn coins at random positions (optional for more complex spawning logic)
        public void SpawnCoinsAtRandomPositions(int numberOfCoins, Vector2 spawnAreaMin, Vector2 spawnAreaMax)
        {
            for (int i = 0; i < numberOfCoins; i++)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                    0f);

                SpawnCoin(randomPosition);
            }
        }
    }
}

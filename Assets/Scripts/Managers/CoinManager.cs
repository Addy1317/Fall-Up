using SS.FallUp.Services;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace SS.FallUp.Coin
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] private CoinSO coinSO;
        [SerializeField] private Transform coinParent;
        [SerializeField] private TextMeshProUGUI coinUIText;

        private List<GameObject> coinPool;
        public int totalCoins { get; private set; }

        private void Start()
        {
            InitializeCoinPool();
            LoadSavedCoins();
            UpdateCoinDisplay(totalCoins);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                //AddCoins(1);
                //UpdateCoinDisplay(1);

            }
        }

        private void InitializeCoinPool()
        {
            coinPool = new List<GameObject>();

            for (int i = 0; i < coinSO.initialPoolSize; i++)
            {
                GameObject coin = Instantiate(coinSO.coinPrefab, coinParent);
                coin.SetActive(false);
                coin.GetComponent<Coin>().Initialize(coinSO);
                coinPool.Add(coin);
            }
        }

        public GameObject GetCoinFromPool()
        {
            foreach (GameObject coin in coinPool)
            {
                if (!coin.activeInHierarchy)
                {
                    coin.SetActive(true);
                    return coin;
                }
            }

            // If no inactive coin, instantiate a new one
            GameObject newCoin = Instantiate(coinSO.coinPrefab, coinParent);
            newCoin.GetComponent<Coin>().Initialize(coinSO);
            coinPool.Add(newCoin);
            return newCoin;
        }

        public void ReturnCoinToPool(GameObject coin)
        {
            coin.SetActive(false);
        }

        public void HandleCoinCollection(Coin coin)
        {
            AddCoins(coinSO.coinValue);
            UpdateCoinDisplay(totalCoins);
            ReturnCoinToPool(coin.gameObject);
        }

        public void AddCoins(int amount)
        {
            totalCoins += amount;
            SaveCoins();
        }

        private void SaveCoins()
        {
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
        }

        private void LoadSavedCoins()
        {
            totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        }

        public void UpdateCoinDisplay(int coinCount)
        {
            if (coinUIText != null)
            {
                coinUIText.text = coinCount.ToString();
            }
        }

        public int GetCoinCount()
        {
            return totalCoins;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp.Coin
{
    [CreateAssetMenu(fileName = "CoinSO", menuName = "Currency/CoinSO")]
    public class CoinSO : ScriptableObject
    {
        [Header("Coin Settings")]
        public int coinValue = 1;

        [Header("Coin Pooling Settings")]
        public int initialPoolSize = 10; 
        public GameObject coinPrefab;

        [Header("Collision Detection Settings")]
        public LayerMask playerLayer;
    }
}

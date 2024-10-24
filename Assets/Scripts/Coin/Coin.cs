using SS.FallUp.Services;
using SS.FallUp.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp.Coin
{
    public class Coin : MonoBehaviour
    {
        private CoinSO coinData; 

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & coinData.playerLayer) != 0)
            {
                GameService.Instance.coinManager.HandleCoinCollection(this);
            }
        }

        public void Initialize(CoinSO coinSO)
        {
            coinData = coinSO; 
        }
    }
}

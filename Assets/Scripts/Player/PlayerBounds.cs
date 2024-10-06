using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.FallUp.Mangers;

namespace SS.FallUp.Player
{
    public class PlayerBounds : MonoBehaviour
    {
        [SerializeField] private float _minX = -2.6f;
        [SerializeField] private float _maxX = 2.6f;
        [SerializeField] private float _minY = -5.6f;

        private bool _outofBounds;

        private void Update()
        {
            
        }

        private void CheckBounds()
        {
            Vector2 temp = transform.position;

            if(temp.x > _maxX)
            {
                temp.x = _maxX;
            }

            if(temp.x < _minX)
            {
                temp.x = _minX;
            }

            transform.position = temp;

            if(temp.y <= _minY)
            {
                if(! _outofBounds)
                {
                    _outofBounds = true;
                    GameManager.instance.RestartGame();
                    SoundManager.instance.DeathSound();
                }
            }

        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            if (target.tag == "TopSpike")
            {
                transform.position = new Vector2(1000f, 1000f);
                GameManager.instance.RestartGame();
                SoundManager.instance.DeathSound();
            }
        }
    }
}

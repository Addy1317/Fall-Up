using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.FallUp.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;

        private Rigidbody2D _rigidbody2d;

        private void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            if(Input.GetAxisRaw("Horizontal") > 0f)
            {
                _rigidbody2d.velocity = new Vector2(_moveSpeed, _rigidbody2d.velocity.y);
            }

            if(Input.GetAxisRaw("Horizontal") < 0f)
            {
                _rigidbody2d.velocity = new Vector2(-_moveSpeed, _rigidbody2d.velocity.y);
            }
        }

        internal void PlatformMove(float x)
        {
            _rigidbody2d.velocity = new Vector2(x, _rigidbody2d.velocity.y);
        }
    }
}

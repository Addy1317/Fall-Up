using UnityEngine;

namespace SS.FallUp.Platforms
{
    public class LeftMovingPlatform : Platform
    {
        [SerializeField] private float slideSpeed = 2f;
        [SerializeField] private LayerMask playerLayer;

        protected override void Update()
        {
            base.Update();  
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (IsPlayer(collision.gameObject))
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    ApplySlideEffect(playerRb);
                }
            }
        }

        private void ApplySlideEffect(Rigidbody2D playerRb)
        {
            Vector2 slideVelocity = new Vector2(-slideSpeed, playerRb.linearVelocity.y);
            playerRb.linearVelocity = slideVelocity;
        }

        private bool IsPlayer(GameObject obj)
        {
            return ((1 << obj.layer) & playerLayer) != 0;
        }

        protected override PlatformType GetPlatformType()
        {
            return PlatformType.LeftMovingPlatform;
        }
    }
}

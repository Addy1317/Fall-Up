using SS.FallUp.Player;
using UnityEngine;

namespace SS.FallUp
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerController playerController;
        private float moveDirection = 0f;

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
            if (playerController == null || playerController.PlayerData == null)
            {
                Debug.LogError("PlayerController or PlayerData is missing on " + gameObject.name);
            }

            // Make sure physics is setup right
            playerController.rigidbody2d.gravityScale = 1f;
            playerController.rigidbody2d.freezeRotation = true;
        }

        private void Update()
        {
            moveDirection = Input.GetAxisRaw("Horizontal"); // -1 (Left), 0, 1 (Right)
        }

        private void FixedUpdate()
        {
            PlayerMovement();
            ClampPositionToBounds();
        }

        private void PlayerMovement()
        {
            /* float moveSpeed = playerController.playerData.moveSpeed;
             Rigidbody2D rb = playerController.rigidbody2d;

             // Apply horizontal velocity, keep gravity untouched
             Vector2 velocity = rb.linearVelocity;
             velocity.x = moveDirection * moveSpeed;
             rb.linearVelocity = velocity;*/

            Rigidbody2D rb = playerController.rigidbody2d;
            float moveSpeed = playerController.playerData.moveSpeed;

            // Preserve platform movement if no input
            Vector2 velocity = rb.linearVelocity;

            if (moveDirection != 0)
            {
                // Apply player movement ADDITIVELY (not override)
                velocity.x = moveDirection * moveSpeed * Time.fixedDeltaTime * 100f; // scaled up to feel right
            }

            rb.linearVelocity = velocity;
        }

        private void ClampPositionToBounds()
        {
            Rigidbody2D rb = playerController.rigidbody2d;
            Vector2 pos = rb.position;

            float minX = playerController.playerData.minX;
            float maxX = playerController.playerData.maxX;
            float minY = playerController.playerData.minY;

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Max(pos.y, minY);

            rb.position = pos;
        }
    }
}
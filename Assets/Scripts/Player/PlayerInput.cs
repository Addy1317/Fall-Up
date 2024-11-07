using System;
using UnityEngine;

namespace SS.FallUp.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController playerController;

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
            if (playerController == null || playerController.PlayerData == null)
            {
                Debug.LogError("PlayerController or PlayerData is missing on " + gameObject.name);
            }
        }

        private void Update()
        {
            ProcessInputs();
        }

        private void ProcessInputs()
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector3.right);
                Debug.Log("Moving Right");
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector3.left);
                Debug.Log("Moving Left");
            }
        }

        private void Move(Vector3 direction)
        {
            // Calculate movement force based on the moveSpeed in PlayerSO
            Vector2 movementForce = direction * playerController.playerData.moveSpeed * Time.deltaTime;

            // Apply the movement force
            playerController.rigidbody2d.AddForce(movementForce, ForceMode2D.Force);
        }

        private void FixedUpdate()
        {
            ClampPositionToBounds();
        }

        private void ClampPositionToBounds()
        {
            // Get the current Rigidbody2D position
            Vector2 position = playerController.rigidbody2d.position;

            // Clamp the x position to ensure it stays within bounds
            float clampedX = Mathf.Clamp(position.x, playerController.playerData.minX, playerController.playerData.maxX);

            // If player reaches bounds, stop the movement by setting x-velocity to zero
            if (position.x != clampedX)
            {
                position.x = clampedX;
                playerController.rigidbody2d.linearVelocity = new Vector2(0, playerController.rigidbody2d.linearVelocity.y);
            }

            // Clamp the y position to ensure player doesn�t fall below screen (keeping Y bound open upwards)
            float clampedY = Mathf.Clamp(position.y, playerController.playerData.minY, position.y);
            if (position.y != clampedY)
            {
                position.y = clampedY;
                playerController.rigidbody2d.linearVelocity = new Vector2(playerController.rigidbody2d.linearVelocity.x, 0);
            }

            // Update the Rigidbody2D position
            playerController.rigidbody2d.position = position;
        }
    }
}

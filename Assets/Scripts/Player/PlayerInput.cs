using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SS.FallUp.Player
{
    public class PlayerInput : MonoBehaviour
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
        }

        private void ProcessInputs()
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move();
                Debug.Log("Moving Right");
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move();
                Debug.Log("Moving Left");
            }
        }

        private void Update()
        {
            moveDirection = Input.GetAxisRaw("Horizontal"); // -1 (Left), 0 (Idle), 1 (Right)
        }

        private void FixedUpdate()
        {
            Move();
            ClampPositionToBounds();
        }

        private void Move()
        {
            /*            Vector2 movementForce = direction * playerController.playerData.moveSpeed * Time.deltaTime;
                        playerController.rigidbody2d.AddForce(movementForce, ForceMode2D.Force);*/

            if (moveDirection != 0)
            {
                float moveAmount = moveDirection * playerController.playerData.moveSpeed;
                playerController.rigidbody2d.linearVelocity = new Vector2(moveAmount, playerController.rigidbody2d.linearVelocity.y);
            }
            else
            {
                playerController.rigidbody2d.linearVelocity = new Vector2(0, playerController.rigidbody2d.linearVelocity.y);
            }
        }

        private void ClampPositionToBounds()
        {
            Vector2 position = playerController.rigidbody2d.position;

            float clampedX = Mathf.Clamp(position.x, playerController.playerData.minX, playerController.playerData.maxX);
            position.x = clampedX;
            /*if (position.x != clampedX)
            {
                position.x = clampedX;
                playerController.rigidbody2d.linearVelocity = new Vector2(0, playerController.rigidbody2d.linearVelocity.y);
            }*/

            float clampedY = Mathf.Clamp(position.y, playerController.playerData.minY, position.y);
            position.y = clampedY;
            /*if (position.y != clampedY)
            {
                position.y = clampedY;
                playerController.rigidbody2d.linearVelocity = new Vector2(playerController.rigidbody2d.linearVelocity.x, 0);
            }*/

            playerController.rigidbody2d.position = position;
        }
    }
}

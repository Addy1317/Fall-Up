using UnityEngine;

namespace SS.FallUp.Platforms
{
    public  abstract class Platform : MonoBehaviour
    {
        protected float moveSpeed;
        protected float upperBound;
        protected float lowerBound;

        private bool isActive;

        // Initialize platform variables from ScriptableObject
        public virtual void InitializeFromSO(PlatformSpawnerSO platformSpawnerSO)
        {
            if (platformSpawnerSO != null)
            {
                moveSpeed = platformSpawnerSO.moveSpeed;
                upperBound = platformSpawnerSO.spawnYPosition + 10f; 
                lowerBound = platformSpawnerSO.spawnYPosition - 5f; 
            }
            else
            {
                Debug.LogError("PlatformSpawnerSO not assigned in Platform script.");
            }
        }

        protected virtual void Update()
        {
            if (isActive)
            {
                MoveUpward();
                CheckOutOfBounds();
            }
        }

        protected virtual void MoveUpward()
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        protected virtual void CheckOutOfBounds()
        {
            if (transform.position.y > upperBound)
            {
                ReturnToPool();
            }
        }

        protected virtual void ReturnToPool()
        {
            isActive = false;
            gameObject.SetActive(false);
            Debug.Log("Platform returned to pool");
        }

        public virtual void Activate()
        {
            isActive = true;
            gameObject.SetActive(true);
        }
    }
}


using UnityEngine;

namespace SS.FallUp.Platforms
{
    public abstract class Platform : MonoBehaviour
    {
        protected float moveSpeed;
        protected float targetHeight;
        protected PlatformSpawner platformSpawner;

        public void Initialize(PlatformSpawnerSO platformSpawnerSO, PlatformSpawner spawner)
        {
            moveSpeed = platformSpawnerSO.moveSpeed;
            targetHeight = Camera.main.orthographicSize + 2f; // Moves out of screen
            platformSpawner = spawner;
        }

        public virtual void Activate()
        {
            Debug.Log($"Activating Platform: {GetPlatformType()}");
            gameObject.SetActive(true);
        }

        protected virtual void Update()
        {
            MoveUpwards();
        }

        protected virtual void MoveUpwards()
        {
            Debug.Log("Platform Moving Upwards");
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            if (transform.position.y >= targetHeight)
            {
                ReturnToPool();
            }
        }

        protected virtual void ReturnToPool()
        {
            platformSpawner.ReturnPlatform(GetPlatformType(), gameObject);
        }

        protected abstract PlatformType GetPlatformType();
    }
}

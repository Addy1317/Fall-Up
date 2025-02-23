using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace SS.FallUp.Platforms
{
    #region PlatformTypes Enum
    public enum PlatformType
    {
        StandardPlatform,
        SpikesPlatform,
        VanishingPlatform,
        RightMovingPlatform,
        LeftMovingPlatform
    }
    #endregion

    #region Platform Prefab Struct
    [System.Serializable]
    public struct PlatformPrefab
    {
        public PlatformType platformType;
        public GameObject prefab;
    }
    #endregion

    public class PlatformSpawner : MonoBehaviour
    {
        #region Serialized Fields
        [Header("Platform ScriptableObject")]
        [SerializeField] private PlatformSpawnerSO platformSpawnerSO;

        [Header("Platform Pool")]
        [SerializeField] private Transform poolParent;

        [Header("Platform Prefabs")]
        [SerializeField] private PlatformPrefab[] platformPrefabs;
        #endregion

        #region Private Variables
        private Dictionary<PlatformType, Queue<GameObject>> platformPools = new Dictionary<PlatformType, Queue<GameObject>>();
        private Dictionary<PlatformType, GameObject> platformPrefabLookup = new Dictionary<PlatformType, GameObject>();

        private const int PoolSize = 5;
        private float nextSpawnTime;
        #endregion

        private void Awake()
        {
            InitializePools();
        }

        private void Update()
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnRandomPlatform();
                nextSpawnTime = Time.time + Random.Range(platformSpawnerSO.minSpawnInterval, platformSpawnerSO.maxSpawnInterval);
            }
        }

        /// <summary>
        /// Initializes object pools for each platform type.
        /// </summary>
        private void InitializePools()
        {
            foreach (var platform in platformPrefabs)
            {
                Queue<GameObject> pool = new Queue<GameObject>();
                platformPools[platform.platformType] = pool;
                platformPrefabLookup[platform.platformType] = platform.prefab;

                for (int i = 0; i < PoolSize; i++)
                {
                    GameObject platformInstance = Instantiate(platform.prefab, poolParent);
                    platformInstance.SetActive(false);
                    pool.Enqueue(platformInstance);
                }
            }
        }

        /// <summary>
        /// Spawns a platform at a random X position.
        /// </summary>
        private void SpawnRandomPlatform()
        {
            PlatformType randomType = (PlatformType)Random.Range(0, System.Enum.GetValues(typeof(PlatformType)).Length);
            Vector3 spawnPosition = new Vector3(
                Random.Range(-platformSpawnerSO.spawnXRange, platformSpawnerSO.spawnXRange),
                platformSpawnerSO.spawnYPosition,
                0f
            );

            Quaternion spawnRotation = Quaternion.identity;
            GameObject platformObject = GetPlatform(randomType, spawnPosition, spawnRotation);

            if (platformObject.TryGetComponent(out Platform platform))
            {
                platform.Initialize(platformSpawnerSO, this);
                platform.Activate();
            }
        }

        /// <summary>
        /// Retrieves a platform from the pool.
        /// </summary>
        private GameObject GetPlatform(PlatformType platformType, Vector3 position, Quaternion rotation)
        {
            if (!platformPools.ContainsKey(platformType))
            {
                Debug.LogError($"Platform Type {platformType} not found in pool!");
                return null;
            }

            Queue<GameObject> pool = platformPools[platformType];

            GameObject platformInstance;
            if (pool.Count > 0)
            {
                platformInstance = pool.Dequeue();
            }
            else
            {
                // Pool is empty, instantiate a new one (optional, depends on design)
                platformInstance = Instantiate(platformPrefabLookup[platformType], poolParent);
            }

            platformInstance.transform.SetPositionAndRotation(position, rotation);
            platformInstance.SetActive(true);
            return platformInstance;
        }

        /// <summary>
        /// Returns a platform back to its pool.
        /// </summary>
        public void ReturnPlatform(PlatformType platformType, GameObject platform)
        {
            if (!platformPools.ContainsKey(platformType))
            {
                Debug.LogError($"Platform Type {platformType} not found in pool!");
                return;
            }

            platform.SetActive(false);
            platform.transform.SetParent(poolParent);
            platformPools[platformType].Enqueue(platform);
        }
    }
}

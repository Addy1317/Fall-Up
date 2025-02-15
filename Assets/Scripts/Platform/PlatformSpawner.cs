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
        [Header("Platform ScriptableObject")]
        [SerializeField] private PlatformSpawnerSO platformSpawnerSO;
        [SerializeField] private Transform poolParent;
        [Header("Platform Prefabs")]
        [SerializeField] private PlatformPrefab[] platformPrefabs;


        private ObjectPool<Platform> platformPool;

        private void Start()
        {
            platformPool = new ObjectPool<Platform>(CreateNewPlatform, platform => platform.gameObject.SetActive(true), platform => platform.gameObject.SetActive(false), platform => Destroy(platform.gameObject));
            StartCoroutine(SpawnPlatforms());
        }

        private IEnumerator SpawnPlatforms()
        {
            while (true)
            {
                float spawnInterval = Random.Range(platformSpawnerSO.minSpawnInterval, platformSpawnerSO.maxSpawnInterval);
                yield return new WaitForSeconds(spawnInterval);

                SpawnPlatform();
            }
        }

        private void SpawnPlatform()
        {
            /*   // Choose a random platform type from the array
               int randomIndex = Random.Range(0, platformPrefabs.Length);
               PlatformPrefab selectedPlatform = platformPrefabs[randomIndex];

               // Choose a random position within the X range
               float randomX = Random.Range(-platformSpawnerSO.spawnXRange, platformSpawnerSO.spawnXRange);
               Vector3 spawnPosition = new Vector3(randomX, platformSpawnerSO.spawnYPosition, 0);

               // Instantiate the platform at the calculated position
               Platform platform = platformPool.Get(); // Assuming platformPool is properly set up

               platform.InitializeFromSO(platformSpawnerSO); // Initialize with spawnerSO data
               platform.transform.position = spawnPosition;

               platform.gameObject.SetActive(true); // Activate the platform*/

            //========================================================================================
            int randomIndex = Random.Range(0, platformPrefabs.Length);
            PlatformPrefab selectedPlatform = platformPrefabs[randomIndex];

            float randomX = Random.Range(-platformSpawnerSO.spawnXRange, platformSpawnerSO.spawnXRange);
            Vector3 spawnPosition = new Vector3(randomX, platformSpawnerSO.spawnYPosition, 0);

            GameObject platformInstance = Instantiate(selectedPlatform.prefab, spawnPosition, Quaternion.identity);

            Platform platformScript = platformInstance.GetComponent<Platform>();
            if (platformScript != null)
            {
                platformScript.InitializeFromSO(platformSpawnerSO);  
                platformScript.Activate(); 
            }

            Debug.Log($"Spawned platform type: {selectedPlatform.platformType}");
        }

        private Platform CreateNewPlatform()
        {
            // Instantiate the platform prefab from the pool array
            int randomIndex = Random.Range(0, platformPrefabs.Length);
            PlatformPrefab selectedPlatform = platformPrefabs[randomIndex];
            GameObject platformObject = Instantiate(selectedPlatform.prefab, poolParent);
            return platformObject.GetComponent<Platform>();
        }
    }
}

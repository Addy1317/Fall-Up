using UnityEngine;

namespace SS.FallUp
{
    [CreateAssetMenu(fileName = "PlatformSpawnerSO", menuName = "Scriptable Objects/PlatformSpawnerSO")]
    public class PlatformSpawnerSO : ScriptableObject
    {
        [Header("Platform Move Speed")]
        [SerializeField] internal float moveSpeed;

        [Header("Spawn Interval Settings")]
        [SerializeField] internal float minSpawnInterval = 1f;
        [SerializeField] internal float maxSpawnInterval = 3f;

        [Header("Spawn Position Settings")]
        [SerializeField] internal float spawnXRange = 5f;      
        [SerializeField] internal float spawnYPosition = -5f;  

/*      [Header("Bounds Settings")]
        [SerializeField] internal float minXBound = -4f;   
        [SerializeField] internal float maxXBound = 4f;    
        [SerializeField] internal float upperYBound = 10f; 
        [SerializeField] internal float lowerYBound = -5f; */
    }
}


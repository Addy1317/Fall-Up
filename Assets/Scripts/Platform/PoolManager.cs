using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

namespace SS.FallUp.Platforms
{
    public class PoolManager : MonoBehaviour
    {
        private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
        public static PoolManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void CreatePool(GameObject prefab, int poolSize)
        {
            string poolKey = prefab.name;
            if (!poolDictionary.ContainsKey(poolKey))
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < poolSize; i++)
                {
                    GameObject obj = Instantiate(prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary[poolKey] = objectPool;
            }
        }

        public GameObject GetObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            string poolKey = prefab.name;

            if (poolDictionary.ContainsKey(poolKey) && poolDictionary[poolKey].Count > 0)
            {
                GameObject obj = poolDictionary[poolKey].Dequeue();
                obj.SetActive(true);
                obj.transform.position = position;
                obj.transform.rotation = rotation;

                return obj;
            }
            else
            {
                Debug.LogWarning($"No objects available in the pool for {poolKey}, creating a new instance.");
                return Instantiate(prefab, position, rotation);
            }
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            string poolKey = obj.name.Replace("(Clone)", "").Trim();

            if (poolDictionary.ContainsKey(poolKey))
            {
                poolDictionary[poolKey].Enqueue(obj);
            }
            else
            {
                Debug.LogError($"No pool found for {poolKey}. The object will be destroyed.");
                Destroy(obj);
            }
        }
    }
}

using System;
using UnityEngine;

namespace SS.FallUp.Platform
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _platformPrefab;
        [SerializeField] private GameObject _spikePlatformPrefab;
        [SerializeField] private GameObject[] _movingPlatformPrefab;
        [SerializeField] private GameObject _breakablePlatformPrefab;

        [SerializeField] private float _platformSpawnTimer = 2f;

        [SerializeField] private float _minX = -2f;
        [SerializeField] private float _maxX = 2f;

        private float _currentPlatformSpawnTimer;
        private int _platformSpawnCount;

        // Start is called before the first frame update
        void Start()
        {
            _currentPlatformSpawnTimer = _platformSpawnTimer;
        }

        // Update is called once per frame
        void Update()
        {
            SpawnPlatform();
        }

        private void SpawnPlatform()
        {
            _currentPlatformSpawnTimer += Time.deltaTime;

            if(_currentPlatformSpawnTimer >= _platformSpawnTimer)
            {
                _platformSpawnCount++;

                Vector3 temp = transform.position;
                temp.x = UnityEngine.Random.Range(_minX, _maxX);

                GameObject newPlatform = null;
                if (_platformSpawnCount < 2)
                {
                    newPlatform = Instantiate(_platformPrefab, temp, Quaternion.identity);
                }
                else if (_platformSpawnCount == 2)
                {
                    if (UnityEngine.Random.Range(0, 2) > 0)
                    {
                        newPlatform = Instantiate(_platformPrefab, temp, Quaternion.identity);
                    }
                    else
                    {
                        newPlatform = Instantiate(_movingPlatformPrefab[UnityEngine.Random.Range(0, _movingPlatformPrefab.Length)], temp, Quaternion.identity);
                    }
                }
                else if (_platformSpawnCount == 3) 
                {
                    if(UnityEngine.Random.Range(0, 2) > 0)
                    {
                        newPlatform = Instantiate(_platformPrefab, temp, Quaternion.identity);
                    }
                    else
                    {
                        newPlatform = Instantiate(_spikePlatformPrefab, temp, Quaternion.identity);
                    }
                }
                else if (_platformSpawnCount == 4)
                {
                    if (UnityEngine.Random.Range(0, 2) > 0)
                    {
                        newPlatform = Instantiate(_platformPrefab, temp, Quaternion.identity);
                    }
                    else
                    {
                        newPlatform = Instantiate(_breakablePlatformPrefab, temp, Quaternion.identity);
                    }
                    _platformSpawnCount = 0;
                }
                if(newPlatform )
                {
                    newPlatform.transform.parent = transform;
                }
                _currentPlatformSpawnTimer = 0f;
            }
        }
    }
}

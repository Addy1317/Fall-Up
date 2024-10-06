using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 0.5f; 

        private Vector2 startPos;
        private float height;

        void Start()
        {
            startPos = transform.position;
            height = GetComponent<SpriteRenderer>().bounds.size.y; 
        }

        void Update()
        {
            // Move the background vertically
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, height);
            transform.position = startPos + Vector2.up * newPosition;
        }
    }
}

#region Summary
/// <summary>
///  
/// </summary>
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.FallUp.Enviornment
{
    public class BackgroundScroll : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float _scrollSpeed = 0.3f;

        private string _mainTextue = "_MainTex";

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            HandleBackgroundScrolling();
        }

        private void HandleBackgroundScrolling()
        {
            Vector2 offset = _meshRenderer.sharedMaterial.GetTextureOffset(_mainTextue);
            offset.y += Time.deltaTime * _scrollSpeed;

            _meshRenderer.sharedMaterial.SetTextureOffset(_mainTextue, offset);
        }
    }
}

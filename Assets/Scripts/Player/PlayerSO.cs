using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerSO : ScriptableObject
    {
        [Header("Movement Settings")]
        [SerializeField] internal float moveSpeed = 5f;

        [Header("PlayerFall Position")]
        [SerializeField] internal float deathYPosition = -5.6f;

        [Header("Physics Settings")]
        [SerializeField] internal float gravityScale = 1f;

        [Header("Player Bounds")]
        [SerializeField] internal float minX = -2.6f;
        [SerializeField] internal float maxX = 2.6f;
        [SerializeField] internal float minY = -5.6f;
    }
}

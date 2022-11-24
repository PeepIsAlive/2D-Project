using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct MovableComponent
    {
        public const float MinMoveSpeed = 6f;

        public Rigidbody2D Rigidbody;
        [Range(MinMoveSpeed, 10f)] public float MoveSpeed;
    }
}

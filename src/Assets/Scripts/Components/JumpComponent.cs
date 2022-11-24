using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct JumpComponent
    {
        [Range(3f, 6f)] public float JumpForce;
    }
}

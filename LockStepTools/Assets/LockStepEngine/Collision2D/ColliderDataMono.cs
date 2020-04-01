using LockStep.Game;
using System;
using LockStep.Math;
using UnityEngine;

namespace LockStep.Collision2D
{

    public partial class ColliderData : IAfterBackup { public void OnAfterDeserialize() { } }
    public partial class CTransform2D : IAfterBackup { public void OnAfterDeserialize() { } }

    public class ColliderDataMono :UnityEngine.MonoBehaviour
    {
        public ColliderData colliderData;
    }

    public partial class ColliderData
    {
        [Header("offect")]
        public LFloat y;
        public LVector2 pos;

        [Header("Collider data")]
        public LFloat high;
        public LFloat radius;
        public LVector2 size;
        public LVector2 up;
        public LFloat deg;
    }
}
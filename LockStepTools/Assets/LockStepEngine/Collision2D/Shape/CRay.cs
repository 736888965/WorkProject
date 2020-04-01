using System;
using System.Runtime.InteropServices;
using LockStep.Math;
using LockStep.Util;

namespace LockStep.Collision2D
{
    /// <summary>
    /// 射线
    /// </summary>
    public class CRay :CBaseShape
    {
        public override int TypeId => (int)EShape2D.Ray;
        public LVector2 pos;
        public LVector2 dir;
    }

    [StructLayout(LayoutKind.Sequential, Pack = NativeHelper.STRUCT_PACK)]
    public unsafe struct Ray2D
    {
        public int TypeId => (int)EShape2D.Ray;
        public LVector2 origin;
        public LVector2 direction;
    }

    [StructLayout(LayoutKind.Sequential, Pack = NativeHelper.STRUCT_PACK)]
    public struct LRaycastHit2D
    {
        public LVector2 point;
        public LFloat distance;
        public int colliderId;

    }
}

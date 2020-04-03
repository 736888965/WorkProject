using System;
using LockStep.Math;
namespace LockStep.Collision2D
{
    /// <summary>
    /// 线段
    /// </summary>
    public class CSegment :CBaseShape
    {
        public override int TypeId => (int)EShape2D.Segment;
        public LVector2 pos1;
        public LVector2 pos2;
    }
}

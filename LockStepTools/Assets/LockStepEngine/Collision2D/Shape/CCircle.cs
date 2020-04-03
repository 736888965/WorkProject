using System;
using LockStep.Math;
namespace LockStep.Collision2D
{
    /// <summary>
    /// 圆形
    /// </summary>
    public class CCircle : CBaseShape
    {
        public override int TypeId => (int)EShape2D.Circle;
        /// <summary>
        /// 半径
        /// </summary>
        public LFloat radius;
        public CCircle():this(LFloat.zero) { }
        public CCircle (LFloat radius)
        {
            this.radius = radius;
        }
        public override string ToString()
        {
            return $"radius:{radius}";
        }
    }
}

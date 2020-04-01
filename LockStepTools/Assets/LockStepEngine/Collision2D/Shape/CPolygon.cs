using System;
using LockStep.Math;
namespace LockStep.Collision2D
{
    /// <summary>
    /// 多变性
    /// </summary>
    public class CPolygon : CCircle
    {
        public override int TypeId => (int)EShape2D.Polygon;
        /// <summary>
        /// 边的数量
        /// </summary>
        public int vertexCount;
        /// <summary>
        /// 弧度
        /// </summary>
        public LFloat deg;

        public LVector2[] vertexes;
    }
}

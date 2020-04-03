using System;
using LockStep.Math;

namespace LockStep.Collision2D
{
    public class CBaseShape
    {
        /// <summary>
        /// 物体类型
        /// </summary>
        public virtual int TypeId => (int)EShape2D.EnumCount;
        public int id;
        /// <summary>
        /// 高度
        /// </summary>
        public LFloat high;
    }
}

using LockStep.Collision2D;
using System;


namespace LockStep.Math
{
    public struct LRect
    {
        public LFloat x;
        public LFloat y;
        public LFloat xMax;
        public LFloat yMax;

        public LRect (LFloat x,LFloat y,LFloat width,LFloat heigh)
        {
            this.x = x;
            this.y = y;
            this.xMax = x + width;
            this.yMax = y + heigh;
        }

        public LRect (LVector2 position,LVector2 size)
        {
            this.x = position.x;
            this.y = position.y;
            this.xMax = x + size.x;
            this.yMax = y + size.y;
        }

        public static LRect CreateRect(LVector2 center,LVector2 halfSize)
        {
            return new LRect(center - halfSize, halfSize * 2);
        }

        public LFloat width
        {
            get => xMax - x;
            set => xMax = x + width;
        }
        public LFloat height
        {
            get => yMax - y;
            set => yMax = y + height;
        }

        public static LRect zero
        {
            get { return new LRect(LFloat.zero, LFloat.zero, LFloat.zero, LFloat.zero); }
        }

        public static LRect MinMaxRect (LFloat xmin,LFloat ymin,LFloat xmax,LFloat ymax)
        {
            return new LRect(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        public void Set(LFloat x,LFloat y,LFloat width,LFloat height)
        {
            this.x = x;
            this.y = y;
            this.xMax = x + width;
            this.yMax = y + height;
        }
        /// <summary>
        /// 位置  左下角
        /// </summary>
        public LVector2 position
        {
            get { return new LVector2(this.x, this.y); }
            set { this.x = value.x;this.y = value.y; }
        }

        /// <summary>
        /// 中心点
        /// </summary>
        public LVector2 center
        {
            get { return new LVector2((x + xMax) / 2, (y + yMax) / 2); }
            set
            {
                LFloat wid = width;
                LFloat hei = height;
                this.x = value.x - width / 2;
                this.y = value.y - height / 2;
                xMax = x + wid;
                yMax = x + hei;
            }
        }
        /// <summary>
        /// 位置  左下角
        /// </summary>
        public LVector2 min
        {
            get { return new LVector2(x, y); }
            set { x = value.x;y = value.y; }
        }

        /// <summary>
        /// 位置 右上角
        /// </summary>
        public LVector2 max
        {
            get { return new LVector2(xMax, yMax); }
            set { xMax = value.x;yMax = value.y; }
        }
        /// <summary>
        /// 矩形的长宽
        /// </summary>
        public LVector2 size
        {
            get { return new LVector2(xMax - x, yMax - y); }
            set { this.xMax = value.x + x; this.yMax = value.y + y; }
        }
        /// <summary>
        /// 矩形的长宽的一半  面积 1/4
        /// </summary>
        public LVector2 halfSizw => new LVector2(xMax - x, yMax - y) / 2;

        /// <summary>
        /// 点是否在矩形内
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(LVector2 point)
        {
            return point.x >= this.x && point.x < this.xMax && point.y >= this.y && point.y < this.yMax;
        }
        /// <summary>
        /// 点是否在矩形内
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(LVector3 point)
        {
            return point.x >= this.x && point.x < this.xMax && point.y >= this.y && point.y < this.yMax;
        }

        /// <summary>
        /// 归置rect 按大小顺序排列
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static LRect OrderMinMax(LRect rect)
        {
            if(rect.x>rect.xMax)
            {
                LFloat minx = rect.xMax;
                rect.xMax = rect.x;
                rect.x = minx;
            }
            if(rect.y>rect.yMax)
            {
                LFloat miny = rect.yMax;
                rect.yMax = rect.y;
                rect.y = miny;
            }
            return rect;
        }

        /// <summary>
        /// 两个矩形是否重合
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Overlaps(LRect other)
        {
            return other.xMax > this.x && other.x < this.xMax && other.yMax > this.y && other.y < yMax;
        }
        /// <summary>
        /// 两个矩形是否重合
        /// </summary>
        /// <param name="other"></param>
        /// <param name="allowInverse"></param>
        /// <returns></returns>
        public bool Overlaps(LRect other,bool allowInverse)
        {
            var rect = this;
            if (allowInverse)
            {
                rect = LRect.OrderMinMax(rect);
                other = LRect.OrderMinMax(other);
            }
            return rect.Overlaps(other);
        }

        public bool IntersectRay(Ray2D other, out LFloat distance)
        {
            return Utils.TestRayAABB(other.origin, other.direction, min, max, out distance);
        }
        /// <summary>
        /// 返回矩形内的一个点
        /// </summary>
        /// <param name="rectangle">矩形</param>
        /// <param name="normalizedRectCoordinates">X,Y 上的比例</param>
        /// <returns></returns>
        public static LVector2 NormalizedToPoint(LRect rectangle, LVector2 normalizedRectCoordinates)
        {
            return new LVector2(LMath.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x),
                                LMath.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
        }
        public static bool operator !=(LRect lhs, LRect rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator ==(LRect lhs, LRect rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y &&
                   lhs.xMax == rhs.xMax && lhs.yMax == rhs.yMax;
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.xMax.GetHashCode() << 2 ^ this.y.GetHashCode() >> 2 ^
                   this.yMax.GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is LRect))
                return false;
            return this.Equals((LRect)other);
        }

        public bool Equals(LRect other)
        {
            return this.x.Equals(other.x) && this.y.Equals(other.y) && this.xMax.Equals(other.xMax) &&
                   this.yMax.Equals(other.yMax);
        }
        public override string ToString()
        {
            return
                $"(x:{(object)this.x:F2}, y:{(object)this.y:F2}, width:{(object)this.xMax:F2}, height:{(object)this.yMax:F2})";
        }
    }
}

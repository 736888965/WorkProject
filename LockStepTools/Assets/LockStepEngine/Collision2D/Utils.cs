using System;
using LockStep.Math;
namespace LockStep.Collision2D
{
    public enum ECollisionEvent
    {
        Enter,
        Stay,
        Exit,
    }


    public enum EShape2D
    {
        Segment,
        Ray,
        Circle,
        AABB,
        OBB,
        Polygon,
        EnumCount,
    }

    public static unsafe  partial class Utils
    {
        public static bool TestPolygonPolygon(LVector2* _points, int vertexCount, LVector2* _points2, int vertextCount2)
        {
            return false;
        }

        /// <summary>
        /// 测试圆和多边形是否相交
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="_points"></param>
        /// <param name="vertextCount">多边形顶点数量</param>
        /// <returns></returns>
        public static bool TestCirclePolygon(LVector2 c,LFloat r,LVector2* _points,int vertextCount)
        {
            //圆形的半径平方
            var radiusSquared = r * r;
            //圆的中心点
            var circleCenter = c;
            // 记录最近的距离
            var nearestDistance = LFloat.MaxValue;
            // 记录最近的顶点
            int nearestVertex = -1;
            for (int i = 0; i < vertextCount; i++)
            {
                LVector2 axix = circleCenter - _points[i];
                // 多边形的任何顶点在圆的范围内  说明相交
                var distance = axix.sqrMagnitude - radiusSquared;
                if (distance <= 0)
                    return true;
                if(distance < nearestDistance)
                {
                    nearestVertex = i;
                    nearestDistance = distance;
                }
            }

            // 获取点
            LVector2 GetPoint(int index)
            {
                if (index < 0)
                    index += vertextCount;
                else if (index >= vertextCount)
                    index -= vertextCount;
                return _points[index];
            }

            var vertex = GetPoint(nearestVertex - 1);

            for (int i = 0; i < 2; i++)
            {
                LVector2 nextVertex = GetPoint(nearestVertex + 1);
                LVector2 edge = nextVertex - vertex;
                LFloat edgeLengthSquared = edge.sqrMagnitude;
                if (edgeLengthSquared != 0)
                {
                    LVector2 axis = circleCenter - vertex;
                    LFloat  dot = LVector2.Dot(edge, axis);
                    if(dot>=0&&dot<=edgeLengthSquared)
                    {
                        //   (dot / edgeLengthSquare ) *  edge   是求 向量的投影  
                        //  两向量 a'  b'  夹角β    
                        //  a·b = |a'||b'|cosβ  
                        //  
                        LVector2 projection = vertex + (dot / edgeLengthSquared) * edge;
                        axis = projection - circleCenter;
                        if (axis.sqrMagnitude <= radiusSquared)
                            return true;
                        else
                        {
                            if (edge.x > 0)
                                if (axis.y > 0)
                                    return false;
                            else if (edge.x < 0)
                                if (axis.y < 0)
                                    return false;
                            else if (edge.y > 0)
                                if (axis.x < 0)
                                    return false;
                            else
                                if (axis.x > 0)
                                    return false;
                        }
                    }
                }
                vertex = nextVertex;
            }
            return true;
        }
        /// <summary>
        /// 射线和多边形
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dir"></param>
        /// <param name="points"></param>
        /// <param name="vertexCount"></param>
        /// <returns></returns>
        public static bool TestRayPolygon(LVector2 o,LVector2 dir,LVector2* points,int vertexCount)
        {
            for (int i = 0; i < vertexCount; i++)
            {
                LVector2 b1 = points[i];
                LVector2 b2 = points[(i + 1) % vertexCount];
                int inter = TestRaySegment(o, dir, b1, b2);
                if (inter >= 0)
                    return true;
            }
            return false;
        }

        public static bool TestRayPolygon(LVector2 o,LVector2 dir,LVector2* points,int vertexCount,ref LVector2 point)
        {
            LFloat t = LFloat.FLT_MAX;
            for (int i = 0; i < vertexCount; i++)
            {
                LVector2 b1 = points[i];
                LVector2 b2 = points[(i + 1) % vertexCount];
                int inter = TestRaySegment(o, dir, b1, b2);
                if(inter >=0)
                    if (inter < t)
                        t = inter;
            }
            if (t < LFloat.FLT_MAX)
            {
                point = o + dir * t;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">射线的起点</param>
        /// <param name="d1">射线的终点</param>
        /// <param name="p2">多边形的顶点</param>
        /// <param name="p3">多边形相邻的顶点（和p2 组成线段）</param>
        /// <returns>返回大于等于0时  说明相交</returns>
        public static LFloat TestRaySegment(LVector2 o,LVector2 d1,LVector2 p2,LVector2 p3)
        {
            LVector2 diff = p2 - o;
            LVector2 d2 = p3 - p2;
            // 叉乘的结果如过为0  说明平行  
            LFloat dome = LMath.Cross2D(d1, d2);
            if(LMath.Abs(dome)< LFloat.EPSILON)
                return LFloat.negOne;
            LFloat t1 = LMath.Cross2D(d2, diff);
            LFloat t2 = LMath.Cross2D(d1, diff);

            if (t1 >= 0 && (t2 >= 0 && t2 <= 1))
                return t1;
            return LFloat.negOne;
        }

        public static LFloat TestSegmentSegment(LVector2 p0,LVector2 p1,LVector2 p2,LVector2 p3)
        {
            LVector2 diff = p2 - p0;
            LVector2 d1 = p1 - p0;
            LVector2 d2 = p3 - p2;

            var demo = LMath.Cross2D(d1, d2);
            if (LMath.Abs(demo) < LFloat.EPSILON) //parallel
                return LFloat.negOne;

            var t1 = LMath.Cross2D(d2, diff) / demo; // Cross2D(diff,-d2)
            var t2 = LMath.Cross2D(d1, diff) / demo; //Dot(v1,pd0) == cross(d0,d1)

            if ((t1 >= 0 && t1 <= 1) && (t2 >= 0 && t2 <= 1))
                return t1; // return p0 + (p1-p0) * t1
            return LFloat.negOne;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">射线起点</param>
        /// <param name="d">射线终点</param>
        /// <param name="min">矩形的左下角坐标</param>
        /// <param name="max">矩形的右上角坐标</param>
        /// <param name="tmin">返回距离</param>
        /// <returns></returns>
        public static bool TestRayAABB(LVector2 o,LVector2 d,LVector2 min,LVector2 max,out LFloat tmin)
        {
            tmin = LFloat.zero;
            LFloat tmax = LFloat.FLT_MAX;
            for (int i = 0; i < 2; i++)
            {
                if (LMath.Abs(d[i]) < LFloat.EPSILON)
                {
                    if (o[i] < min[i] || o[i] > max[i])
                        return false;
                }
                else
                {
                    LFloat ood = LFloat.one / d[i];
                    LFloat t1 = (min[i] - o[i]) * ood;
                    LFloat t2 = (max[i] - o[i]) * ood;
                    if (t1 > t2)
                    {
                        var temp = t1;
                        t1 = t2;
                        t2 = temp;
                    }
                    tmin = LMath.Max(tmin, t1);
                    tmax = LMath.Min(tmax, t2);
                    if (tmin > tmax) return false;
                }
            }
            return true;
        }

    }
}

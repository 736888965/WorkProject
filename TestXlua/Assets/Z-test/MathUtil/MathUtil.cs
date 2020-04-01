using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathTools
{
    public class MathUtil
    {
        public const float kPi = 3.14159265f;
        public const float k2Pi = kPi * 2;
        public const float kPiOver2 = kPi / 2;
        public const float k1OverPi = 1.0f / kPi;
        public const float k1Over2Pi = 1.0f / k2Pi;
        public static Vector3 kZeroVector = new Vector3(0,0,0);
        /// <summary>
        /// 通过适当的 k2Pi 把角度限制在 -kPi 到 kPi
        /// </summary>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static float WrapPi(float theta)
        {
            theta += kPi;
            theta -= Mathf.Floor(theta * k1OverPi) * k2Pi;
            theta -= kPi;
            return theta;
        }
        /// <summary>
        /// 安全的反三角函数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float SafeAcos(float x)
        {
            if (x <= -1.0f) return kPi;
            if (x >= 1.0f) return 0.0f;
            return Mathf.Acos(x);
        }

        public void SinCos(float theta,out float sin,out float cos)
        {
            sin = Mathf.Sin(theta);
            cos = Mathf.Cos(theta);
        }
    }
}


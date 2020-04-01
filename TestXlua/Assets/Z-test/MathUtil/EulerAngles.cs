using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathTools
{
    public class EulerAngles
    {
        public float heading;
        public float pitch;
        public float bank;

        public EulerAngles kEulerAnglesIdentity = new EulerAngles(0.0f, 0.0f, 0.0f);

        public EulerAngles() { }

        public EulerAngles(float heading, float pitch, float bank)
        {
            this.heading = heading;
            this.pitch = pitch;
            this.bank = bank;
        }


        public void identity()
        {
            heading = pitch = bank = 0.0f;
        }

        public void Canonize()
        {
            pitch = MathUtil.WrapPi(pitch);
            if (pitch < -MathUtil.kPiOver2)
            {
                pitch = -MathUtil.kPi - pitch;
                heading += MathUtil.kPi;
                bank += MathUtil.kPi;
            }
            else if (pitch > MathUtil.kPiOver2)
            {
                pitch = MathUtil.kPi - pitch;
                heading += MathUtil.kPi;
                bank += MathUtil.kPi;
            }


        }

        /// <summary>
        /// 四元数转化为欧拉角
        /// </summary>
        /// <param name="que"></param>
        public void FromObjectToInertialQuaternion(Quaternion que)
        {

        }

        public void FromInertialToObjectQuaternion(Quaternion que)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix4X4"></param>
        public void FromObjectToWorldMatrix(Matrix4x4 matrix4X4)
        {

        }
        /// <summary>
        /// 矩阵转换为欧拉角 （无平移）
        /// </summary>
        /// <param name="matrix4X4"></param>
        public void FromWorldToObjectMatrix(Matrix4x4 matrix4X4)
        {

        }
        /// <summary>
        /// 旋转矩阵到欧拉角
        /// </summary>
        private void FromRotationMatrix()
        {

        }

    }
}

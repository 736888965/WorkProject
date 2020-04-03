using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LockStep.Math
{

    public partial class Random
    {
        public ulong randSeed = 1;
        public Random(uint speed = 17)
        {
            randSeed = 17;
        }
        public LFloat value => new LFloat(true, Range(0, 1000));

        public uint Next()
        {
            randSeed = randSeed * 1103515245 + 36153;
            return (uint)(randSeed / 66536);
        }
        public uint Next(uint max)
        {
            return Next() % max;
        }
        public int Next(int max)
        {
            return (int)(Next() % max);
        }

        public uint Range(uint min, uint max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("minValue",
                    string.Format("'{0}' cannot be greater than {1}.", min, max));

            uint num = max - min;
            return this.Next(num) + min;
        }

        public int Range(int min, int max)
        {
            if (min >= max - 1)
                return min;
            int num = max - min;

            return this.Next(num) + min;
        }

        public LFloat Range(LFloat min, LFloat max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("minValue",
                    string.Format("'{0}' cannot be greater than {1}.", min, max));

            uint num = (uint)(max._val - min._val);
            return new LFloat(true, Next(num) + min._val);
        }
    }
}


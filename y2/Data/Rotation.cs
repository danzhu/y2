using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace y2.Data
{
    public struct Rotation
    {
        public int Angle;

        public Rotation(int angle)
        {
            Angle = angle;
        }

        public static implicit operator float(Rotation rot)
        {
            return rot.Angle * MathHelper.PiOver2;
        }
    }
}

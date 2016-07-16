using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace y2.Data
{
    public struct Int3
    {
        public int X;
        public int Y;
        public int Z;

        public Int3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Int3 Sign
        {
            get { return new Int3(Math.Sign(X), Math.Sign(Y), Math.Sign(Z)); }
        }

        public IEnumerable<Int3> Range(Int3 target)
        {
            var maxX = Math.Max(X, target.X);
            var maxY = Math.Max(Y, target.Y);
            var maxZ = Math.Max(Z, target.Z);

            for (var x = Math.Min(X, target.X); x <= maxX; ++x)
                for (var y = Math.Min(Y, target.Y); y <= maxY; ++y)
                    for (var z = Math.Min(Z, target.Z); z <= maxZ; ++z)
                        yield return new Int3(x, y, z);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == (Int3)obj;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return X ^ Y ^ Z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public static Int3 Zero = new Int3();
        public static Int3 Up = new Int3(0, 1, 0);
        public static Int3 Down = new Int3(0, -1, 0);

        public static bool operator==(Int3 i1, Int3 i2)
        {
            return i1.X == i2.X && i1.Y == i2.Y && i1.Z == i2.Z;
        }

        public static bool operator!=(Int3 i1, Int3 i2)
        {
            return i1.X != i2.X || i1.Y != i2.Y || i1.Z != i2.Z;
        }

        public static Int3 operator+(Int3 i1, Int3 i2)
        {
            return new Int3(i1.X + i2.X, i1.Y + i2.Y, i1.Z + i2.Z);
        }

        public static Int3 operator-(Int3 i1, Int3 i2)
        {
            return new Int3(i1.X - i2.X, i1.Y - i2.Y, i1.Z - i2.Z);
        }

        public static Int3 operator*(Int3 i, int op)
        {
            return new Int3(i.X * op, i.Y * op, i.Z * op);
        }

        public static Int3 operator/(Int3 i, int op)
        {
            return new Int3(i.X / op, i.Y / op, i.Z / op);
        }

        public static Int3 operator%(Int3 i, int op)
        {
            return new Int3(i.X % op, i.Y % op, i.Z % op);
        }

        public static implicit operator Vector3(Int3 i)
        {
            return new Vector3(i.X, i.Y, i.Z);
        }
    }
}

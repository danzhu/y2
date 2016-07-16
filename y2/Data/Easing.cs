using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace y2.Data
{
    public delegate float Easing(float x);

    public static class Easings
    {
        public static float Linear(float x) => x;

        public static float QuadIn(float x) => x * x;

        public static float QuadOut(float x) => 1 - (x - 1) * (x - 1);
    }
}

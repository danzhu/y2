using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace y2.Data
{
    /// <summary>
    /// Represents the shortest time interval for a game update to happen.
    /// </summary>
    public struct Click : IComparable<Click>
    {
        /// <summary>
        /// Represents the number of <see cref="Click"/> per second.
        /// </summary>
        public const int ClicksPerSecond = 4;
        public static readonly Click Second = new Click(ClicksPerSecond);

        public readonly int Clicks;

        public Click(int clicks)
        {
            Clicks = clicks;
        }

        public static implicit operator int(Click c) => c.Clicks / ClicksPerSecond;

        public static implicit operator float(Click c) => c.Clicks / (float)ClicksPerSecond;

        public static Click PerSecond(int times) => new Click(ClicksPerSecond / times);

        public static Click operator+(Click c1, Click c2) => new Click(c1.Clicks + c2.Clicks);

        public static Click operator++(Click c) => new Click(c.Clicks + 1);

        public static Click operator*(Click c, int scale) => new Click(c.Clicks * scale);

        public static Click operator/(Click c, int scale) => new Click(c.Clicks / scale);

        public static bool operator<(Click c1, Click c2) => c1.Clicks < c2.Clicks;

        public static bool operator>(Click c1, Click c2) => c1.Clicks > c2.Clicks;

        public int CompareTo(Click other) => Clicks.CompareTo(other.Clicks);

        public override string ToString() => Clicks.ToString();
    }
}

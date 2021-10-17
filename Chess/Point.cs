using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class Point
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Point(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Point point)
        {
            x = point.x;
            y = point.y;
        }

        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Move(Point point)
        {
            Move(point.x, point.y);
        }

        public Point Unit
        {
            get
            {
                int gcd = GCD(Math.Abs(this.x), Math.Abs(this.y));
                int x = this.x == 0 ? 0 : this.x / gcd;
                int y = this.y == 0 ? 0 : this.y / gcd;

                return new Point(x, y);
            }
        }

        private int GCD(int x, int y)
        {
            if (y == 0)
                return x;
            else
                return GCD(y, x % y);
        }

        public double Abs => Math.Sqrt(x * x + y * y);

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public static bool operator <(Point a, Point b)
        {
            return a.Abs < b.Abs;
        }

        public static bool operator >(Point a, Point b)
        {
            return a.Abs > b.Abs;
        }

        public static Point operator -(Point point)
        {
            return new Point(-point.x, -point.y);
        }
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return a + -b;
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }
        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   x == point.x &&
                   y == point.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}

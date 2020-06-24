using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingstButtonClicker
{
    public class ColorTester
    {
        public ColorTester(params ColorTestPoint[] p)
        {
            Points = p;
        }

        public ColorTestPoint[] Points { get; }

        public string GetReport()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < Points.Length; i++)
            {

            }
        }
    }

    public struct ColorTestPoint
    {
        public Point Coordinates { get; }
        public string Name { get; }
    }
}

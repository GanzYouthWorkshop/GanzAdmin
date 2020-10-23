using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GanzAdmin.Components.Chart
{
    public class ChartSeries
    {
        public struct Point
        {
            public float X { get; set; }
            public float Y { get; set; }
            public string CustomLabel { get; set; }

            public Point(float y) : this(float.NaN, y, null) { }
            public Point(float x, float y) : this(x, y, null) { }
            public Point(float y, string label) : this(float.NaN, y, label) { }
            public Point(float x, float y, string label)
            {
                this.X = x;
                this.Y = y;
                this.CustomLabel = label;
            }
        }

        public ChartTypess ChartType { get; set; }
        public List<Point> Points { get; } = new List<Point>();

        public string ToJson()
        {
            List<string> points = new List<string>();
            foreach(Point p in this.Points)
            {
                string s = "{ ";
                if(p.X != float.NaN)
                {
                    s += $"x: {p.X}";
                }
                if (p.CustomLabel != null)
                {
                    s += $"indexLabel: {p.CustomLabel}";
                }

                s += $"y: {p.Y}";
            }

            return $"type: 'line', dataPoints: [ {String.Join(',', points) } ]";
        }
    }
}

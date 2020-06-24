using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Drawing;

namespace KingstButtonClicker
{
    [DataContract]
    public struct ClickPoint
    {
        public ClickPoint(ReferencedPoint c, string n)
        {
            Coordinates = c;
            Name = n;
        }
        public ClickPoint(Point p, PointReference r, string n) : this(new ReferencedPoint(p, r), n)
        { }
        public ClickPoint(int x, int y, PointReference r, string n) : this(new Point(x, y), r, n)
        { }

        [DataMember]
        public ReferencedPoint Coordinates { get; private set; }
        [DataMember]
        public string Name { get; private set; }
    }

    [DataContract]
    public class PointDatabase : List<ClickPoint>
    {
        
    }
}

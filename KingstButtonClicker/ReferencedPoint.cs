using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace KingstButtonClicker
{
    public enum PointReference
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    [DataContract]
    public class ReferencedPoint
    {
        public ReferencedPoint(Point init, PointReference r)
        {
            RawPoint = init;
            RawReference = r;
        }
        public ReferencedPoint(int x, int y, PointReference r) : this(new Point(x, y), r)
        { }

        [DataMember]
        public Point RawPoint { get; private set; }
        [DataMember]
        public PointReference RawReference { get; private set; }

        public Point GetClientPoint(PointReference r, Rectangle window)
        {
            if (r == RawReference) return RawPoint;
            return TopLeftToSpecified(ToTopLeft(window), r, window);
        }

        public Point GetDesktopPoint(Rectangle window)
        {
            Point p = GetClientPoint(PointReference.TopLeft, window);
            return new Point(p.X + window.X, p.Y + window.Y);
        }

        private Point ToTopLeft(Rectangle w)
        {
            switch (RawReference)
            {
                case PointReference.TopLeft:
                    return RawPoint;
                case PointReference.TopRight:
                    return new Point(RawPoint.X + w.Width, RawPoint.Y);
                case PointReference.BottomLeft:
                    return new Point(RawPoint.X, RawPoint.Y + w.Height);
                case PointReference.BottomRight:
                    return new Point(RawPoint.X + w.Width, RawPoint.Y + w.Height);
                default:
                    throw new ArgumentException("Point reference type is out of range!");
            }
        }

        private Point TopLeftToSpecified(Point p, PointReference r, Rectangle w)
        {
            switch (r)
            {
                case PointReference.TopLeft:
                    return p;
                case PointReference.TopRight:
                    return new Point(p.X - w.Width, p.Y);
                case PointReference.BottomLeft:
                    return new Point(p.X, p.Y - w.Height);
                case PointReference.BottomRight:
                    return new Point(p.X - w.Width, p.Y - w.Height);
                default:
                    throw new ArgumentException("Point reference type is out of range!");
            }
        }
    }
}

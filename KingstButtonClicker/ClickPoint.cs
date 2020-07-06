using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace UIAutomationTool
{
    /// <summary>
    /// Corners
    /// </summary>
    public enum PointReference
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    /// <summary>
    /// Combines name, ReferencedPoint coordinates and some methods for convenience
    /// </summary>
    [DataContract]
    public class ClickPoint
    {
        public ClickPoint(Point p, PointReference r, string n)
        {
            RawPoint = p;
            RawReference = r;
            Name = n;
        }
        public ClickPoint(int x, int y, PointReference r, string n) : this(new Point(x, y), r, n)
        { }

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Point RawPoint { get; private set; }
        [DataMember]
        public PointReference RawReference { get; private set; }

        /// <summary>
        /// We use a borderless form when we record the points, therefore we get screen coordinates right away
        /// TODO: consider getting client rectangle for the window and calculate client coordinates
        /// </summary>
        /// <param name="r">Reference corner</param>
        /// <param name="window">Target window rectangle</param>
        /// <returns></returns>
        public Point GetPoint(PointReference r, Rectangle window)
        {
            if (r == RawReference) return RawPoint;
            return TopLeftToSpecified(ToTopLeft(window), r, window);
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

    /// <summary>
    /// Simply a list of ClickPoints
    /// </summary>
    [CollectionDataContract]
    public class PointDatabase : List<ClickPoint>
    {
        
    }
}

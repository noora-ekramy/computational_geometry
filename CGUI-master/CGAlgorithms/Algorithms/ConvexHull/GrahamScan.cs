using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {
        double angle(Point p1 , Point p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            if (points.Count < 4)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    outPoints.Add(points[i]);
                }
                return;
            }

            var orderedPoints = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            outPoints.Add(orderedPoints[0]);
            orderedPoints.RemoveAt(0);

            List<KeyValuePair<Point,double>> Angles = new List<KeyValuePair<Point,double>>();
            
            for (int i=0; i < orderedPoints.Count; i++)
            {
                Angles.Add(new KeyValuePair<Point, double>(orderedPoints[i], angle(outPoints[0], orderedPoints[i])));
            }

            var orderedAngles = Angles.OrderBy(x => x.Value).ToList();

            outPoints.Add(orderedAngles[0].Key);
            orderedAngles.RemoveAt(0);

            for (int i = 0; i < orderedAngles.Count; i++)
            {
                Line l = new Line(outPoints[outPoints.Count - 2], outPoints[outPoints.Count - 1]);
                while (HelperMethods.CheckTurn(l, orderedAngles[i].Key) == Enums.TurnType.Right|| HelperMethods.CheckTurn(l, orderedAngles[i].Key) == Enums.TurnType.Colinear)
                {
                    outPoints.RemoveAt(outPoints.Count - 1);
                    if (outPoints.Count <2)
                    {
                        break;
                    }
                    else 
                    {
                        l = new Line(outPoints[outPoints.Count - 2], outPoints[outPoints.Count - 1]);
                    }

                }
                outPoints.Add(orderedAngles[i].Key);

            }
          Console.WriteLine(outPoints .Count);
        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}

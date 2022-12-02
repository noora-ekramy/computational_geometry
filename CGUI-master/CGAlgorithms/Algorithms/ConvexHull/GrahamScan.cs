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

            var orderedPoints = points.OrderBy(x => x.Y).ToList();

            Point convex_points_1;
            Point convex_points_2;

            convex_points_1 = orderedPoints[0];
            
            orderedPoints.RemoveAt(0);

            List<KeyValuePair<Point,double>> Angles = new List<KeyValuePair<Point,double>>();
            
            for (int i=0; i < orderedPoints.Count; i++)
            {
                Angles.Add(new KeyValuePair<Point, double>(orderedPoints[i],angle(convex_points_1, orderedPoints[i])));
            }

            var orderedangles = Angles.OrderBy(x => x.Value).ToList();
            convex_points_2 = orderedangles[0].Key;
            orderedangles.RemoveAt(0);

            outPoints.Add(convex_points_1);
            outPoints.Add(convex_points_2);

            while (orderedangles.Count > 0)
            {
                Line l = new Line(convex_points_1, convex_points_2);
                Point point = orderedangles[0].Key;
                outPoints.Add(point);
                orderedangles.RemoveAt(0);

                if (HelperMethods.CheckTurn(l, point) == Enums.TurnType.Left)
                {
                    convex_points_1 = convex_points_2;
                    convex_points_2 = point;
                }
                else
                {
                    while (true)
                    {
                        convex_points_1 = outPoints[outPoints.Count - 3];
                        convex_points_2 = outPoints[outPoints.Count - 1];
                        outPoints.RemoveAt(outPoints.Count - 2);

                        Line ll = new Line(outPoints[outPoints.Count - 3], outPoints[outPoints.Count - 2]);
                        Point po = outPoints[outPoints.Count - 1];
                        if (HelperMethods.CheckTurn(ll, po) == Enums.TurnType.Left)
                        {
                            break;
                        }
                    }

                }
            }

        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}

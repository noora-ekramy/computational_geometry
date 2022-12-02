using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count <= 3)
            {
                outPoints = points;
                return;
            }

            List<Point> hull = new List<Point>();
            int left_most = 0;
            int cnt = points.Count;
            for (int i = 1; i < cnt; i++)
            {
                if (points[i].X < points[left_most].X)
                {
                    left_most = i;
                }
            }

            int p = left_most;
            int q;

            do
            {
                hull.Add(points[p]);

                q = (p + 1) % cnt;

                for (int i = 0; i < cnt; i++)
                {
                    Point p12 = calculate_vector(points[p], points[i]);
                    Point p23 = calculate_vector(points[i], points[q]);
                    if (HelperMethods.CheckTurn(p12, p23) == Enums.TurnType.Left)
                    {
                        q = i;
                    }
                }
                p = q;
            } while (p != left_most);

            outPoints = hull;

        }
        private static Point calculate_vector(Point p1, Point p2)
        {
            Point v = new Point(p2.X - p1.X, p2.Y - p1.Y);
            return v;
        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}

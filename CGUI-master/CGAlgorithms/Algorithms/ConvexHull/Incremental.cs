using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {
        private int Orientation(Point a,Point b,Point c)
        {
            var vab = HelperMethods.GetVector(new Line(a, b));
            var vbc = HelperMethods.GetVector(new Line(b, c));
            var res = HelperMethods.CrossProduct(vab, vbc);
            if (res > 0)
                return 1;
            else if (res < 0)
                return -1;
            else
                return 0;
        }
        private bool IsLeftTurn(Point a, Point b, Point c)
        {
            return Orientation(a, b, c) <=0;
        }
        private bool IsRightTurn(Point a, Point b, Point c)
        {
            return Orientation(a, b, c) >= 0;
        }


        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count <= 3)
            {
                outPoints = points;
                return;
            }
            points.Sort(comp());
            //remove duplicates
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].Equals(points[i + 1]))
                {
                    points.RemoveAt(i);
                    i--;
                }
            }
            nlgnImp(points, outPoints);

        }

        private void nlgnImp(List<Point> points, List<Point> outPoints)
        {
            int[] next = new int[points.Count];
            int[] prev = new int[points.Count];
            next[0] = 1;
            next[1] = 0;
            prev[0] = 1;
            prev[1] = 0;
            for (int i = 2; i < points.Count; i++)
            {
                if (points[i].Y > points[i - 1].Y)
                {
                    next[i] = i - 1;
                    prev[i] = prev[i - 1];
                }
                else
                {
                    next[i] = next[i - 1];
                    prev[i] = i - 1;
                }
                next[prev[i]] = i;
                prev[next[i]] = i;
                while (CCW(i, prev[i], prev[prev[i]], points))
                {
                    next[prev[prev[i]]] = i;
                    prev[i] = prev[prev[i]];
                }
                while (CCW(next[next[i]], next[i], i, points))
                {
                    prev[next[next[i]]] = i;
                    next[i] = next[next[i]];
                }

            }
            outPoints.Add(points[0]);
            int k = next[0];
            while (k != 0 && outPoints.Count <= points.Count + 1)
            {
                outPoints.Add(points[k]);
                k = next[k];
            }
        }

        private bool CCW(int i, int v1, int v2, List<Point> points)
        {
            return IsLeftTurn(points[i], points[v1], points[v2]);
        }
        

        private void on2Implm(List<Point> points, List<Point> outPoints)
        {
           
            int[] next = new int[points.Count];
            int[] prev = new int[points.Count];
            next[0] = 1;
            next[1] = 0;
            prev[0] = 1;
            prev[1] = 0;
            for (int i = 2; i < points.Count; i++)
            {
                Point p = points[i];
                int upper = 0;
                int lower = 0;
                for (int j = 1; j < i; ++j)
                {
                    Point a = points[j];
                    Point b = points[j - 1];
                    if (IsLeftTurn(p, a, b) && IsLeftTurn(p, a, points[upper]))
                    {
                        upper = j;
                    }
                    else if (IsRightTurn(p, a, b) && IsRightTurn(p, a, points[lower]))
                    {
                        lower = j;

                    }
                }
                next[upper] = i;
                prev[lower] = i;
                next[i] = lower;
                prev[i] = upper;

            }
            outPoints.Add(points[0]);
            int k = next[0];
            while (k != 0 && outPoints.Count <= points.Count + 1)
            {
                outPoints.Add(points[k]);
                k = next[k];
            }
        }

        private static Comparison<Point> comp()
        {
            return (p1, p2) =>
            {
                if (p1.X == p2.X)
                    return p1.Y.CompareTo(p2.Y);
                return p1.X.CompareTo(p2.X);
            };
        }

        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }

    }
}









































//for (int i = 3; i < points.Count; i++)
//{
//    for (int j = outPoints.Count - 1; j >= 0; j--)
//    {
//        Line line1 = new Line(outPoints[j], outPoints[(j + 1) % outPoints.Count]);
//        Line line2 = new Line(outPoints[j], outPoints[(j - 1 + outPoints.Count) % outPoints.Count]);
//        if (HelperMethods.CheckTurn(line1, points[i]) == Enums.TurnType.Left 
//            &&  HelperMethods.CheckTurn(line2, points[i]) == Enums.TurnType.Right)
//        {
//            outPoints.Insert(j + 1, points[i]);
//            break;
//        }
//        else
//        {
//            if (HelperMethods.CheckTurn(line1, points[i]) == Enums.TurnType.Colinear)
//            {
//                outPoints.Insert(j + 1, points[i]);
//                break;
//            }
//            else if (HelperMethods.CheckTurn(line2, points[i]) == Enums.TurnType.Colinear)
//            {
//                outPoints.Insert(j, points[i]);
//                break;
//            }
//        }

//    }
//}
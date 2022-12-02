using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            int connectPoint = -1 , endpoint=-1;

            if (points.Count < 4)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    outPoints.Add(points[i]);
                }
                return;
            }

            for (int i = 0; i < points.Count; i++)
            {
                int F = 0;
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[i].Equals(points[j])) continue;

                    Line l = new Line(points[i], points[j]);
                    var flagF = 0;
                    var flagT = 0;
                    for (int k = 0; k < points.Count; k++)
                    {
                        if (points[k].Equals(points[i]) || points[k].Equals(points[j])) continue;

                        if (HelperMethods.CheckTurn(l, points[k]) == Enums.TurnType.Right)
                        {
                            flagT = 1;
                        }
                        if (HelperMethods.CheckTurn(l, points[k]) == Enums.TurnType.Left)
                        {
                            flagF = 1;
                        }

                        if (flagT == flagF) break;
                    }

                    if (flagF != flagT)
                    {
                        endpoint = j;
                        connectPoint = i;
                        outPoints.Add(points[j]);
                        outPoints.Add(points[i]);
                        F = 1;
                        break;
                    }
                }
                if (F==1) break;
            }

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(points[connectPoint])) continue;

                Line l = new Line(points[connectPoint], points[i]);

                var flagF = 0;
                var flagT = 0;
                
                for (int k = 0; k < points.Count; k++)
                {
                    if (points[k].Equals(points[i]) || points[k].Equals(points[connectPoint])) continue;
                   
                    if (HelperMethods.CheckTurn(l, points[k]) == Enums.TurnType.Right)
                    {
                        flagT = 1;
                    }
                    if (HelperMethods.CheckTurn(l, points[k]) == Enums.TurnType.Left)
                    {
                        flagF = 1;
                    }

                    if (flagT == flagF) break;

                }

                if (flagF != flagT)
                {
                    if (!outPoints.Contains(points[i]))
                    {
                        connectPoint = i;
                        outPoints.Add(points[i]);
                        i = 0;
                    }
                }
            }

        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}



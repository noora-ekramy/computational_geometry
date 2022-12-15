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
            if (points.Count <= 3)
            {
                outPoints = points;
                return;
            }

            for (int i = 0; i < points.Count; i++)
            {
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
                        if (!outPoints.Contains(points[j]))
                        {
                            outPoints.Add(points[j]);
                        }

                        if (!outPoints.Contains(points[i]))
                        {
                            outPoints.Add(points[i]);
                        }
                    }
                }
            }

            for (int i = 0; i < outPoints.Count; i++)
            {
                bool flag = false;
                
                for (int j = 0; j < outPoints.Count; j++)
                {
                    for (int k = 0; k < outPoints.Count; k++)
                    {
                        if (outPoints[j] == outPoints[k]) continue;
                        
                        if (outPoints[i] != outPoints[j] && outPoints[i] != outPoints[k])
                        {
                            if (HelperMethods.PointOnSegment(outPoints[i], outPoints[j], outPoints[k]))
                            {
                                flag = true;
                                outPoints.RemoveAt(i);
                                i--;
                                break;
                            }
                        }
                    }
                    if (flag) break;
                }
            }

        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}
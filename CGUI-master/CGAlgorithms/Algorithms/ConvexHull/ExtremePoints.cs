using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var flag = true;
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[j].Equals(points[i]))
                        continue;
                    for (int k = 0; k < points.Count; k++)
                    {
                        if (points[k].Equals(points[i]) || points[k].Equals(points[j]))
                            continue;
                        for (int l = 0; l < points.Count; l++)
                        {
                            if (points[l].Equals(points[i]) || points[l].Equals(points[j]) || points[l].Equals(points[k]))
                                continue;
                            var test = HelperMethods.PointInTriangle(points[i], points[l], points[j], points[k]);
                            if (test != Enums.PointInPolygon.Outside)
                            {
                                flag = false;
                                break;
                            }

                        }
                        if (!flag)
                            break;
                    }

                    if (!flag)
                        break;
                }
                if (flag && !outPoints.Contains(points[i]))
                    outPoints.Add(points[i]);
            }

        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}

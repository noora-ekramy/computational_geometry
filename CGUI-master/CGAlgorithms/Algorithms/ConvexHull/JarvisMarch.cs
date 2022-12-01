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
        static double inf = 100000000 + 7;
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if(points.Count <= 3)
            {
                outPoints = points;
                return;
            }
            Point origin = new Point(0, 0);
            double miny = inf;
            int min_point_index = -1;
           
            for(int i = 0; i < points.Count; i++ )
            {
                if (points[i].Y <= miny)
                {
                    min_point_index = i;
                    miny = points[i].Y;
                }
            }
            Point minPoint = new Point(points[min_point_index].X , points[min_point_index].Y);
            List<Point> answer = new List<Point>();
            
            do
            {
                
                answer.Add(minPoint);
                double maxangle = -inf;
                int max_point_index = -1;
                for (int i = 0; i < points.Count; i++)
                {
                    double angel = Calculate_angle(origin, minPoint, points[i])*180 /Math.PI;
                    double diffrance = ((maxangle - angel) + 180) % 360 - 180;
                    if (diffrance <= 0)
                    {
                        maxangle = angel;
                        max_point_index = i;
                         
                    }
                }
                origin = minPoint;
                if (max_point_index != -1)
                    minPoint = points[max_point_index];
                else break;

                Console.WriteLine(answer[0] +" " + minPoint);
            } while (answer[0].X != minPoint.X || answer[0].Y != minPoint.Y);

            outPoints = answer;


        }
        private static double length_of_the_segment(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }
        private static double Calculate_angle(Point p1, Point p2, Point p3)
        {
            // arccos((P122 + P132 - P232) / (2 * P12 * P13))
            double p12 = length_of_the_segment(p1, p2);
            double p13 = length_of_the_segment(p1, p3);
            double p23 = length_of_the_segment(p2, p3);
            return Math.Acos((p12 +p23-p13)/(2* (Math.Sqrt(p12)) *(Math.Sqrt(p23))));
           
          
        }
        private static double Calculate_angle2(Point p1, Point p2, Point p3)
        {
            
            return 0.1;
        }
        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}

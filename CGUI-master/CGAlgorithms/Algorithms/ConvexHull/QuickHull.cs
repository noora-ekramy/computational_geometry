using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class QuickHull : Algorithm
    {

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            hull = new List<Point>();
            if (points.Count <= 3)
            {
                outPoints= points;
                return;
            }

            int min_x = 0;
            int max_x = 0;
            int min_y = 0;
            int max_y = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < points[min_x].X)
                {
                    min_x = i;
                }
                if (points[i].X > points[max_x].X)
                {
                    max_x = i;
                }
                if (points[i].Y < points[min_y].Y)
                {
                    min_y = i;
                }
                if (points[i].Y > points[max_y].Y)
                {
                    max_y = i;
                }
            }

            hull.Add(points[min_x]);
            if(!hull.Contains(points[max_x]))
                hull.Add(points[max_x]);
            if (!hull.Contains(points[min_y]))
                hull.Add(points[min_y]);
            if (!hull.Contains(points[max_y]))
                hull.Add(points[max_y]);
            quickHull(points , points.Count, points[min_y], points[max_x], -1);
            quickHull(points, points.Count, points[max_x], points[max_y], -1);
            quickHull(points, points.Count, points[max_y], points[min_x], -1);
            quickHull(points, points.Count, points[min_x], points[min_y], -1);




            outPoints = hull;

        }
        //Calculate Distance Between Point p And Line (p1 ,p2)
        private static double Calculate_Distance_Between_Point_And_Line(Point p1 , Point p2 , Point p)
        {
            return Math.Abs( ((p.Y -p1.Y )*(p2.X - p1.X))-((p2.Y - p1.Y)*(p.X -p1.X )));
        }
        //returns sideof the point
        public static int findSide(Point p1, Point p2, Point p)
        {
            double val = (p.Y - p1.Y) * (p2.X - p1.X) - (p2.Y - p1.Y) * (p.X - p1.X);
            //left
            if (val > 0)
            {
                return 1;
            }
            //right
            if (val < 0)
            {
                return -1;
            }
            //on the line
            return 0;
        }
        public static List<Point> hull;
        public static void quickHull(List<Point> points, int n, Point p1, Point p2, int side)
        {
            int ind = -1;
            double max_dist = 0;

            for (int i = 0; i < n; i++)
            {
                double temp = Calculate_Distance_Between_Point_And_Line(p1, p2, points[i]);
                if (findSide(p1, p2, points[i]) == side && temp > max_dist){
                    ind = i;
                    max_dist = temp;
                }
            }
            if (ind == -1)
            {
                return;
            }
            hull.Add(points[ind]);
            // Recur for the two parts divided by (p1 , a[ind]) , (p2 , a[ind])
            quickHull(points, n, p1, points[ind],-1);
            quickHull(points, n, points[ind], p2,-1);
        }
       

        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}

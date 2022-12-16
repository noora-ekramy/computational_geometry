using CGUtilities;
using System.Collections.Generic;
using System.Linq;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {
        public static int Min_X(List<Point> points)
        {
            int index = 0;

            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].X < points[index].X)
                {
                    index = i;
                }
                else if (points[i].X == points[index].X)
                {
                    if (points[i].Y < points[index].Y)
                        index = i;
                }
            }
            return index;
        }
        public static int Max_X(List<Point> points)
        {
            int index = 0;

            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].X > points[index].X)
                {
                    index = i;
                }
                else if (points[i].X == points[index].X)
                {
                    if (points[i].Y > points[index].Y)
                        index = i;
                }
            }
            return index;
        }
        public List<Point> merge(List<Point> Lift, List<Point> Right)
        {
            int MLP = Max_X(Lift);
            int MRP = Min_X(Right);
            int cl = Lift.Count;
            int cr = Right.Count;
            bool found;

           
            //  upper tangent 
            int ULP = MLP;
            int URP = MRP;
            int NextLP = (ULP + 1) % cl;
            int PreRP = (cr + URP - 1) % cr;

            do
            {
                found = true;
                while (CGUtilities.HelperMethods.CheckTurn(new Line(Right[URP], Lift[ULP]), Lift[NextLP]) == Enums.TurnType.Right)
                {
                    ULP = NextLP;
                    NextLP = (ULP + 1) % cl;

                    found = false;
                }

                if (found == true && (CGUtilities.HelperMethods.CheckTurn(new Line(Right[URP], Lift[ULP]), Lift[NextLP]) == Enums.TurnType.Colinear))
                    ULP = NextLP;
                NextLP = (ULP + 1) % cl;

                while (CGUtilities.HelperMethods.CheckTurn(new Line(Lift[ULP], Right[URP]), Right[PreRP]) == Enums.TurnType.Left)
                {
                    URP = PreRP;
                    PreRP = (cr + URP - 1) % cr;

                    found = false;
                }

                if (found == true && (CGUtilities.HelperMethods.CheckTurn(new Line(Lift[ULP], Right[URP]), Right[PreRP]) == Enums.TurnType.Colinear))
                    URP = PreRP;
                PreRP = (cr + URP - 1) % cr;


            } while (found == false);



            //lower tangent
            int DLP = MLP;
            int DRP = MRP;
            int PreLP = (cl + DLP - 1) % cl;
            int NextRP = (DRP + 1) % cr;

            do
            {
                found = true;
                while (CGUtilities.HelperMethods.CheckTurn(new Line(Right[DRP], Lift[DLP]), Lift[PreLP]) == Enums.TurnType.Left)
                {
                    DLP = PreLP;
                    PreLP = (cl + DLP - 1) % cl;

                    found = false;
                }

                if (found == true && (CGUtilities.HelperMethods.CheckTurn(new Line(Right[DRP], Lift[DLP]), Lift[PreLP]) == Enums.TurnType.Colinear))
                    DLP = PreLP;
                PreLP = (cl + DLP - 1) % cl;

                while (CGUtilities.HelperMethods.CheckTurn(new Line(Lift[DLP], Right[DRP]), Right[NextRP]) == Enums.TurnType.Right)
                {
                    DRP = NextRP;
                    NextRP = (DRP + 1) % cr;

                    found = false;
                }

                if (found == true && (CGUtilities.HelperMethods.CheckTurn(new Line(Lift[DLP], Right[DRP]), Right[NextRP]) == Enums.TurnType.Colinear))
                    DRP = NextRP;
                NextRP = (DRP + 1) % cr;

            } while (found == false);





            List<Point> out_points = new List<Point>();

            int ind = ULP;
            if (!out_points.Contains(Lift[ULP]))
                out_points.Add(Lift[ULP]);

            while (ind != DLP)
            {
                ind = (ind + 1) % cl;

                if (!out_points.Contains(Lift[ind]))
                    out_points.Add(Lift[ind]);
            }

            ind = DRP;
            if (!out_points.Contains(Right[DRP]))
                out_points.Add(Right[DRP]);

            while (ind != URP)
            {
                ind = (ind + 1) % cr;
                if (!out_points.Contains(Right[ind]))
                    out_points.Add(Right[ind]);

            }

            return out_points;
        }

        public List<Point> divide(List<Point> Points)
        {


            if (Points.Count == 1)
            {
                return Points;
            }


            List<Point> L = new List<Point>();
            List<Point> R = new List<Point>();

            for (int i = 0; i < Points.Count / 2; i++) L.Add(Points[i]);
            for (int i = Points.Count / 2; i < Points.Count; i++) R.Add(Points[i]);

            List<Point> Left = divide(L);
            List<Point> Right = divide(R);

            return merge(Left, Right);
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            points = points.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
            outPoints = divide(points);
        }

        public override string ToString()
        {
            return "Convex Hull - Divide & Conquer";
        }

    }
}
using System;
using OpenTK;

namespace Terrain {
	public class Frustum {
        const float farDist = 10000.0f;

        private Vector2d A;
        private Vector2d C;
        private Vector2d D;

        public Frustum(Vector3 eye, Vector3 target, double facing)
        {
            double quadtreeCellLength = Terrain.Instance.PageSideLength * Terrain.Instance.gridSpacing;

            A = new Vector2d(eye.X, eye.Z);
            double cellHypotenuse = Math.Sqrt(Math.Pow(quadtreeCellLength, 2) * 2);

            A = new Vector2d(A.X + Math.Cos(facing - Math.PI) * cellHypotenuse,
                A.Y + Math.Sin(facing - Math.PI) * cellHypotenuse);
            double sideHypotenuse = Math.Sqrt(Math.Pow(farDist, 2) * 2);


            C = new Vector2d(A.X + Math.Cos(facing - Math.PI / 4d) * sideHypotenuse,
                A.Y + Math.Sin(facing - Math.PI / 4d) * sideHypotenuse);
            D = new Vector2d(A.X + Math.Cos(facing + Math.PI / 4d) * sideHypotenuse,
                A.Y + Math.Sin(facing + Math.PI / 4d) * sideHypotenuse);



        }

        public bool Contains(params Vector3[] points)
        {
            bool result = false;
            foreach (Vector3 point in points)
            {
                result = PointInside(A, D, C, new Vector2d(point.X, point.Z));
            }
            return result;
        }

        private bool PointInside(Vector2d a, Vector2d b, Vector2d c, Vector2d p)
        {
            double asX = p.X - a.X;
            double asY = p.Y - a.Y;
            bool pAB = (b.X - a.X) * asY - (b.Y - a.Y) * asX > 0;
            if ((c.X - a.X) * asY - (c.Y - a.Y) * asX > 0 == pAB) return false;
            if ((c.X - b.X) * (p.Y - b.Y) - (c.Y - b.Y) * (p.X - b.X) > 0 != pAB) return false;
            return true;
        }
    }
}


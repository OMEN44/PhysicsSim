using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsSim
{
    public static class VMath
    {
        //length of vector
        public static float Magnitude(Vector2f v) => MathF.Sqrt(v.X * v.X + v.Y * v.Y);
        //distance between vectors
        public static float Distance(Vector2f v1, Vector2f v2)
        {
            float dx = v1.X - v2.X;
            float dy = v1.Y - v2.Y;
            return dx * dx + dy * dy;
        }
        //the direction is maintained but the length is one, now the direction of two vectors can be compared
        public static Vector2f Normalize(Vector2f v) => new Vector2f(v.X/Magnitude(v), v.Y/Magnitude(v));
        public static float Dot(Vector2f v1, Vector2f v2) => v1.X * v2.X + v1.Y * v2.Y;
        public static float Cross(Vector2f v1, Vector2f v2) => v1.X * v2.X - v1.Y * v2.Y;
    }
}

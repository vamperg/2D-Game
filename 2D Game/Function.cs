using System;
using System.Collections.Generic;
using System.Text;

namespace _2D_Game
{
    public static class function
    {
        public static float Clamp(float value,float min, float max)
        {
            return (float)Math.Max(Math.Min(value, max), min);
        }
        public static int Clamp(int value, int min, int max)
        {
            return (int)Math.Max(Math.Min(value, max), min);
        }

        public static float Length(Vector2 vector)
        {
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y);
        }

        public static Vector3 Norm(Vector3 vector)
        {
            return vector / Vector3.Length(vector);
        }

  
        public static Vector2 Sphere(Vector3 ro, Vector3 rd, float r)
        {
            float b = ro * rd;
            float c = ro*ro - r * r;
            float h = b * b - c;
            if (h < 0.0) return new Vector2(-1, 0);
            h = (float)Math.Sqrt(h);
            return new Vector2(-b - h, -b + h);
        }

        public static float Plane(Vector3 ro, Vector3 rd, Vector3 p, float w)
        {
            return -(ro * p + w) / (rd * p);
        }

        public static Vector3 Reflect(Vector3 rd, Vector3 n)
        {
            return rd - n * (2 * (n * rd));
        }

        public static Vector3 rotateX(Vector3 a, float angle)
        {
            Vector3 b = a;
            b.z = (float)(a.z * Math.Cos(angle) - a.y * Math.Sin(angle));
            b.y = (float)(a.z * Math.Sin(angle) + a.y * Math.Cos(angle));
            return b;
        }
        public static Vector3 rotateY(Vector3 a, float angle)
        {
            Vector3 b = a;
            b.x = (float)(a.x * Math.Cos(angle) - a.z * Math.Sin(angle));
            b.z = (float)(a.x * Math.Sin(angle) + a.z * Math.Cos(angle));
            return b;
        }
        public static Vector3 rotateZ(Vector3 a, float angle)
        {
            Vector3 b = a;
            b.x = (float)(a.x * Math.Cos(angle) - a.y * Math.Sin(angle));
            b.y = (float)(a.x * Math.Sin(angle) + a.y * Math.Cos(angle));
            return b;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace _2D_Game
{
    public static class function
    {
        public static float Clamp(float value, float min, float max)
        {
            return (float)Math.Max(Math.Min(value, max), min);
        }
        public static int Clamp(int value, int min, int max)
        {
            return (int)Math.Max(Math.Min(value, max), min);
        }

        public static float Step (float a, float b){
            if (a > b)
                return 1f;
            else return 0;
        }

        public static float Length(Vector2 vector)
        {
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y);
        }

        public static Vector3 Norm(Vector3 vector)
        {
            return vector / Vector3.Length(vector);
        }

        public static Vector2 Box(Vector3 ro, Vector3 rd, float boxSize, out Vector3 outNormal)
        {
            Vector3 m = new Vector3(1f / rd.x, 1f / rd.y, 1f / rd.z);
            Vector3 n = new Vector3(m.x * ro.x, m.y * ro.y, m.z * ro.z);
            Vector3 absM = Vector3.Abs(m);
            Vector3 k = new Vector3(absM.x * boxSize, absM.y * boxSize, absM.z * boxSize);
            Vector3 t1 = n*-1 - k;
            Vector3 t2 = n * -1 + k;
            float tN = Math.Max(Math.Max(t1.x,t1.y),t1.z);
            outNormal = new Vector3(1);
            float tF = Math.Max(Math.Max(t2.x, t2.y), t2.z);
            if (tN > tF || tF < 0f) return new Vector2(-1f);    
            outNormal = new Vector3(1);
            outNormal.x = Vector3.Sign(rd).x * -1 * Vector3.Step(new Vector3(t1.y, t1.z, t1.x), new Vector3(t1.x, t1.y, t1.z)).x *Vector3.Step(new Vector3(t1.z, t1.x, t1.y), new Vector3(t1.x, t1.y, t1.z)).x;
            outNormal.y = Vector3.Sign(rd).y * -1 * Vector3.Step(new Vector3(t1.y, t1.z, t1.x), new Vector3(t1.x, t1.y, t1.z)).y * Vector3.Step(new Vector3(t1.z, t1.x, t1.y), new Vector3(t1.x, t1.y, t1.z)).y;
            outNormal.z = Vector3.Sign(rd).z * -1 * Vector3.Step(new Vector3(t1.y, t1.z, t1.x), new Vector3(t1.x, t1.y, t1.z)).z * Vector3.Step(new Vector3(t1.z, t1.x, t1.y), new Vector3(t1.x, t1.y, t1.z)).z;
            return new Vector2(tN, tF);
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
            var x = rd.x - n.x * (2 * (n * rd));
            var y = rd.y - n.y * (2 * (n * rd));
            var z = rd.z - n.z * (2 * (n * rd));
            return new Vector3(x, y, z);
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

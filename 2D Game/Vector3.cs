using System;
using System.Collections.Generic;
using System.Text;

namespace _2D_Game
{
    public class Vector3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public Vector3(float x, Vector2 b)
        {
            this.x = x;
            this.y = b.x;
            this.z = b.y;
        }
        public Vector3(Vector2 a, float z)
        {
            this.x = a.x;
            this.y = a.y;
            this.z = x;
        }
        public Vector3(float value)
        {
            this.x = value;
            this.y = value;
            this.z = value;
        }


        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3 { x = a.x + b.x, y = a.y + b.y, z = a.z + b.z };
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3 { x = a.x - b.x, y = a.y - b.y, z = a.z - b.z };
        }
        public static Vector3 operator *(Vector3 a, float scalar)
        {
            return new Vector3 { x = a.x * scalar, y = a.y * scalar, z = a.z * scalar };
        }
        public static Vector3 operator /(Vector3 a, float length)
        {
            return new Vector3 { x = a.x / length, y = a.y / length, z = a.z / length };
        }
        public static float operator *(Vector3 a, Vector3 b)
        {
            return (float)(a.x * b.x + a.y * b.y + a.z * b.z);
        }
 
        public static float Length( Vector3 a)
        {

            return (float)((Math.Pow(a.x, 2) + Math.Pow(a.y, 2) + Math.Pow(a.z, 2)));
        }
        public static float GetAngle (Vector3 a, Vector3 b)
        {
            return (float)(Math.Acos((a * b) / (Vector3.Length(a)*Vector3.Length(b))));
        }
    }
}

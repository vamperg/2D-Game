using System;
 

namespace _2D_Game
{
    public class Vector2
    {
        public float x { get;  set; }
        public float y { get;  set; }

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(float value)
        {
            this.x = value;
            this.y = value;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2 { x = a.x + b.x, y = a.y + b.y };
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2 { x = a.x - b.x, y = a.y - b.y };
        }

        public static Vector2 operator -(Vector2 a, float b)
        {
            return new Vector2 { x = a.x - b, y = a.y - b };
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2 { x = a.x * b.x, y = a.y * b.y };
        }
        public static Vector2 operator *(Vector2 a, float scalar)
        {
            return new Vector2 { x = a.x * scalar, y = a.y * scalar };
        }
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2 { x = a.x / b.x, y = a.y / b.y };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace _2D_Game
{
    public class Shape
    {
        protected Vector2 position;
        static protected char[] texture;


        public Shape(Vector2 position, char[] texture)
        {
            this.position = position;
            
        }

        public virtual char[] Draw(Graphics graphics)
        {
            return new char[graphics.width * graphics.height];
        }

        public void Move(Vector2 direction)
        {
            position += direction;
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Vector2 GetPosition()
        {
            return position;
        }


    }

    public class Circle : Shape
    {
        protected float size;

        public Circle(Vector2 position, float size) : base(position, texture)
        {
            this.size = size;
            Circle circle = this;
   
        }

        override public char[] Draw(Graphics graphics)
        {
            char[] buffer = new char[graphics.buffer.Length];
            for (int i = 0; i < graphics.width; i++)
                for (int j = 0; j < graphics.height; j++)
                {
                    float x = (float)i / graphics.width * 2.0f * graphics.multiplier - 1.0f * graphics.multiplier;
                    float y = (float)j / graphics.height * 2.0f * graphics.multiplier - 1.0f * graphics.multiplier;
                   /* float t = 1;
                    x += (float)Math.Cos(t * 0.001f) * 0.7f;
                    y += (float)Math.Sin(t * 0.001f) * 0.7f;*/

                    x *= graphics.aspect * graphics.pixelAspect;
                    float offsetPosition = (x - position.x) * (x - position.x) + (y - position.y) * (y - position.y); //смещение центра координат для обьекта

                    char pixel;
                     
                    if (offsetPosition < size)//рисуем круг до границ круга
                    {
                        float dist = (float)Math.Sqrt(x * x + y * y);
                        int color = (int)(1.0f / dist);
                        color = function.Clamp(color, 0, graphics.gradientSize);
                        pixel = graphics.gradient[color];

                        buffer[i + j * graphics.width] = pixel;
                    }
                }
            return buffer;
        }
    }

    public class SolidRectangle : Shape
    {
        protected Vector2 size;

        /// <summary>
        /// Залитый прямоугольник
        /// </summary>
        /// <param name="position">Поцизия</param>
        /// <param name="size">Поцизия</param>
        /// <param name="texture">цвет прямоугольника из 1 символа</param>
        public SolidRectangle(Vector2 position, Vector2 size, char[] texture) : base(position, texture)
        {
            this.size = size;
        }

        override public char[] Draw(Graphics graphics)
        {
            char[] buffer = new char[graphics.buffer.Length];
            for (int i = 0; i < graphics.width; i++)
                for (int j = 0; j < graphics.height; j++)
                {
                    float x = (float)i / graphics.width * 2.0f * graphics.multiplier - 1.0f * graphics.multiplier;
                    float y = (float)j / graphics.height * 2.0f * graphics.multiplier - 1.0f * graphics.multiplier;
                    /* float t = 1;
                     x += (float)Math.Cos(t * 0.001f) * 0.7f;
                     y += (float)Math.Sin(t * 0.001f) * 0.7f;*/

                    x *= graphics.aspect * graphics.pixelAspect;
                    char pixel = graphics.gradient[4];
                    if (x > position.x && x < position.x + size.x && y > position.y && y < position.y + size.y)
                        buffer[i + j * graphics.width] = pixel;
                }
            return buffer;
        }


    }

    public class Line : Shape
    {
        protected Vector2 endPosition;
        protected float k;
        protected float a;
        protected float width;
        public Line(Vector2 position, Vector2 endPosition, char[] texture) : base(position, texture)
        {
            this.endPosition = endPosition;
            this.width = 0.05f;
            k = (endPosition.y - position.y) / (endPosition.x - position.x);
            a = position.y - position.x * k;
        }
        public Line(Vector2 position, Vector2 endPosition, float width,  char[] texture) : base(position, texture)
        {
            this.endPosition = endPosition;
            this.width = width;
            k = (endPosition.y - position.y) / (endPosition.x - position.x);
            a = position.y - position.x * k;
        }

        override public char[] Draw(Graphics graphics)
        {
            char[] buffer = new char[graphics.buffer.Length];
            for (int i = 0; i < graphics.width; i++)
                for (int j = 0; j < graphics.height; j++)
                {
                    float x = (float)i / graphics.width * 2.0f * graphics.multiplier - 1.0f * graphics.multiplier;
                    float y = (float)j / graphics.height * 2.0f * graphics.multiplier - 1.0f * graphics.multiplier;
           

                    x *= graphics.aspect * graphics.pixelAspect;
                    float f = k * x + a;
                    if (y > f -width && y < f + width && x > position.x-0.3f && x < endPosition.x+0.7f)
                        buffer[i + j * graphics.width] = texture[0];
                }
            return buffer;
        }
    }

}

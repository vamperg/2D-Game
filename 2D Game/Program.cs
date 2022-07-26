using System;
using System.Threading;
namespace _2D_Game
{
    class Program
    {


        static void Main(string[] args)
        {
            float speed = 0.05f;
            
            Graphics graphics = new Graphics(221, 51, 2.5f);
            float width = graphics.height / 10;
            float height = graphics.width / 100;

            Console.SetBufferSize(221, 51);

            Circle circle = new Circle(new Vector2(0, 0), 0.1f, new char[] { '-', ':', '#', '@' });
            SolidRectangle rect = new SolidRectangle(new Vector2(1, -1f), new Vector2(1,2), new char[] { '=' });
            SolidRectangle rect2 = new SolidRectangle(new Vector2(-1f, -1.5f), new Vector2(2, 1), new char[] { '=' });
            SolidRectangle rect3 = new SolidRectangle(new Vector2(-1f, 1.5f), new Vector2(2, 1), new char[] { '=' });
            SolidRectangle rect4 = new SolidRectangle(new Vector2(-1.8f, -1.5f), new Vector2(0.5f,3f), new char[] { '=' });
            Shape[] shapes = new Shape[1];
            shapes[0] = new Line(new Vector2(-3f, -1.2f), new Vector2(-1f, 0.4f), new char[] { '=' });
            

            for (int i = 0; i < 10000; i++)
            {
                
               // circle.Move(new Vector2(0.05f,0));
                graphics.Begin();
                graphics.Draw(rect);
                graphics.Draw(rect2);
                graphics.Draw(rect3);
                graphics.Draw(rect4);
                graphics.Draw(circle);
                graphics.RayCast();
                graphics.End();



                Control();

            }

            void Control()
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A:
                        graphics.playerAngle = 3.95f;
                        graphics.playerPos.x = graphics.playerPos.x - speed;


                        break;
                    case ConsoleKey.W:
                        graphics.playerAngle = 2.2f;
                        graphics.playerPos.y = graphics.playerPos.y - speed;

                        break;
                    case ConsoleKey.D:
                        graphics.playerAngle = 0.75f;
                        graphics.playerPos.x = graphics.playerPos.x + speed;

                        break;
                    case ConsoleKey.S:
                        graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;

                        break;
                }
                
            }
        }
    }
}

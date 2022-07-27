using System;
using System.Threading;
using System.Runtime.InteropServices;
 
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Drawing;
namespace _2D_Game
{

    public class Program
    {
        private static Graphics graphics = new Graphics(331, 76, 2f);
        
        public static void Control()
        {
            float speed = 0.1f;
            while (true)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        /*graphics.playerAngle = 3.95f;
                        graphics.playerPos.x = graphics.playerPos.x - speed;*/
                        graphics.camera.Move(new Vector3(0, -speed, 0));


                        break;
                    case ConsoleKey.W:
                        /*graphics.playerAngle = 2.2f;
                        graphics.playerPos.y = graphics.playerPos.y - speed;*/
                        graphics.camera.Move(new Vector3(speed, 0, 0));
                        break;
                    case ConsoleKey.D:
                        /* graphics.playerAngle = 0.75f;
                         graphics.playerPos.x = graphics.playerPos.x + speed;*/
                        graphics.camera.Move(new Vector3(0, speed, 0));
                        break;
                    case ConsoleKey.S:
                        /*graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;*/
                        graphics.camera.Move(new Vector3(-speed, 0 , 0));
                        break;
                    case ConsoleKey.Spacebar:
                        /*graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;*/
                        graphics.camera.Move(new Vector3(0, 0, -speed));
                        break;
                    case ConsoleKey.C:
                        /*graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;*/
                        graphics.camera.Move(new Vector3(0, 0, speed));
                        break;
                    case ConsoleKey.NumPad8:
                        /*graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;*/
                        graphics.camera.rotation.y -= speed/2;
                        break;
                    case ConsoleKey.NumPad2:
                        /*graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;*/
                        graphics.camera.rotation.y += speed / 2;
                        break;
                    case ConsoleKey.NumPad4:
                        /*graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;*/
                        graphics.camera.rotation.z -= speed / 2;
                        break;
                    case ConsoleKey.NumPad6:
                        /*graphics.playerAngle = 5.6f;
                        graphics.playerPos.y = graphics.playerPos.y + speed;*/
                        graphics.camera.rotation.z += speed / 2;
                        break;
                }
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ConsoleFont
        {
            public uint Index;
            public short SizeX, SizeY;
        }

        public static class ConsoleHelper
        {
            [DllImport("kernel32")]
            public static extern bool SetConsoleIcon(IntPtr hIcon);

           

            [DllImport("kernel32")]
            private extern static bool SetConsoleFont(IntPtr hOutput, uint index);

            private enum StdHandle
            {
                OutputHandle = -11
            }

            [DllImport("kernel32")]
            private static extern IntPtr GetStdHandle(StdHandle index);

            public static bool SetConsoleFont(uint index)
            {
                return SetConsoleFont(GetStdHandle(StdHandle.OutputHandle), index);
            }

            [DllImport("kernel32")]
            private static extern bool GetConsoleFontInfo(IntPtr hOutput, [MarshalAs(UnmanagedType.Bool)] bool bMaximize,
                uint count, [MarshalAs(UnmanagedType.LPArray), Out] ConsoleFont[] fonts);

            [DllImport("kernel32")]
            private static extern uint GetNumberOfConsoleFonts();

            public static uint ConsoleFontsCount
            {
                get
                {
                    return GetNumberOfConsoleFonts();
                }
            }

            public static ConsoleFont[] ConsoleFonts
            {
                get
                {
                    ConsoleFont[] fonts = new ConsoleFont[GetNumberOfConsoleFonts()];
                    if (fonts.Length > 0)
                        GetConsoleFontInfo(GetStdHandle(StdHandle.OutputHandle), false, (uint)fonts.Length, fonts);
                    return fonts;
                }
            }

        }

        static void Main(string[] args)
        {

            ConsoleHelper.SetConsoleFont(10);
        
            Console.SetBufferSize(331,Console.BufferHeight);

            float speed = 0.02f;
            Console.Beep(500, 200);
           
            float width = graphics.height / 10;
            float height = graphics.width / 100;

           
            Console.Title = "3Д УЛЬТРАМЕГАЭНДЖИН v0.1";


            /*Hello();*/

            Circle circle = new Circle(new Vector2(-1, 0), 0.3f);
            SolidRectangle rect = new SolidRectangle(new Vector2(1, -1f), new Vector2(1,2), new char[] { '=' });
            SolidRectangle rect2 = new SolidRectangle(new Vector2(-1f, -1.5f), new Vector2(2, 1), new char[] { '=' });
            SolidRectangle rect3 = new SolidRectangle(new Vector2(-1f, 1.5f), new Vector2(2, 1), new char[] { '=' });
            SolidRectangle rect4 = new SolidRectangle(new Vector2(-1.8f, -1.5f), new Vector2(0.5f,3f), new char[] { '=' });
            Shape[] shapes = new Shape[1];
            shapes[0] = new Line(new Vector2(-3f, -1.2f), new Vector2(-1f, 0.4f), new char[] { '=' });
            Thread control = new Thread(Control);
            control.Start();

            Thread Sound = new Thread(Sounds);
            Sound.Start();
             
            for (int t = 0; t < 10000; t++)
            {
               
                // circle.Move(new Vector2(0.05f,0));
                graphics.Begin();
                /*graphics.Draw(rect);
                graphics.Draw(rect2);
                graphics.Draw(rect3);
                graphics.Draw(rect4);
                graphics.Draw(circle);*/
                
                graphics.RayCast(t);

                //graphics.Draw(rect);
                graphics.End();
                
                Console.SetCursorPosition(0, 3);
                Console.Write("Dev. Vamperg - v0.01");
                Console.SetCursorPosition(0, 0);


            }

            

            void Sounds()
            {
                Console.Beep(800, 100);
            }
            

            void Hello()
            {
                Console.WriteLine("Чтобы все работало правильно, необходимо провести настройку консоли.\nНажмите правой кнопкой мыши по загаловку консоли, далее свойства. ");
                Console.WriteLine("В настройках расположение изменить размер окна. Ширина 331  высота 81!");
                Console.WriteLine("В настройках терминала Цвет курсора, использовать цвет и все значения в 0");
                Console.WriteLine("Обязательно поставьте 10 РАЗМЕР шрифта в консоли!! ");

                Console.WriteLine("!!!\nНажмите любые кнопки, чтобы начать\n WASD Space C - Управление");
                Console.ReadKey();
            }
        }
    }
}

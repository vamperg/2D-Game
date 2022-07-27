using System;
using System.Threading;


namespace _2D_Game
{
    public class Camera
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 normal;

        public Camera()
        {
            position = new Vector3(-3,0,-1f);
            rotation = new Vector3(0);

        }
        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }
        public void Move(Vector3 direction)
        {
            position = position + direction;
        }
    }
    public class Graphics
    {
        private const double fov = Math.PI /2;
        public Vector2 playerPos = new Vector2(0, 0);
        public float playerAngle = 0;
        private static float depth = 22;

        public Camera camera = new Camera();
        public int width { get; }
        public int height { get; }
        public float aspect { get; }
        public float pixelAspect { get; }
        public float multiplier { get; }
        public char[] buffer { get; private set; }
        
        public Vector3 ro = new Vector3(-4, 0, 0);

        public char[] gradient = {' ','.',':','!','/','r','(','l','1','Z','4','H','9','W','8','$','@'};
        public int gradientSize;

        public Vector3[] spheresPos = new Vector3[50];
        public float[] spheresSize = new float[50];

        public Graphics(int width, int height, float multiplier)
        {
            this.width = width;
            this.height = height;
            this.multiplier = multiplier;
            aspect = (float)width / height;
            pixelAspect = 0.5f;
            buffer = new char[width * height+1];
            gradientSize = gradient.Length - 2;
            var rand = new Random();

            for(int i=0; i < 50; i++)
            {
                spheresPos[i] = new Vector3(rand.Next(-200, 200)/10, rand.Next(-200, 200)/10, -(rand.Next(2, 150)/10));
                spheresSize[i] = rand.Next(5, 30)/10;
            }
        }


        private void Reset() // Буфер пробелами
        {

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    buffer[i + j * width] = ' ';

        }

        public void Begin() //Обнуляет буфер и положение курсора
        {
            Reset();
            Console.SetCursorPosition(0, 0);
        }

        public void Draw(Shape shape) //Добавляет форму в буффер
        {

            //draw у шейп возвращает измененный массив чаров где добавлялась форма
            char[] shapeBuffer = shape.Draw(this);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (shapeBuffer[i + j * width] != 0)
                    {
                        buffer[i + j * width] = shapeBuffer[i + j * width];
                    }
                }
        }

        public void RayCast(int t)
        {
           
            char[] buf = new char[width * height];
 
            /*for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    buf[i + j * width] = ' ';  //заполняем буфер тьмы пустотой*/
            Vector3 itPoint = new Vector3(1);
             
           
            Vector3 spherePos = new Vector3(0,0,0.1f);
            Vector3 spherePos2 = new Vector3(0, -4, 0.1f);
            Vector3 spherePos3 = new Vector3(2, 6, 0.7f);
            Vector3 boxPos = new Vector3(0, -2, 0);
            float specular = 0;
            float itDefault = 1200;
            /*Vector3 light = function.Norm(new Vector3(25*(float)Math.Cos(t*0.1f),10*(float)Math.Sin(t*0.1f)*1.0f,-7f));*/

            camera.normal = function.rotateY(new Vector3(1, 0, 0), camera.rotation.y);
            camera.normal = function.rotateZ(new Vector3(1, 0, 0), camera.rotation.z);
            Vector3 light = function.Norm(new Vector3((float)Math.Cos(t*0.1f), (float)Math.Sin(t*0.1f), - 1f));
            for (int i = 0; i < width; i++)

                for (int j = 0; j < height; j++)
                {
                    float distanceToWall = 0;
                    bool hitWall = false;

                    Vector2 uv = new Vector2(i, j) / new Vector2(width, height) * 2.0f - 1.0f;
                    uv.x *= aspect * pixelAspect;

                   
                    Vector3 rd = function.Norm(new Vector3(1, uv.x,uv.y));

                    float color = 4;
                    char pixel = ' ';

                   
                    ro = camera.position;

                    float diff = 1;
                    float albedo = 1;


                     
                    for (int k = 0; k<2; k++)
                    {
                        float minIt = itDefault;
                        Vector3 n = new Vector3(0);
                        Vector2 intersection;
                         
                        for(int z = 0; z < 20; z++)
                        {
                            intersection = function.Sphere(ro - spheresPos[z], rd, spheresSize[z]);
                            if (intersection.x > 0 && intersection.x < minIt)
                            {
                                itPoint = ro + rd * intersection.x;
                                minIt = intersection.x;
                                n = function.Norm(itPoint - spheresPos[z]);

                            }
                        }
                        

                      

                        Vector3 planePosition = new Vector3(0, 0, -1f);
                        intersection = new Vector2(function.Plane(ro, rd, planePosition, 1),1);
                        albedo = 1;
                        if (intersection.x > 0 && intersection.x < minIt)
                        {
                            minIt = intersection.x;
                            n = planePosition;
                            albedo = 0.1f;
                        }

                        Vector3 planePosition2 = new Vector3(0, 0, 22);
                        intersection = new Vector2(function.Plane(ro-planePosition2, rd, planePosition2, 1), 1);
                        albedo = 1;
                        if (intersection.x > 0 && intersection.x < minIt)
                        {
                            minIt = intersection.x;
                            n = planePosition2;
                            albedo = 0.1f;
                        }



                        if (minIt < itDefault)
                        {
                            diff *= (n * light*0.5f + 0.5f);
                            ro = ro + rd * (minIt - 0.01f);
                            specular = (float)Math.Max(0.0,Math.Pow(function.Reflect(rd, n)* light,132)*2);

                            rd = function.Reflect(rd, n);
                            color = ((diff+specular) * 15 * albedo);
                        }
                        else
                        {
                            break;
                        }
                        
                      
                    }
                    color = function.Clamp(color, 0, gradientSize);
                    pixel = gradient[(int)color];
                    
                    buffer[i + j * width] = pixel;



                    /* float f = (uv.x - playerPos.x) * (uv.x - playerPos.x) + (uv.y - playerPos.y) * (uv.y - playerPos.y);//точность попадания по координатам

                     if (f < 0.015f)
                     {
                         buf[i + j * width] = 'X'; //рисую игрока
                     }

                     if (f < 0.11f)
                     {
                         double rayAngle = playerAngle + fov / 2 - uv.x * fov / width;

                         double rayX = Math.Sin(rayAngle);
                         double rayY = Math.Cos(rayAngle);

                         while (!hitWall && distanceToWall < depth) //пока луч не ударится или  уйдет далеко
                         {
                             distanceToWall += rayStep;//дистанция до обьекта

                             int testX = (int)(i + rayX * distanceToWall);
                             int testY = (int)(j + rayY * distanceToWall);

                             if (testX < 0 || testX > i + depth || testY < 0 || testY >= j + depth) //Если луч ушел
                             {
                                 hitWall = true;
                                 distanceToWall = depth;

                             }
                             else //При попадании на обьект
                             {

                                 char testCell = buffer[testX + testY * width]; //берем символ обьекта
                                 if (testCell != ' ')
                                 {
                                     hitWall = true;
                                     buf[testX + testY * width] = 'R';
                                 }
                             }
                         }

                         if (distanceToWall < depth) // если луч попал на обьект
                         {
                             rayCordX = ((float)(int)(i + rayX * distanceToWall) / width * 2.0f * multiplier - 1.0f * multiplier) * 2; // перевод x карты в координату x
                             rayCordY = ((float)(int)(j + rayY * distanceToWall) / height * 2.0f * multiplier - 1.0f * multiplier) * 2; // перевод x карты в координату x



                             SolidRectangle rect = new SolidRectangle(new Vector2(playerPos.x + 0.16f, playerPos.y), new Vector2(rayCordX - (playerPos.x + 0.25f), 0.1f), new char[] { 'z' }); //прямоугольник - уже не нужен



                         }

                     }
 */
                }
            /* for (int i = 0; i < width; i++)
                 for (int j = 0; j < height; j++)
                 {
                     if (buf[i + j * width] != ' ')
                     {
                         buffer[i + j * width] = buf[i + j * width];
                     }
                 }*/

             
        }






        

        public void End()//Вывод буффер
        {

            
            /*for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                   
                    if (buffer[i + j * width] != ' ')
                    {

                        Console.SetCursorPosition(i, j);
                        Console.Write(buffer[i + j * width]);
                    }
                    if (buffer[i + j * width] != viewBuffer[i + j * width])
                    {
                        Console.Write("wawdawddawd");
                        Console.SetCursorPosition(i, j);
                        Console.Write("w");
                    }
                }

            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    
                    if (buffer[i + j * width] != viewBuffer[i + j * width])
                    {
                        Console.Write("wawdawddawd");
                        Console.SetCursorPosition(i, j);
                        Console.Write("z");
                    }
                }

            }*/
            Console.Write(buffer);
            Console.SetCursorPosition(0, 1);
            Console.Write(playerAngle);
            Console.SetCursorPosition(0, 0);
        }
    }
}

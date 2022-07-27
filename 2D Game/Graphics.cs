using System;


namespace _2D_Game
{

    public class Graphics
    {
        private const double fov = Math.PI /2;
        public Vector2 playerPos = new Vector2(0, 0);
        public float playerAngle = 0;
        private static float depth = 22;

        public int width { get; }
        public int height { get; }
        public float aspect { get; }
        public float pixelAspect { get; }
        public float multiplier { get; }
        public char[] buffer { get; private set; }
        
        public Vector3 ro = new Vector3(-4, 0, 0);

        public char[] gradient = {' ','.',':','!','/','r','(','l','1','Z','4','H','9','W','8','$','@'};
        public int gradientSize;

        public Graphics(int width, int height, float multiplier)
        {
            this.width = width;
            this.height = height;
            this.multiplier = multiplier;
            aspect = (float)width / height;
            pixelAspect = 0.5f;
            buffer = new char[width * height+1];
            gradientSize = gradient.Length - 2;
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
            float rayStep = 0.03f;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    buf[i + j * width] = ' ';  //заполняем буфер тьмы пустотой
            float rayCordX = 0; //координата x луча
            float rayCordY = 0; //координата x луча
            
            Vector3 light = function.Norm(new Vector3((float)Math.Sin(t*0.01f),(float)Math.Cos(t*0.01f), -2f));
            for (int i = 0; i < width; i++)

                for (int j = 0; j < height; j++)
                {

                    float distanceToWall = 0;
                    bool hitWall = false;

                    Vector2 uv = new Vector2(i, j) / new Vector2(width, height) * 2.0f - 1.0f;
                    uv.x *= aspect * pixelAspect;

                    
                    Vector3 rd = function.Norm(new Vector3(1, uv.x,uv.y));

                  
                     
                    int color = 0;
                    char pixel = ' ';

                    Vector3 spherePos = new Vector3(0);
                    float diff = 1;
                    for(int k = 0; k<5; k++) {
                        float minIt = 99999;
                        Vector2 intersection = function.Sphere(ro - spherePos, rd, 1f);



                        Vector3 n = new Vector3(0);


                       
                        if (intersection.x > 0)
                        {
                            Vector3 itPoint = new Vector3(1); /*ro - spherePos + rd * intersection.x;*/
                            itPoint.x = ro.x-spherePos.x + rd.x * intersection.x;
                            itPoint.y = ro.y - spherePos.y + rd.y * intersection.x;
                            itPoint.z = ro.z - spherePos.z + rd.z * intersection.x;

                            minIt = intersection.x;
                            n = function.Norm(itPoint);
                        }
                        intersection = new Vector2(function.Plane(ro, rd, new Vector3(0, 0, -1), 1), 2);
                        if (intersection.x > 0 && intersection.x < minIt)
                        {
                             
                            minIt = intersection.x;
                            n = new Vector3(0, 0, -1);
                        }


                        if (minIt < 99999)
                        {
                            diff = (float)(n.x*light.x+n.y * light.y + n.z * light.z);
                            //ro = ro + rd * (minIt - 0.01f);
                            rd = function.Reflect(rd, n);
                            color = (int)(diff * 20);



                        }
                        color = function.Clamp(color, 0, gradientSize);
                        pixel = gradient[color];



                        buffer[i + j * width] = pixel;
                    }
                    
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
*/                }
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

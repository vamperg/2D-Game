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


        public Graphics(int width, int height, float multiplier)
        {
            this.width = width;
            this.height = height;
            this.multiplier = multiplier;
            aspect = (float)width / height;
            pixelAspect = 0.5f;
            buffer = new char[width * height];
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

        public void RayCast()
        {
            char[] buf = new char[width * height];
            float rayStep = 0.03f;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    buf[i + j * width] = ' ';  //заполняем буфер тьмы пустотой
            float rayCordX = 0; //координата x луча
            float rayCordY = 0; //координата x луча

 
            for (int i = 0; i < width; i++)

                for (int j = 0; j < height; j++)
                {
                    float distanceToWall = 0;
                    bool hitWall = false;

                    float x = (float)i / width * 2.0f * multiplier - 1.0f * multiplier;
                    float y = (float)j / height * 2.0f * multiplier - 1.0f * multiplier;//координаты игрока

                    

                    x *= aspect * pixelAspect; //перевод координат


                    float f = (x - playerPos.x) * (x - playerPos.x) + (y - playerPos.y) * (y - playerPos.y);//точность попадания по координатам

                    if (f< 0.015f)
                    {
                        buf[i + j * width] = 'X'; //рисую игрока
                    }

                    if (f < 0.11f)
                    {

                        


                        double rayAngle = playerAngle + fov / 2 - x * fov / width;

                        double rayX = Math.Sin(rayAngle);
                        double rayY =  Math.Cos(rayAngle);

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
                }
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (buf[i + j * width] != ' ')
                    {
                        buffer[i + j * width] = buf[i + j * width];
                    }
                }
            

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

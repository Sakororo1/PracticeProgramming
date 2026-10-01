namespace task2_1_4
{
    class Program
    {
        public interface IDrawable //интерфейс
        {
            string Draw(); //метод для вывода информации
        }
        class Circle() : IDrawable //класс окружность
        {
            double R=0; //радиус
            public Circle(double r) : this()
            {
                this.R = r;
            }
            public string Draw()
            {
                return "Радиус: "+R.ToString(); //вывод радиуса
            }
        }
        class Rectangle() : IDrawable //класс прямоугольник
        {
            private double side1=0; //стороны
            private double side2=0;
            public Rectangle(double s1, double s2) : this()
            {
                side1= s1; side2= s2;
            }
            public string Draw()
            {
                //вывод сторон
                return $"Сторона a: {side1}, сторона b:{side2}";
            }
        }
        //треугольник
        class Triangle() : IDrawable
        {
            double side1=0; //стороны
            double side2=0;
            double side3=0;
            //конструктор
            public Triangle(double s1, double s2,double s3) : this()
            {
                side1 = s1; side2 = s2; side3 = s3;
            }
            public string Draw()
            {
                return $"Стороны: {side1}, {side2}, {side3}";
            }
        }
        static void Main(string[] args)
        {
            IDrawable[] drawables = new IDrawable[3]; //создание массива для интерфейса
            drawables[0] = new Circle(3); //инициализация элементов массива
            drawables[1] = new Rectangle(3,5.1);
            drawables[2] = new Triangle(4,3,5);
            for (int i = 0; i < drawables.Length; i++)
            {
                Console.WriteLine(drawables[i].Draw()); //вывод информации о фигуре
            }
        }
    }
}

namespace task4_1
{
    class Program
    {
        interface IFigure //интерфейс фигура
        {
            public double Area(); //метод для вычисления площади
            public double Perimeter(); //метод для вычисления периметра
        }

        class Circle : IFigure //класс круг
        {
            private double R; //радиус
            public Circle(double r) //конструктор круга
            {
                R = r;
            }
            public double Area() //площать круга
            {
                return Math.Pow(R, 2)*Math.PI;
            }
            public double Perimeter() //перименр круга
            {
                return 2*R*Math.PI;
            }
        }
        class Rectangle : IFigure //класс прямоугольник
        {
            private double a, b; //стороны прямоугольника
            public Rectangle(double A, double B) //конструктор прямоугольника
            {
                a = A; b=B;
            }
            public double Area() //площадь прямоугольника
            {
                return a * b;
            }
            public double Perimeter() //периметр прямоугольника
            {
                return (a + b) * 2;
            }
        }

        class Triangle: IFigure
        {
            private double a, b, c; //стороны треугольника
            public Triangle(double A, double B, double C) //конструктор треугольника
            {
                a=A; b=B; c=C;
            }

            public double Area() //площадь треугольника
            {
                double p = Perimeter() / 2; //полупериметр
                return Math.Sqrt(p*(p-a)*(p-b)*(p-c));
            }
            public double Perimeter() //периметр треугольника
            {
                return a+b+c;
            }
        }
        static void Main(string[] args)
        {
            IFigure c=new Circle(5.6); //создание объекта круга
            IFigure rect = new Rectangle(10,8.9); //создание объекта прямоугольника
            IFigure t = new Triangle(3.4,4.5,5.6); //создание объекта треугольника
            Console.WriteLine($"Длина окружности с R=5.6: {c.Perimeter()}"); //периметр и площадь круга
            Console.WriteLine($"Площадь круга с R=5.6: {c.Area()}");
            Console.WriteLine($"Периметр прямоугольника, a=10, b=8.9: {rect.Perimeter()}"); //периметр и площадь прямоугольника
            Console.WriteLine($"Площадь прямоугольника, a=10, b=8.9: {rect.Area()}");
            Console.WriteLine($"Периметр треугольника, a=3.4, b=4.5, c=5.6: {t.Perimeter()}"); //периметр и площадь треугольника
            Console.WriteLine($"Площадь треугольника, a=3.4, b=4.5, c=5.6: {t.Area()}");
        }
    }
}

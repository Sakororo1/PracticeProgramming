namespace task2_1_2
{
    class Program
    {
        public class Shape //класс фигура
        {
            private double c=-1; //характеристика фигуры
                        //периметр
            public virtual double Perimeter()
            {
                return c; //неизвестна фигура, неизвестно как считать
            }
            //площадь
            public virtual double Area()
            {
                return c; //неизвестна фигура, неизвестно как считать
            }
        }
        class Rectangle : Shape //прямоугольник
        {
            private double a; //сторона a
            private double b; //сторона b
                              //конструктор
            public Rectangle(double sidea, double sideb)
            {
                a = sidea; b = sideb;
            }
            public override double Perimeter() //периметр прямоугольника
            {
                return (a + b) * 2;
            }
            public override double Area() //площадь прямоугольника
            {
                return a * b;
            }
        }
        class Circle : Shape //круг
        {
            public double R; //радиус
            public Circle(double r) //конструктор
            {
                R = r;
            }
            public override double Perimeter() //периметр круга
            {
                return 2 * Math.PI * R;
            }
            public override double Area() //площадь круга
            {
                return Math.PI * Math.Pow(R, 2);
            }
        }
        static void Main(string[] args)
        {
            Shape s = new Shape(); //создание фигуры
            Rectangle r = new Rectangle(10,7); //создание прямоугольника
            Circle c= new Circle(5); //создание круга
            Console.WriteLine("-Класс Shape (объект s):"); //проверка методов Shape
            Console.WriteLine($"s.Perimeter(): {s.Perimeter()}");
            Console.WriteLine($"s.Area(): {s.Area()}");
            Console.WriteLine("-Класс Rectangle (объект r, a=10,b=7):"); //проверка методов Rectangle
            Console.WriteLine($"r.Perimeter(): {r.Perimeter()}");
            Console.WriteLine($"r.Area(): {r.Area()}");
            Console.WriteLine("-Класс Circle (объект c, R=5):"); //проверка методов Circle
            Console.WriteLine($"c.Perimeter(): {c.Perimeter()}");
            Console.WriteLine($"c.Area(): {c.Area()}");
            
        }
    }
}

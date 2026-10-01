namespace task3_1
{
    class Program
    {
         class Figure //класс фигура
         {
            public static double Area(params double[] num)
            {
                switch (num.Length) //выбор метода
                {
                    case 1: //площадь круга
                        DoingA=Circle.Area;
                        return DoingA.Invoke(num[0]);
                    case 2: //площадь прямоугольника
                        DoingA=Rectangle.Area;
                        return DoingA.Invoke(num[0], num[1]);
                    case 3: //площадь треугольника
                        DoingA = Triangle.Area;
                        return DoingA.Invoke(num[0], num[1], num[2]);
                    default:
                        return -1;                
                }
            }
            delegate double A(params double[] num); //делегат
            static A DoingA;
            private class Circle//класс круг
            {
                public static double Area(params double[] R) //площадь круга
                {
                    return Math.Pow(R[0], 2) * Math.PI;
                }
            }
            private class Rectangle //класс прямоугольник
            {
                public static double Area(params double[] a) //площадь прямоугольника
                {
                    return a[0] * a[1];
                }
            }
            private class Triangle //класс треугольник
            {
                public static double Area(params double[] a) //площадь треугольника
                {
                    double p = (a[0] + a[1] + a[2]) / 2; //вычисление полупериметра
                    return Math.Sqrt(p * (p - a[0]) * (p - a[1]) * (p - a[2])); //формула герона
                }
            }
         }        
        static void Main(string[] args)
        {
            Console.WriteLine($"Вызов метода площади без аргументов: {Figure.Area()}");
            Console.WriteLine($"Площадь круга с радиусом 3: {Figure.Area(3)}");
            Console.WriteLine($"Площадь прямоугольника со сторонами 2.5,60: {Figure.Area(2.5,60)}");
            Console.WriteLine($"Площадь треугольника со сторонами 6,8,10: {Figure.Area(6,8,10)}");
            Console.WriteLine($"Вызов метода площади с аргументами>3: {Figure.Area(2,5,2,1)}");
        }
    }
}

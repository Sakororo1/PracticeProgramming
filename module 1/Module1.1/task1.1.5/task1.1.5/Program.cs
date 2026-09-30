namespace task1_1_5
{
    class Program
    {
        static void Main(string[] args)
        {
           Random r= new Random(); //объект для генерации случайных чисел
           int res = (int)r.NextInt64(1,101); //генерация числа от 1 до 100
           if ((res % 3 == 0) && (res % 5 == 0)) //проверка что число кратно 3 и 5
           {
               Console.WriteLine("FizzBuzz");
           }
           else if (res % 3 == 0) //проверка, что число кратно 3
           {
               Console.WriteLine("Fizz");
           }
           else if (res % 5 == 0) //проверка, что число кратно 5
           {
               Console.WriteLine("Buzz");
           }
           else
           {
               Console.WriteLine(res); //вывод числа
           }
        }
    }
}

using System;
namespace task1_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r=new Random();
            int num = (int)r.NextInt64(1, 101); //генерация числа от 1 до 100
            Console.Write("Угадайте целое число от 1 до 100: ");
            int user_input=Convert.ToInt32(Console.ReadLine()); //ввод числа
            while (user_input!=num) //проверка, что ответ неверный
            {
                if (user_input<num) //проверка, что ответ меньше нужного
                {
                    Console.Write("Ответ должен быть больше. Введите заново: ");
                }
                else //ответ больше нужного
                {
                    Console.Write("Ответ должен быть меньше. Введите заново: ");
                }
                user_input = Convert.ToInt32(Console.ReadLine()); //повторный ввод числа
            }
            Console.WriteLine($"Правильно! Ответ равен {num}"); //вывод при правильном ответе
        }
    }
}

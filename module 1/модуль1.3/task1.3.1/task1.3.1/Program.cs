namespace task1_3_1
{
    class Program
    {
        //функция для нахождения НОД двух чисел
        static uint GCM(uint n1, uint n2)
        {
            uint g = 1; //значение НОД, по умолчанию 1
            for (uint i = n1; i <= 1 ;i--) 
            {
                if ((n1%i==0)&&(n2%i==0)) //проверка, что оба числа делятся на i
                {
                    g = i; //новый НОД
                }
            }        
            return g;
        }
        static void Main(string[] args)
        {
            Console.Write("Введите целое неотрицательное число (числитель): ");
            uint num1=Convert.ToUInt32(Console.ReadLine()); //ввод числителя
            Console.Write("Введите целое положительное число (знаменатель), большее чем числитель: ");
            uint num2=Convert.ToUInt32(Console.ReadLine()); //ввод знаменателя
            while (num2 == 0) //проверка, что знаменатель равен нулю
            {
                //повторный ввод
                Console.Write("Знаменатель не может быть нулём. Введите заново: ");
                num2 = Convert.ToUInt32(Console.ReadLine());
            }
            while (num1>num2) //проверка, что дробь неправильная
            {
                //повторный ввод
                Console.Write("Программа может работать только с обыкновенными дробями.");
                Console.Write("Введите целое неотрицательное число (числитель): ");
                num1 = Convert.ToUInt32(Console.ReadLine()); //ввод числителя
                Console.Write("Введите целое положительное число (знаменатель), большее чем числитель: ");
                num2 = Convert.ToUInt32(Console.ReadLine());
                while (num2 == 0)
                {
                    Console.Write("Знаменатель не может быть нулём. Введите заново: ");
                    num2 = Convert.ToUInt32(Console.ReadLine());
                }
            }
            uint G = GCM(num1, num2);
            Console.WriteLine($"{num1}\n-\n{num2}");
            Console.WriteLine("   =");
            Console.Write($"{num1/G}\n-\n{num2/G}");
        }
    }
}

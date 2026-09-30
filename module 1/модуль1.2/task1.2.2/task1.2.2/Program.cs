namespace task1_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[10]; //массив размером 10
            Random r=new Random();
            Console.WriteLine("Изначальный массив:");
            for (int i = 0; i < a.Length; i++)
            {
                a[i] =(int) r.NextInt64(-100,101);
                Console.WriteLine(a[i]);
            }
            Console.Write("Введите целое число: ");
            int N = Convert.ToInt32(Console.ReadLine()); //ввод числа
            int M = a.Max(); //определение наибольшего значения в массиве
                        
            for (int i = 0; i < a.Length; i++) //замена наибольшего элемента 
            {
                if (a[i]==M) //проверка, что текущий элемент имеет наибольшее значение
                {
                    a[i] = N; //замена элемента на введённое число
                }    
            }
            Console.WriteLine("Изменённый массив:");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }
    }
}

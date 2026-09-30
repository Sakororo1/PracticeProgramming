namespace task1_2_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер массива K: ");
            uint K=Convert.ToUInt32(Console.ReadLine()); //ввод K
            Random r=new Random();
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"; //русский алфавит
            char[] a=new char[K]; //создание символьного массива
            Console.WriteLine("Первый массив: ");
            for (int i=0; i<K; i++)
            {
                a[i] = alphabet[(int)r.NextInt64(0,alphabet.Length)]; //присвоение элементу случайного символа
                Console.WriteLine(a[i]);
            }
            string sogl = "бвгджзйклмнпрстфхцчшщъь"; //согласные буквы
            int count = 0; //количество согласных букв
            foreach (char c in a)
            {
                if (sogl.Contains(c)) //проверка, что буква согласная
                {
                    count++;
                }
            }
            if (count>0)
            {
                char[] b=new char[count]; //создание массива с согласными буквами
                int j = 0; //счётчик, указывающий на элемент массива b
                for (int i=0; i<a.Length; i++) //добавление символов
                {
                    if (sogl.Contains(a[i])) //проверка, что текущая буква согласная
                    {
                        b[j] = a[i]; //добавление символа в массив
                        j++;
                    }
                }
                Console.WriteLine("Второй массив:");
                foreach(char c in b) //вывод элементов массива с согласными буквами
                {
                    Console.WriteLine(c); //вывод элемента
                }
            }
            else
            {
                Console.WriteLine("Созданный массив согласных букв будет пуст");
            }
        }
    }
}

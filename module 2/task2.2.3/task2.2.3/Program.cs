namespace task2_2_3
{
    class Program
    {
        class StringArray //одномерный массив строк фиксированной длины
        {
            private uint fSize; //размер массива
            private string[] fStrings; //элементы массива

            public StringArray(uint size) //конструктор
            {
                fSize = size;
                fStrings = new string[size];
                for (uint i = 0; i < fSize; i++)
                {
                    fStrings[i] = ""; //изначально все строки пустые
                }
            }
            public uint GetSize() //получение размера массива
            {
                return fSize;
            }
            public void SetElement(uint index, string value) //установка элемента по индексу
            {
                if (index >= fSize)
                {
                    Console.WriteLine($"Ошибка: выход за пределы массива");
                    return;
                }
                fStrings[index] = value;
            }
            public string GetElement(uint index) //получение элемента по индексу
            {
                if (index >= fSize)
                {
                    Console.WriteLine($"Ошибка: выход за пределы массива");
                    return "";
                }
                return fStrings[index];
            }
            public StringArray Concat(StringArray other) //поэлементное сцепление двух массивов
            {
                //размер - минимум из двух размеров
                uint size = Math.Min(other.GetSize(),fSize);
                StringArray result = new StringArray(size);
                for (uint i = 0; i < size; i++)
                {
                    result.SetElement(i, fStrings[i] + other.GetElement(i));
                }
                return result;
            }
            public StringArray Merge(StringArray other) //слияние с исключением повторяющихся элементов
            {
                List<string> unique = new List<string>(); //список уникальных строк
                //добавляем элементы первого массива
                for (uint i = 0; i < fSize; i++)
                {
                    if (!unique.Contains(fStrings[i]))
                    {
                        unique.Add(fStrings[i]);
                    }
                }
                //добавление уникальных элементов второго массива
                for (uint i = 0; i < other.GetSize(); i++)
                {
                    if (!unique.Contains(other.GetElement(i)))
                    {
                        unique.Add(other.GetElement(i));
                    }
                }
                //создание нового массива
                StringArray result = new StringArray((uint)unique.Count);
                for (int i = 0; i < unique.Count; i++)
                {
                    result.SetElement((uint)i, unique[i]);
                }
                return result;
            }
            public void PrintElement(uint index) //вывод элемента по индексу
            {
                if (index >= fSize)
                {
                    Console.WriteLine($"Ошибка: выход за пределы массива");
                    return;
                }
                Console.WriteLine($"Элемент [{index}]: \"{fStrings[index]}\"");
            }
            public void PrintAll() //вывод всего массива
            {
                Console.WriteLine($"Массив из {fSize} элементов:");
                for (uint i = 0; i < fSize; i++)
                {
                    Console.WriteLine($"\"{fStrings[i]}\"");
                }
            }
        }
        static void Main(string[] args)
        {
            StringArray a = new StringArray(3); //создание первого массива
            a.SetElement(0, "lorem");
            a.SetElement(1, "ipsum");
            a.SetElement(2, "dolor");
            StringArray b = new StringArray(2); //создание второго массива
            b.SetElement(0, "sit");
            b.SetElement(1, "amet");
            Console.WriteLine("Массив 1:");
            a.PrintAll();
            Console.WriteLine("Массив 2:");
            b.PrintAll();
            //вывод элемента по индексу
            Console.WriteLine("Вывод элемента массива 1 по индексу 1:");
            a.PrintElement(1);
            //проверка выхода за пределы массива
            Console.WriteLine("Проверка выхода за пределы массива:");
            a.PrintElement(10);
            a.SetElement(10, "тест");
            //поэлементное сцепление
            Console.WriteLine("Поэлементное сцепление массивов:");
            StringArray c = a.Concat(b);
            c.PrintAll();
            //слияние с исключением повторов
            Console.WriteLine("Слияние массивов с исключением повторяющихся элементов:");
            StringArray d = a.Merge(b);
            d.PrintAll();
        }
    }
}
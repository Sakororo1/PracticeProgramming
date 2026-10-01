namespace task3_4
{
    class Program
    {
        class Datas //фильтрация
        {
            private List<Data> dat; //список записей
            private struct Data //структура данные
            {
                public int Value; //значение
                public string Type; //тип данных    
                public int ID; //номер       
            }
            //добавление записи
            public void AddData(int val, string t)
            {
                if (dat == null)
                {
                    dat = new List<Data>();
                }
                Data d; d.Value = val; d.Type = t; d.ID = dat.Count;
                dat.Add(d); //добавление эдемента в класс
            }
            public void PrintAll() //вывод всех записей
            {
                if (dat == null) return;
                for (int i = 0; i < dat.Count; i++)
                {
                    Print(i);
                }
            }
            public void Print(int n) //вывод элемента по индексу
            {
                if (dat == null) return;
                Console.WriteLine($"номер: {n}, значение: {dat[n].Value}, тип: {dat[n].Type}");
            }
            //ФИЛЬТРАЦИЯ
            delegate void FilterString(string s); //фильтр со строковым значением
            FilterString FilterStr; //фильтр по типу данных
            delegate void FilterNum(int n); //фильтр по числовому значению
            FilterNum FilterVal; //фильтр по значению
            FilterNum FilterID; //фильтр по номеру
            //выбор фильтра
            public void ChooseFilter(byte action)
            {
                switch (action)
                {       
                    case 1://1 - по типу
                        FilterStr = FilterType;
                        Console.Write("Введите тип данных (int,long,double,short,sbyte): ");
                        FilterStr.Invoke(Console.ReadLine());
                        break;
                    case 2://2 - больше значения
                        FilterVal = FilterMoreThanValue;
                        Console.Write("Введите значение: ");
                        FilterVal.Invoke(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 3: //3 - меньше значения
                        FilterVal = FilterLessThanValue;
                        Console.Write("Введите значение: ");
                        FilterVal.Invoke(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 4: //4 - равно значению
                        FilterVal = FilterEqualToValue;
                        Console.Write("Введите значение: ");
                        FilterVal.Invoke(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 5: //5 - первые n
                        FilterID = FilterFirst;
                        Console.Write("Введите значение: ");
                        FilterID.Invoke(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 6: //6 - последние n
                        FilterID = FilterLast;
                        Console.Write("Введите значение: ");
                        FilterID.Invoke(Convert.ToInt32(Console.ReadLine()));
                        break;
                    default:
                        Console.WriteLine("Что-то пошло не так");
                        break;
                }
            }               
            //фильтрация по типу данных
            private void FilterType(string type)
            {
                for (int i = 0; i < dat.Count; i++)
                {
                    //вывод только подходящих
                    if (dat[i].Type == type)
                    {
                        Print(i);
                    }
                }
            }
            //фильтрация по значению: больше
            private void FilterMoreThanValue(int v)
            {
                for (int i = 0; i < dat.Count; i++)
                {
                    if (dat[i].Value > v)
                    {
                        Print(i);
                    }
                }
            }
            //фильтрация по значению: меньше
            private void FilterLessThanValue(int v)
            {
                for (int i = 0; i < dat.Count; i++)
                {
                    if (dat[i].Value < v)
                    {
                        Print(i);
                    }
                }
            }
            //фильтрация по значению: равно
            private void FilterEqualToValue(int v)
            {
                for (int i = 0; i < dat.Count; i++)
                {
                    if (dat[i].Value == v)
                    {
                        Print(i);
                    }
                }
            }
            //фильтрация по номеру: первые n
            private void FilterFirst(int n)
            {
                for (int i = 0; (i < n) && (i < dat.Count); i++)
                {
                    Print(i);
                }
            }
            //фильтрация по номеру: последние n
            private void FilterLast(int n)
            {
                for (int i = dat.Count - 1; (i >= dat.Count - n) && (i >= 0); i--)
                {
                    Print(i);
                }
            }
        }
        static void Main(string[] args)
        {
            Datas d = new Datas(); //создание объекта для работы с данными
            string[] Types = new string[5] { "short", "int", "long", "sbyte", "double" };
            Random r = new Random();
            int N = (int)r.NextInt64(5, 40); //определение количества записей
            for (int i = 0; i < N; i++)
            {
                d.AddData((int)r.NextInt64(-50, 51), Types[r.Next(0, 5)]); //добавление записей
            }
            Console.WriteLine("Все добавленные данные:");
            d.PrintAll(); //вывод всех записей
            string choice;
            do
            {
                Console.Write("Выберите фильтр (1 - по типу, 2 - больше значения, 3 - меньше значения, " +
                                  "4 - равно значению, 5 - первые n, 6 - последние n): ");
                choice = Console.ReadLine();
                if (choice!="exit")
                {
                    d.ChooseFilter(Convert.ToByte(choice));
                }
                else
                {
                    break;
                }
            } while (true);
        }
    }
}
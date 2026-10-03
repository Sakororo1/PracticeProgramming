namespace task4_3
{
    class Program
    {
        interface IStudent
        {
            public double IGetAverage(); //средний балл студентов
            public void IPrintInfo(); //вывод информации
            public void SetAverageMark(int i, double Mark); //установка среднего балла группы
            public int GetNumOfGroups(); //получение количества групп
        }
        class StudYear : IStudent //основа для остальных классов
        {
            protected Group[] groups; //массив с группами
            protected struct Group //тип данных группа
            {
                public string fName; //название группы
                public double fAverageMark; //средний балл в группе
                public byte fStudNum; //количество студентов в группе
            }
            public double IGetAverage()
            {
                double sum = 0;
                for (int i = 0; i < groups.Length; i++) //вычисление количества студентов на 1 курсе
                {
                    sum += groups[i].fAverageMark;
                }
                return Math.Round((sum / groups.Length),2); //среднее арифметическое
            }
            public void IPrintInfo()
            {
                byte sum = 0;
                for (int i = 0; i < groups.Length; i++) //вычисление количества студентов
                {
                    sum += groups[i].fStudNum;
                }
                Console.WriteLine($"Всего студентов на курсе: {sum}");
                Console.WriteLine($"Всего групп на курсе: {groups.Length}");
                Console.WriteLine($"Группы:");
                for (int i = 0; i < groups.Length; i++)
                {
                    Console.WriteLine($"{groups[i].fName}, средний балл: {groups[i].fAverageMark}"); //вывод группы по очереди
                }
            }
            public void SetAverageMark(int i, double Mark) //метод для установки среднего балла
            {
                //проверка на выход за границы массива, а также того, что оценка находится в пределе (0,10]
                if (i >= 0 && i < groups.Length && Mark > 0 && Mark <= 10)
                {
                    groups[i].fAverageMark = Mark;
                }
            }
            public int GetNumOfGroups() //получение количества групп
            {
                return groups.Length; 
            }
        }
        class FirstYear : StudYear //1 курс
        {
            public FirstYear() //конструктор
            {
                groups = new Group[3]; //определение групп
                groups[0].fName = "1АИ48";
                groups[1].fName = "1ИЭ48";
                groups[2].fName = "1ДИ48";
                groups[0].fStudNum = 25;
                groups[1].fStudNum = 30;
                groups[2].fStudNum = 23;
                //средние баллы групп определяются в основной программе
            }
        }
        class SecondYear : StudYear //2 курс
        {
            public SecondYear() //конструктор
            {
                groups = new Group[3]; //определение групп
                groups[0].fName = "2АИ47";
                groups[1].fName = "2ИЭ47";
                groups[2].fName = "2ДИ47";
                groups[0].fStudNum = 25;
                groups[1].fStudNum = 30;
                groups[2].fStudNum = 23;
                //средние баллы групп определяются в основной программе
            }
        }
        class ThirdYear : StudYear //2 курс
        {
            public ThirdYear() //конструктор
            {
                groups = new Group[2]; //определение групп
                groups[0].fName = "3АИ46";
                groups[1].fName = "3ИЭ46";
                groups[0].fStudNum = 25;
                groups[1].fStudNum = 15;
                //средние баллы групп определяются в основной программе
            }
        }
        static void Main(string[] args)
        {
            IStudent year1 = new FirstYear(); //создание объекта первого курса
            IStudent year2 = new SecondYear(); //создание объекта второго курса
            IStudent year3 = new ThirdYear(); //создание объекта третьего курса
            Random r = new Random();
            for (int i = 0; i < year1.GetNumOfGroups(); i++) //установка средних баллов
            {
                year1.SetAverageMark(i,Math.Round((r.NextDouble()*10),2));
            }
            for (int i = 0;i < year2.GetNumOfGroups(); i++)
            {
                year2.SetAverageMark(i, Math.Round((r.NextDouble() * 10), 2));
            }
            for (int i = 0; i < year3.GetNumOfGroups(); i++)
            {
                year3.SetAverageMark(i, Math.Round((r.NextDouble() * 10), 2));
            }
            //вывод информации о каждом курсе
            Console.WriteLine("1 курс:"); year1.IPrintInfo();
            Console.WriteLine("2 курс:"); year2.IPrintInfo();
            Console.WriteLine("3 курс:"); year3.IPrintInfo();
            //вывод средних баллов на курсе
            Console.WriteLine($"Средние баллы\n1 курс: {year1.IGetAverage()}" +
                $"\n2 курс: {year2.IGetAverage()}\n3 курс: {year3.IGetAverage()}");
        }
    }
}
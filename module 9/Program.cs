namespace task9_1
{
    class Program
    {
        static public void TransformingOrEnter(ConsoleKeyInfo k) //перемещение или Enter
        {
            switch (k.Key)
            {
                case ConsoleKey.UpArrow: //стрелка вверх
                    Console.SetCursorPosition(0, pos);
                    Console.Write(" ");
                    Pos--;
                    Console.SetCursorPosition(0, pos);
                    Console.Write(">");
                    return;
                case ConsoleKey.DownArrow: //стрелка вверх
                    Console.SetCursorPosition(0, pos);
                    Console.Write(" ");
                    Pos++;
                    Console.SetCursorPosition(0, pos);
                    Console.Write(">");
                    return;
                case ConsoleKey.Enter: //нажатие клавиши Enter
                    switch (chapter)
                    {
                        case 1:
                            SwitchEnterFirst();
                            return;
                        case 2:
                            SwitchEnterSecond();
                            return;
                        case 5:
                            SwitchEnterFifth();
                            return;
                    }
                    return;
            }
        }
        static void LoadFromBase() //загрузка из базы данных
        {
            List<WorkStudents.Student> s = work.LoadFromBase(); //получение списка студентов
            if (s.Count == 0)
            {
                Console.WriteLine("Записей нет");
                Up_c = 3;
            }
            else
            {
                for (int i = 0; i < s.Count; i++) //построчный вывод
                {
                    Console.WriteLine($"  {s[i].id} - {s[i].name} - {s[i].group}");
                }
            }
            Down_c = 8 + (uint)s.Count;
        }
        static public void SwitchEnterFirst() //нажатие Enter в первом разделе
        {
            switch (pos) //определение действия по позиции курсора
            {
                case 1: //2 раздел, вывод списка
                    chapter = 2;
                    Up_c = 2;
                    pos = 3;
                    return;
                case 2: //3 раздел, сохранение списка студентов
                    chapter = 3;
                    return;
                case 3: //4 раздел, загрузка из файла
                    chapter = 4;
                    return;
                case 4: //выход
                    KeepGoing = false;
                    return;
            }
        }
        static public void SwitchEnterSecond()
        {
            int count = (int)Down_c - 8; //количество студентов
            if (pos >= 2 && pos < 2 + count) return; //если выбрана строка со студентом, выход из метода
            int exit_pos; //позиция выхода
            if (count > 0)
            {
                exit_pos = (int)Down_c - 1;
            }
            else
            {
                exit_pos = (int)Down_c;
            }
            if (pos >= exit_pos) //выход
            {
                chapter = 1;
                pos = 1;
                Up_c = 1;
                Down_c = 4;
                return;
            }
            if (pos >= exit_pos - 1) //сортировка
            {
                chapter = 5;
                pos = 1;
                Up_c = 1;
                Down_c = 7;
                return;
            }
            if (pos >= exit_pos - 2) //поиск
            {
                Console.Write("Введите строку для поиска: ");
                string s = Console.ReadLine(); //строка поиска
                if (!string.IsNullOrEmpty(s)) //проверка, что строка не пуста
                {
                    List<WorkStudents.Student> found = work.SearchBy(s); //выполнение поиска
                    if (found != null && found.Count > 0) //проверка, что найдены записи
                    {
                        Console.WriteLine("Найденные записи:");
                        for (int i = 0; i < found.Count; i++) //построчный вывод
                        {
                            Console.WriteLine($"  {found[i].id} - {found[i].name} - {found[i].group}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ничего не найдено."); //сообщение об отсутствии результатов
                    }
                }
                Console.ReadLine();
                return;
            }
            if (pos >= exit_pos - 3) //удаление
            {
                Console.Write("Введите номер студента для удаления: ");
                string input = Console.ReadLine(); //чтение номера
                if (int.TryParse(input, out int num) && num >= 1 && num <= count) //проверка корректности
                {
                    work.DeleteStudent(num + 1); //удаление
                }
                return;
            }
            if (pos >= exit_pos - 4) //изменение записи
            {
                Console.Write("Введите номер студента для изменения: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num) && num >= 1 && num <= count) //проверка корректности
                {
                    Console.Write("Что изменить? (1 - ФИО, 2 - группа): ");
                    string choice = Console.ReadLine(); //выбор изменяемого поля
                    if (choice == "1") //если выбрано изменение ФИО
                    {
                        Console.Write("Введите новое ФИО: "); //приглашение к вводу
                        string new_name = Console.ReadLine(); //чтение нового значения
                        work.ChangeStudentName(num + 1, new_name); //изменение ФИО
                    }
                    else if (choice == "2") //если выбрано изменение группы
                    {
                        Console.Write("Введите новую группу: "); //приглашение к вводу
                        string new_group = Console.ReadLine(); //чтение нового значения
                        work.ChangeStudentGroup(num + 1, new_group); //изменение группы
                    }
                }
                return;
            }
            if (pos >= exit_pos - 5) //добавление записи
            {
                Console.Write("Введите ФИО нового студента: "); //приглашение к вводу ФИО
                string name = Console.ReadLine(); //чтение ФИО
                Console.Write("Введите группу нового студента: "); //приглашение к вводу группы
                string group = Console.ReadLine(); //чтение группы
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(group)) //проверка, что оба поля заполнены
                {
                    work.AddStudent(name, group); //добавление студента в базу данных
                }
                return;
            }
        }
        static public void SwitchEnterFifth() //Enter в пятом разделе
        {
            if (pos == 7) //назад
            {
                chapter = 2;
                pos = 3;
                Up_c = 2;
                return;
            }
            //выбор сортировки
            List<WorkStudents.Student> sorted = work.SortBy(pos); //получение отсортированного списка
            if (sorted != null && sorted.Count > 0) //проверка, что список не пуст
            {
                Console.WriteLine("Отсортированный список:");
                for (int i = 0; i < sorted.Count; i++) //построчный вывод
                {
                    Console.WriteLine($"  {sorted[i].id} - {sorted[i].name} - {sorted[i].group}"); //вывод строки
                }
            }
            else
            {
                Console.WriteLine("Записей нет."); //сообщение об отсутствии записей
            }
            Console.ReadLine();
            chapter = 2;
            pos = 3;
            Up_c = 2;
        }
        static byte chapter = 1; //раздел, по умолчанию первый
        //1 - главное меню, 2 - список студентов, 3 - сохранение, 4 - загрузка, 5 - сортировка
        static uint Up_c = 1; //верхнее ограничение
        static uint Down_c = 4; //нижнее ограничение
        static int pos = 1; //позиция указателя
        static int Pos //свойство для указателя для проверок
        {
            get { return pos; }
            set
            {
                if (value > Down_c) pos = (int)Down_c;
                else if (value < Up_c) pos = (int)Up_c;
                else pos = value;

            }
        }
        static WorkStudents work;
        static bool KeepGoing = true; //продолжение работы программы
        static void Main(string[] args)
        {
            work = new WorkStudents();
            Console.ForegroundColor = ConsoleColor.Magenta;
            while (KeepGoing)
            {
                Console.Clear();
                switch (chapter)
                {
                    case 1: //первый раздел
                        {
                            Console.WriteLine("Меню"); //0
                            Console.WriteLine("  Вывести список студентов"); //после вывода доступно редактирование списка
                            Console.WriteLine("  Сохранить список студентов в файл"); //2
                            Console.WriteLine("  Загрузить список студентов из файла"); //3
                            Console.WriteLine("  Выход"); //4
                            Console.SetCursorPosition(0, pos);
                            Console.Write(">");
                            TransformingOrEnter(Console.ReadKey());
                            continue;
                        }
                    case 2:
                        {
                            Console.WriteLine("Список студентов"); //0
                            Console.WriteLine("номер - фио - группа"); //1
                            LoadFromBase(); //2
                            Console.WriteLine("  Добавить запись"); //3+x (новый пункт)
                            Console.WriteLine("  Изменить запись"); //4+x
                            Console.WriteLine("  Удалить запись"); //5+x
                            Console.WriteLine("  Поиск"); //6+x
                            Console.WriteLine("  Сортировка"); //7+x
                            Console.Write("  Выход"); //8+x
                            Console.SetCursorPosition(0, pos);
                            Console.Write(">");
                            TransformingOrEnter(Console.ReadKey());
                            continue;
                        }
                    case 3:
                        {
                            Console.WriteLine("Сохранение списка студентов"); //0
                            Console.Write("Введите путь для сохранения файла: "); //1
                            string path = Console.ReadLine(); //получение пути
                            if (!string.IsNullOrEmpty(path)) //проверка, что путь введён
                            {
                                try
                                {
                                    work.SaveToTXT(path); //сохранение списка в файл
                                    Console.WriteLine("Файл успешно сохранён."); //сообщение об успешном сохранении
                                }
                                catch
                                {
                                    Console.WriteLine("Ошибка при сохранении файла."); //сообщение об ошибке
                                }
                            }
                            Console.ReadLine();
                            chapter = 1;
                            pos = 1;
                            Up_c = 1;
                            Down_c = 4;
                            continue;
                        }
                    case 4:
                        {
                            Console.WriteLine("Загрузка списка студентов из файла");
                            Console.Write("Введите путь к файлу: ");
                            string path = Console.ReadLine(); //получение пути
                            if (!string.IsNullOrEmpty(path)) //проверка, что путь введён
                            {
                                List<WorkStudents.Student> loaded = work.LoadFromFile(path); //загрузка из файла
                                if (loaded == null || loaded.Count == 0) //проверка результата
                                {
                                    Console.WriteLine("Не удалось загрузить файл или файл пуст."); //сообщение об ошибке
                                }
                                else
                                {
                                    Console.WriteLine("Загруженные записи:"); //заголовок вывода
                                    for (int i = 0; i < loaded.Count; i++) //построчный вывод
                                    {
                                        Console.WriteLine($"  {loaded[i].id} - {loaded[i].name} - {loaded[i].group}"); //вывод строки
                                    }
                                    Console.Write("Добавить загруженные записи в базу данных? (y/n): "); //приглашение к выбору
                                    ConsoleKeyInfo k = Console.ReadKey(); //получение ответа
                                    if (k.Key == ConsoleKey.Y)
                                    {
                                        work.AddToBase(); //добавление загруженных записей в базу данных
                                        Console.WriteLine("Записи успешно добавлены.");
                                    }
                                }
                            }
                            Console.ReadKey(); //ожидание нажатия клавиши
                            chapter = 1;
                            pos = 1;
                            Up_c = 1;
                            Down_c = 4;
                            continue;
                        }
                    case 5:
                        {
                            Console.WriteLine("Сортировка студентов"); //0
                            Console.WriteLine("  по номеру (по возрастанию)"); //1
                            Console.WriteLine("  по номеру (по убыванию)"); //2
                            Console.WriteLine("  по ФИО (по возрастанию)"); //3
                            Console.WriteLine("  по ФИО (по убыванию)"); //4
                            Console.WriteLine("  по группе (по возрастанию)"); //5
                            Console.WriteLine("  по группе (по убыванию)"); //6
                            Console.Write("  Назад"); //7
                            Console.SetCursorPosition(0, pos); //установка курсора на текущую позицию
                            Console.Write(">"); //отрисовка указателя
                            TransformingOrEnter(Console.ReadKey()); //обработка нажатия клавиши
                            continue;
                        }
                }
            }
        }
    }
}
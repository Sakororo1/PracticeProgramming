using Microsoft.Data.Sqlite;
namespace task9_1
{
    internal class WorkStudents
    {
        WorkBase wb;
        Loading l; //объект для загрузки из файла
        public WorkStudents()
        {
            wb = new WorkBase();
            l = new Loading();
        }
        //работа с базой данных
        public List<Student> LoadFromBase() //загрузка информации из базы данных
        {
            wb.LoadFromBase(); //загрузка из базы данных
            return students;
        }
        public void AddStudent(string name,string group) //добавление студента
        {
            wb.AddStudent(name,group);
        }
        public void ChangeStudentName(int pos, string new_name)
        {
            wb.ChangeStudentName(pos, new_name);
        }
        public void ChangeStudentGroup(int pos, string new_group)
        {
            wb.ChangeStudentGroup(pos, new_group);
        }
        public void DeleteStudent(int pos) //удаление студента
        {
            wb.DeleteStudent(pos);
        }
        //работа с поиском
        public List<Student> SearchBy(string str)
        {
            Search s = new Search();
            return s.SearchBy(str);
        }
        //работа с сортировками
        public List<Student> SortBy(int pos)
        {
            Sort s= new Sort();
            return s.SortBy(pos);
        }
        //сохранение в файл
        public void SaveToTXT(string filename)
        {
            Saving saving= new Saving();
            saving.SaveToTXT(filename);
        }
        //загрузка из файла
        public List<Student> LoadFromFile(string filename)
        {
            return l.LoadFromTXT(filename);
        }
        public void AddToBase() //добавление загруженных записей в базу данных
        {
            l.AddToBase();
        }
        public struct Student //структура студент
        {
            public int id; //номер
            public string name; //фио
            public string group; //группа
            public Student(int ID, string Name, string Group) //конструктор
            {
                id = ID; name = Name; group = Group;
            }
        }
        static List<Student> students = new List<Student>(); //список студентов
        private class WorkBase //класс для работы с базой данных и хранения информации
        {
            const string c_ctr = "Data Source=Students.db"; //строка подключения
            //первое создание базы данных
            private void CreateNewBase()
            {
                using (SqliteConnection c = new SqliteConnection(c_ctr))
                {
                    c.Open(); //открытие базы данных
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        //создание таблицы со студентами
                        cmd.CommandText = "create table stud(id integer primary key autoincrement, name text not null, group_name text not null);";
                        cmd.ExecuteReader();
                    }
                }
            }
            //загрузка из базы данных
            public void LoadFromBase()
            {
                if (!File.Exists("Students.db")) //проверка, что базы данных не существует
                {
                    CreateNewBase(); //создание новой базы данных с таблицей
                }
                if (students.Count > 0) students.Clear();
                using (SqliteConnection c = new SqliteConnection(c_ctr))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = "select * from stud;";
                        using (SqliteDataReader r = cmd.ExecuteReader())
                        {
                            if (r.HasRows) //проверка, что есть данные
                            {
                                while (r.Read())
                                {
                                    if (r.GetValue(1) == null || r.GetValue(2) == null)
                                        return;
                                    //добавление записи о студенте в список студентов
                                    students.Add(new Student(Convert.ToInt32(r.GetValue(0).ToString()),
                                                                             r.GetValue(1).ToString(),
                                                                             r.GetValue(2).ToString()));
                                }
                            }
                        }
                    }

                }
            }
            //добавление студента
            public void AddStudent(string name, string group)
            {
                if (name == null || name == "" || group == null || group == "") return;
                using (SqliteConnection c = new SqliteConnection(c_ctr))
                {
                    c.Open(); //открытие базы данных
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        //добавление записи
                        cmd.CommandText = $"insert into stud values(null,'{name}','{group}');";
                        cmd.ExecuteReader();
                    }
                }
                LoadFromBase();
            }
            //редактирование фио
            public void ChangeStudentName(int pos, string new_name)
            {
                if (string.IsNullOrEmpty(new_name)) return;
                int index = pos - 2; //индекс меньше на 2, т.к. выше две строки
                int id = students[index].id; //определение номера студента
                using (SqliteConnection c = new SqliteConnection(c_ctr))
                {
                    c.Open(); //открытие базы данных
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        //изменение имени студента
                        cmd.CommandText = $"update stud set name='{new_name}' where id={id};";
                        cmd.ExecuteReader();
                    }
                }
                LoadFromBase();
            }
            //редактирование группы
            public void ChangeStudentGroup(int pos, string new_group)
            {
                if (string.IsNullOrEmpty(new_group)) return;
                int index = pos - 2; //индекс меньше на 2, т.к. выше две строки
                int id = students[index].id; //определение номера студента
                using (SqliteConnection c = new SqliteConnection(c_ctr))
                {
                    c.Open(); //открытие базы данных
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        //изменение имени студента
                        cmd.CommandText = $"update stud set group_name='{new_group}' where id={id};";
                        cmd.ExecuteReader();
                    }
                }
                LoadFromBase();
            }
            //удаление
            public void DeleteStudent(int pos)
            {
                int index = pos - 2; //индекс меньше на 2, т.к. выше две строки
                if (index < 0 || index >= students.Count) return;
                int id = students[index].id; //определение номера студента
                using (SqliteConnection c = new SqliteConnection(c_ctr))
                {
                    c.Open(); //открытие базы данных
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        //удаление студента
                        cmd.CommandText = $"delete from stud where id={id};";
                        cmd.ExecuteReader();
                    }
                }
                LoadFromBase();
            }
        }
        private class Search //класс для поиска
        {
            public List<Student> SearchBy(string search_str) //поиск по строке
            {
                if (string.IsNullOrEmpty(search_str)) return null;
                List<Student> s = new List<Student>(); //список найденных записей
                for (int i = 0; i < students.Count; i++) //поиск по имени
                {
                    if (students[i].name.Contains(search_str))
                    {
                        s.Add(students[i]);
                    }
                }
                for (int i = 0; i < students.Count; i++) //поиск по группе
                {
                    if (s.Contains(students[i])) continue; //пропуск студентов, которые уже есть в списке
                    if (students[i].group.Contains(search_str))
                    {
                        s.Add(students[i]);
                    }
                }
                return s;
            }
        }
        private class Sort //класс для сортировки
        {
            public List<Student> SortBy(int pos) //выбор метода для сортировки
            {
                //Сортировки: 0
                //  По номеру (по возрастанию) 1
                //  По номеру (по убыванию) 2
                //  По фио (по возрастанию) 3
                //  По фио (по убыванию) 4
                //  По группе (по возрастанию) 5
                //  По группе (по убыванию) 6
                switch (pos)
                {
                    case 1:
                        return SortByIDUpper();
                    case 2:
                        return SortByIDLower();
                    case 3:
                        return SortByNameUpper();
                    case 4:
                        return SortByNameLower();
                    case 5:
                        return SortByGroupUpper();
                    case 6:
                        return SortByGroupLower();
                    default: return null;

                }
            }

            //сортировка простыми вставками
            private List<Student> SortByIDUpper() //сортировка по номеру по возрастанию
            {
                List<Student> s = new List<Student>();
                for (int i = 0; i < students.Count; i++) //копирование списка
                {
                    s.Add(students[i]);
                }
                //начало сортировки
                Student buf; //временное значение
                int j;
                for (int i = 1; i < s.Count; i++)
                {
                    buf = s[i]; //сохранение значение текущего элемента
                    j = i - 1; //индекс предыдущего элемента
                    while (j >= 0 && s[j].id > buf.id) //выбор места для текущего элемента
                    {
                        s[j + 1] = s[j]; //элемент перемещается на индекс назад
                        j--; //перемещение назад
                    }
                    s[j + 1] = buf;
                }
                return s;
            }
            private List<Student> SortByIDLower() //сортировка по номеру по убыванию
            {
                List<Student> s = new List<Student>();
                for (int i = 0; i < students.Count; i++) //копирование списка
                {
                    s.Add(students[i]);
                }
                //начало сортировки
                Student buf; //временное значение
                int j;
                for (int i = 1; i < s.Count; i++)
                {
                    buf = s[i]; //сохранение значение текущего элемента
                    j = i - 1; //индекс предыдущего элемента
                    while (j >= 0 && s[j].id < buf.id) //выбор места для текущего элемента
                    {
                        s[j + 1] = s[j]; //элемент перемещается на индекс назад
                        j--; //перемещение назад
                    }
                    s[j + 1] = buf;
                }
                return s;
            }
            private List<Student> SortByNameUpper() //сортировка по фио по возрастанию
            {
                List<Student> s = new List<Student>();
                for (int i = 0; i < students.Count; i++) //копирование списка
                {
                    s.Add(students[i]);
                }
                //начало сортировки
                Student buf; //временное значение
                int j;
                for (int i = 1; i < s.Count; i++)
                {
                    buf = s[i]; //сохранение значения текущего элемента
                    j = i - 1; //индекс предыдущего элемента
                    while (j >= 0 && s[j].name.CompareTo(buf.name) < 0) //выбор места для текущего элемента
                    {
                        s[j + 1] = s[j]; //элемент перемещается на индекс назад
                        j--; //перемещение назад
                    }
                    s[j + 1] = buf;
                }
                return s;
            }
            private List<Student> SortByNameLower() //сортировка по фио по убыванию
            {
                List<Student> s = new List<Student>();
                for (int i = 0; i < students.Count; i++) //копирование списка
                {
                    s.Add(students[i]);
                }
                //начало сортировки
                Student buf; //временное значение
                int j;
                for (int i = 1; i < s.Count; i++)
                {
                    buf = s[i]; //сохранение значения текущего элемента
                    j = i - 1; //индекс предыдущего элемента
                    while (j >= 0 && s[j].name.CompareTo(buf.name) > 0) //выбор места для текущего элемента
                    {
                        s[j + 1] = s[j]; //элемент перемещается на индекс назад
                        j--; //перемещение назад
                    }
                    s[j + 1] = buf;
                }
                return s;
            }
            private List<Student> SortByGroupUpper() //сортировка по группе по возврастанию
            {
                List<Student> s = new List<Student>();
                for (int i = 0; i < students.Count; i++) //копирование списка
                {
                    s.Add(students[i]);
                }
                //начало сортировки
                Student buf; //временное значение
                int j;
                for (int i = 1; i < s.Count; i++)
                {
                    buf = s[i]; //сохранение значения текущего элемента
                    j = i - 1; //индекс предыдущего элемента
                    while (j >= 0 && s[j].group.CompareTo(buf.group) < 0) //выбор места для текущего элемента
                    {
                        s[j + 1] = s[j]; //элемент перемещается на индекс назад
                        j--; //перемещение назад
                    }
                    s[j + 1] = buf;
                }
                return s;
            }
            private List<Student> SortByGroupLower() //сортировка по группе по убыванию
            {
                List<Student> s = new List<Student>();
                for (int i = 0; i < students.Count; i++) //копирование списка
                {
                    s.Add(students[i]);
                }
                //начало сортировки
                Student buf; //временное значение
                int j;
                for (int i = 1; i < s.Count; i++)
                {
                    buf = s[i]; //сохранение значения текущего элемента
                    j = i - 1; //индекс предыдущего элемента
                    while (j >= 0 && s[j].group.CompareTo(buf.group) < 0) //выбор места для текущего элемента
                    {
                        s[j + 1] = s[j]; //элемент перемещается на индекс назад
                        j--; //перемещение назад
                    }
                    s[j + 1] = buf;
                }
                return s;
            }
        }
        private class Saving //класс для сохранения в файл
        {
            public void SaveToTXT(string filename) //сохранение в текстовый файл
            {
                using (StreamWriter w = new StreamWriter(filename)) //открытие файла
                {
                    string s = ""; //строка для записи
                    for (int i = 0; i < students.Count; i++)
                    {
                        //значение записываемой строки
                        s = $"{students[i].id} - {students[i].name} - {students[i].group}";
                        w.WriteLine(s); //запись строки в файл
                    }
                }
            }
        }
        private class Loading //класс для загрузки из файла
        {
            List<Student> studs=new List<Student>();//загружаемый список
            public List<Student> LoadFromTXT(string filename)
            {
                try
                {
                    string s; //прочитанная строка
                    using (StreamReader r = new StreamReader(filename))
                    {
                        while (!r.EndOfStream) //проверка, что конец файла не достигнут
                        {
                            s = r.ReadLine(); //чтение строки
                            string[] parts = s.Split(" - "); //разделение строки
                            Student stud = new Student(Convert.ToInt32(parts[0]), parts[1], parts[2]);
                            studs.Add(stud); //добавление студента
                        }
                    }
                    return studs;
                }
                catch
                {
                    return null;
                }

            }
            public void AddToBase() //добавление загруженных студентов в базу данных
            {
                WorkBase w = new WorkBase();
                for (int i = 0; i < studs.Count; i++)
                {
                    w.AddStudent(studs[i].name,studs[i].group);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
namespace Task8
{
    internal class Model
    {
        public Model()
        {
            sqliteworker = new SQLiteWorker(); //инициализация объекта
        }
        SQLiteWorker sqliteworker; //объект для работы с базой данных
        WorkWord workword; //объект для работы с отгадыванием слова
        
        //использование методов SQLiteWorker
        public void GetWord() //получение слова
        {
            //получение случайного слова
            workword = new WorkWord(sqliteworker.GetWord()); //инициализация объекта для работы с отгадыванием
            OnGotWord(workword.GuessingString); //вызов события о получении слова
        }
        //использование WorkWord
        public void CheckLetter(char c)
        {
            if (workword.CheckLetter(c)) //проверка, что выбранная буква правильная
            {
                OnRightLetter(workword.GuessingString); //вызов события со строкой с угадыванием
                if (!workword.GuessingString.Contains("-")) //проверка, что слово угадано
                {
                    OnGuessedWord(); //сообщение об угаданном слове
                }
            }
            else
            {
                OnWrongLetter(); //вызов события о неправильной букве
                if (workword.MistakesCount>=6) //проверка, что ошибка была последняя
                {
                    OnFinishedHangman(workword.Word); //вызов события о проигрыше
                }
            }
        }
        public int GetMistakes() //получение количества ошибок
        {
            return workword.MistakesCount;
        }
        public void ResetAll() //сброс
        {
            workword = null; //удаление ссылки на объект для работы с угадыванием
        }
        private class SQLiteWorker //класс для работы с базой данных sqlite
        {
            public SQLiteWorker()
            {
                words = new List<string>(); //инициализация списка
            }
            private List<string> words; //список слов, которые можно загадать
            //строка для подключения
            const string c_str = "Data Source=words.db";
            private void GetWords() //получение списка слов
            {
                if (words.Count > 0)
                {
                    words.Clear();
                }
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open(); //открытие базы данных
                    using (SqliteCommand s = c.CreateCommand())
                    {
                        s.CommandText = "select * from words";
                        using (SqliteDataReader r = s.ExecuteReader())//получение информации
                        {
                            if (r.HasRows) //проверка, что есть информация
                            {
                                while (r.Read()) //чтение информации по строкам
                                {
                                    words.Add(r.GetValue(1).ToString()); //добавление слова в список
                                }
                            }
                        }
                    }
                }
            }
            public string GetWord() //получение случайного слова
            {
                if (words.Count == 0) //проверка, что список слов пуст
                {
                    GetWords(); //получение списка слов
                }
                Random r = new Random();
                int index = r.Next(0, words.Count); //получение случайного индекса
                return words[index]; //получение случайного слова
            }
        }
        private class WorkWord //класс для работы с отгадыванием слова
        {
            public WorkWord(string word)
            {
                fword = word; //определение слова
                ffoundIndexes = new List<int>(); //инициализация списков и строки
                fguessingString = new StringBuilder();
                for (int i = 0; i < fword.Length; i++) //заполнение строки
                {
                    fguessingString.Append("-"); //добавление символа подчёркивания
                }
            }
            private string fword; //загаданное слово
            private int fmistakesCount=0; //количество ошибок
            public int MistakesCount //свойство для количества ошибок
            {
                get { return fmistakesCount; } //доступ только для чтения
            }
            private StringBuilder fguessingString; //строка с отгадыванием
            private List<int> ffoundIndexes; //найденные индексы вхождения буквы в слово
            public string Word //свойство для загаданного слова, доступ только для чтения 
            {
                get { return fword; }
            }
            public string GuessingString //свойство строки с отгадыванием
            {
                get { return fguessingString.ToString(); } //доступ для чтения
            }
            public List<int> FoundIndexes //свойство для вхождений букв в слове
            {
                get { return ffoundIndexes; } //доступ только для чтения
            }
            
            public bool CheckLetter(char c) //проверка, правильную ли букву выбрал пользователь
            {
                if (!fword.Contains(c.ToString())) //проверка, что буквы нет в слове
                {
                    fmistakesCount++; //увеличение количества ошибок
                    return false; //неправильная буква
                }
                if (ffoundIndexes.Count > 0) //проверка, что список с индексами не пуст
                {
                    ffoundIndexes.Clear(); //очистка списка
                }
                StringBuilder s = new StringBuilder(fword); //копирование строки
                while (s.ToString().Contains(c.ToString())) //проверка, что в строке есть символ
                {
                    ffoundIndexes.Add(s.ToString().IndexOf(c)); //добавление индекса
                    fguessingString[s.ToString().IndexOf(c.ToString())] = c; //установка символа вместо подчёркивания
                    s.Replace(c.ToString(),"-", s.ToString().IndexOf(c.ToString()),1); //удаление символа
                }
                return true; //правильная буква
            }
        }
        public delegate void GuessingString(string str);
        public event GuessingString OnFinishedHangman; //событие о проигрыше
        public event GuessingString OnGotWord; //событие для передачи полученного слова
        public event GuessingString OnRightLetter; //событие для передачи новой строки с угадыванием
        public delegate void VoidNoArgs();
        public event VoidNoArgs OnWrongLetter; //событие для передачи информации, что буквы неправильные
        public event VoidNoArgs OnGuessedWord; //событие для передачи информации об успехе
    }
}

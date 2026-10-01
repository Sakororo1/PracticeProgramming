namespace task2_1_1
{
    class Program
    {
        //класс для обозначения человека
        class Person 
        {
            private string fName = ""; //имя
            private byte fAge = 0; //возраст
            private string fAdress = ""; //адрес
            //установка имени
            public bool SetName(string Name) 
            {
                //регулярное выражение, сначала заглавная буква, затем строчные
                Regex r = new Regex(@"^([A-Z]{1}[a-z]+)||([А-Я]{1}[а-я]+)$");
                if (r.IsMatch(Name)) //проверка, что строка является именем
                {
                    fName=Name; //установка имени
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //получение имени
            public string GetName()
            {
                return fName;  
            }
            //установка возраста
            public bool SetAge(byte Age)
            {
                if ((Age < 0)||(Age>150))
                {
                    return false;
                }
                else
                {
                    fAge = Age; //установка возраста
                    return true;
                }
            }
            //получение возраста
            public byte GetAge()
            {
                return fAge;
            }
            //установка адреса
            public bool SetAdress(string Adress)
            {
                if (Adress.Length < 2)
                {
                    return false;
                }
                else
                {
                    fAdress= Adress;
                    return true;
                }
            }
            //получение адреса
            public string GetAdress()
            {
                return fAdress;
            }
        }
        static void Main(string[] args)
        {
            Person p1 = new Person(); //создание объекта человек
            p1.SetName("Иннокентий"); //заполнение полей через методы
            p1.SetAge(50);
            p1.SetAdress("г. Минск, ул. Советская, д. 200");
            Person p2 = new Person(); //создание объекта человек 2
            p2.SetName("Алоиза"); //заполнение полей через методы
            p2.SetAge(51);
            p2.SetAdress("г. Орша, ул. Ленина, д. 50");
            Console.WriteLine("Информация о человеке 1:"); //вывод информации о человеке 1
            Console.WriteLine(p1.GetName());
            Console.WriteLine(p1.GetAge());
            Console.WriteLine(p1.GetAdress());
            Console.WriteLine("Информация о человеке 2:"); //вывод информации о человеке 2
            Console.WriteLine(p2.GetName());
            Console.WriteLine(p2.GetAge());
            Console.WriteLine(p2.GetAdress());
        }
    }
}

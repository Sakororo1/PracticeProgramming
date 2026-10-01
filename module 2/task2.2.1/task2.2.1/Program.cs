namespace task2_2_1
{
    class Program
    {
        class BankAccount //банковский счёт
        {
            static private uint fCountID = 0; //количество добавленных счетов
            private uint fID; //номер счёта, назначается при создании счёта
            private string fName = ""; //имя владельца
            private uint fMoney; //баланс

            public BankAccount(string name) //конструктор
            {
                fName = name;
                fMoney = 0; //изначально баланс = 0
                fID = fCountID;
                fCountID++; //увеличение количество счетов
            }
            public void PutMoney(uint change) //пополнение счёта
            {
                if (fMoney + change <= ushort.MaxValue)
                {
                    fMoney += change;
                }
                else
                {
                    Console.WriteLine("Операция отменена");
                }
            }
            public void TakeMoney(uint change) //снятие средств со счёта
            {
                if (fMoney - change >= ushort.MinValue)
                {
                    fMoney -= change;
                }
                else
                {
                    Console.WriteLine("Операция отменена");
                }
            }
            public uint GetBalance() //получение баланса
            {
                return fMoney;
            }
            public string GetName() //получение имени владельца
            {
                return fName;
            }
            public uint GetID() //получение номера счёта
            {
                return fID;
            }
        }
        static void Main(string[] args)
        {
            BankAccount b1 = new BankAccount("Бумажка Алексей Алексеевич"); //создание двух банковских счётов
            BankAccount b2 = new BankAccount("Букажка Людмила Алексеевна");
            //вывод информации о новых счетах
            Console.WriteLine($"Счёт 1. Имя: {b1.GetName()}, номер: {b1.GetID()}, баланс: {b1.GetBalance()}");
            Console.WriteLine($"Счёт 2. Имя: {b2.GetName()}, номер: {b2.GetID()}, баланс: {b2.GetBalance()}");
            Console.WriteLine("Пополнение счёта 1 500 у.е.д.");
            b1.PutMoney(500);
            Console.WriteLine($"Счёт 1. Имя: {b1.GetName()}, номер: {b1.GetID()}, баланс: {b1.GetBalance()}");
            Console.WriteLine("Снятие с счёта 1 200 у.е.д.");
            b1.TakeMoney(200);
            Console.WriteLine($"Счёт 1. Имя: {b1.GetName()}, номер: {b1.GetID()}, баланс: {b1.GetBalance()}");
        }
    }
}

namespace task3_2
{
    class Program
    {
        class Notifications //класс уведомлений
        {
            static private Notifications n;
            private delegate void Notify(); //делегат уведомления
            static private event Notify EMessage; //событие получения сообщения
            static private event Notify EMail; //событие получения почты
            static private event Notify ECall; //событие получения звонка

            static public Notifications GetInstance()
            {
                if (n==null)
                {
                    n = new Notifications();
                }
                return n;
            }
            private Notifications()
            {
                EMessage += NMessage; //привязка обработчиков
                EMail += NMail;
                ECall += NCall;
            }
            private Random r = new Random();
            public void GetNotification() //получение случайного уведомления
            {
                switch (r.NextInt64(1,4)) //выбор одного из трёх уведомлений
                {
                    case 1:
                        EMessage?.Invoke();
                        break;
                    case 2:
                        EMail?.Invoke();
                        break;
                    case 3:
                        ECall?.Invoke();
                        break;
                }
            }
            private void NMessage()
            {
                Console.WriteLine("Уведомление: Получено сообщение");
            }
            private void NMail()
            {
                Console.WriteLine("Уведомление. Получено письмо");
            }
            private void NCall()
            {
                Console.WriteLine("Звонок");
            }
        }
        static void Main(string[] args)
        {
            Notifications n = Notifications.GetInstance();
            do
            {
                n.GetNotification(); //получение случайного уведомления
            } while (Console.ReadLine() != "exit");
        }
    }
}

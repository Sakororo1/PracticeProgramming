namespace task3_3
{
    class Program
    {
        class Tasks //класс задач
        {
            private List<UserTask> tasks; //список задач
            private struct UserTask //структура задачи
            {
                public string Name; //название задачи
                public TaskHandler Handler; //делегат выполнения
            }
            private delegate void TaskHandler(string task); //делегат выполнения задачи
            public void AddTask() //добавление задачи
            {
                if (tasks == null)
                {
                    tasks = new List<UserTask>();
                }
                UserTask t;
                Console.Write("Введите название задачи: ");
                t.Name = Console.ReadLine();
                //выбор делегата
                Console.Write("Выберите способ выполнения (1 - уведомление, 2 - журнал, 3 - почта): ");
                switch (Console.ReadLine())
                {
                    case "1":
                        t.Handler = Notify;
                        break;
                    case "2":
                        t.Handler = Journal;
                        break;
                    case "3":
                        t.Handler = Mail;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        t.Handler = Notify;
                        break;
                }
                tasks.Add(t); //добавление задачи
                Console.WriteLine("Задача добавлена.");
            }

            //вывод всех задач
            public void PrintAll()
            {
                if (tasks == null) return;
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"номер: {i}, название: {tasks[i].Name}");
                }
            }           
            public void ExecuteTask() //выполнение задачи
            {
                if (tasks == null) return;
                Console.Write("Введите номер задачи: ");
                int n = Convert.ToInt32(Console.ReadLine());
                if ((n < 0) || (n >= tasks.Count))
                {
                    Console.WriteLine("Неверный номер");
                    return;
                }
                tasks[n].Handler?.Invoke(tasks[n].Name); //вызов делегата
            }
            //обработчики
            private void Notify(string task) //уведомление
            {
                Console.WriteLine($"Уведомление: задача \"{task}\" выполнена");
            }
            private void Journal(string task) //журнал
            {
                Console.WriteLine($"Журнал: {DateTime.Now:HH:mm:ss} — \"{task}\"");
            }
            private void Mail(string task) //письмо
            {
                Console.WriteLine($"Почта - Письмо о \"{task}\" отправлено");
            }
        }
        static void Main(string[] args)
        {
            Tasks t = new Tasks();
            string choice;
            do
            {
                Console.Write("1 - добавить, 2 - показать, 3 - выполнить, exit - выход: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": //1 - добавление задания
                        t.AddTask();
                        break;
                    case "2": //2 - вывод всех заданий
                        t.PrintAll();
                        break;
                    case "3": //3 - выполнение задания
                        t.ExecuteTask();
                        break;
                    case "exit":
                        break;
                    default:
                        Console.WriteLine("Неверно");
                        break;
                }
            } while (choice != "exit");
        }
    }
}
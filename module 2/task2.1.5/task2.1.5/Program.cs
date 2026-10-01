namespace task2_1_5
{
    class Program
    {
        //сенсор температуры
        class TemperatureSensor
        {
            private static TemperatureSensor instance;
            private TemperatureSensor() //конструктор
            {

            }
            public static TemperatureSensor getInstance()
            {
                if (instance == null) instance = new TemperatureSensor();
                return instance;
            }
            private int Temperature = 0; //поле температура, по умолчанию 0
            public delegate void Notify(int Temp); //делегат
            static public event Notify TemperatureChanged; //событие
            public void ChangeTemperature(short change) //изменение температуры
            {
                Temperature += change; //изменение температуры
                TemperatureChanged?.Invoke(Temperature); //вызов события
            }
        }
        //Класс термостат
        class Thermostat
        {
            private static Thermostat instance;
            public static Thermostat getInstance()
            {
                if (instance == null) instance = new Thermostat();
                return instance;
            }
            private Thermostat() //конструктор, подписывающи обработчик на событие
            {
                TemperatureSensor.TemperatureChanged += HandleHeating;
            }
            //обработчик события, выключающий или отключающий отопление
            public void HandleHeating(int Temperature)
            {
                if (Temperature < 6) //проверка, что температура меньше 6
                {
                    Console.WriteLine($"Температура: {Temperature}. Отопление включено");
                }
                else
                {
                    Console.WriteLine($"Температура: {Temperature}. Отопление выключено");
                }

            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Изначально температура: 0");
            Random r = new Random(); //рандом
            TemperatureSensor TS = TemperatureSensor.getInstance(); //получение объекта сенсора
            Thermostat ThS= Thermostat.getInstance(); //получение объекта термостат
            short change=0; //изменение температуры
            while (true)
            {
                change = (short)r.NextInt64(-40,40); //определение изменения температуры
                TS.ChangeTemperature(change);
                Console.ReadLine();
            }
        }
    }
}

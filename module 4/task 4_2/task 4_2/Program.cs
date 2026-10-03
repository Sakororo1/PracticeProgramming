namespace task4_2
{
    class Program
    {
        interface IProduct //интерфейс товар
        {
            public double GetPrice(); //стоимость товара
            public int GetCount(); //количество товара на складе
            public double GetAllPrice(); //общая стоимость всего товара на складе
        }
        class Table : IProduct //столы
        {
            private const double fPrice = 628.0;
            private static ushort Count = 0; //количество тарелок
            public Table() //конструктор, указывается количество товара
            {
                Count++;
            }
            public double GetPrice()
            {
                return fPrice;
            }
            public int GetCount()
            {
                return Count;
            }
            public double GetAllPrice()
            {
                return Count * fPrice;
            }
        }
        class Chair : IProduct //стулья
        {
            private const double fPrice = 212;
            private static ushort Count = 0; //количество тарелок
            public Chair() //конструктор, указывается количество товара
            {
                Count++;
            }
            public double GetPrice()
            {
                return fPrice;
            }
            public int GetCount()
            {
                return Count;
            }
            public double GetAllPrice()
            {
                return Count * fPrice;
            }
        }
        class Fork : IProduct //вилки
        {
            private const double fPrice = 10.16;
            private static ushort Count = 0; //количество тарелок
            public Fork() //конструктор, указывается количество товара
            {
                Count++;
            }
            public double GetPrice()
            {
                return fPrice;
            }
            public int GetCount()
            {
                return Count;
            }
            public double GetAllPrice()
            {
                return Count * fPrice;
            }
        }
        class Plate : IProduct //тарелки
        {
            private const double fPrice = 22.09;
            private static ushort Count = 0; //количество тарелок
            public Plate() //конструктор, указывается количество товара
            {
                Count++;
            }
            public double GetPrice()
            {
                return fPrice;
            }
            public int GetCount()
            {
                return Count;
            }
            public double GetAllPrice()
            {
                return Count * fPrice;
            }
        }
        static void Main(string[] args)
        {
            IProduct[] products = new IProduct[100]; //массив продуктов количеством 100
            Random r = new Random();
            int t = -1; int c = -1; int f = -1; int p = -1; //указатели на элементы массива
            for (int i = 0; i < products.Length; i++)
            {
                switch (r.Next(1, 5))
                {
                    case 1: //1 - стол
                        products[i] = new Table();
                        t = i;
                        break;
                    case 2: //2 - стул
                        products[i] = new Chair();
                        c = i;
                        break;
                    case 3: //3 - вилка
                        products[i] = new Fork();
                        f = i;
                        break;
                    case 4: //4 - тарелка
                        products[i] = new Plate();
                        p = i;
                        break;
                }
            }
            //столы
            if (t != -1) //проверка, что столы есть среди товаров
            {
                Console.WriteLine($"Всего добавлено столов: {products[t].GetCount()}");
                Console.WriteLine($"Цена стола: {products[t].GetPrice()}");
                Console.WriteLine($"Общая цена столов: {products[t].GetAllPrice()}");
            }
            //стулья
            if (c != -1) //проверка, что стулья есть среди товаров
            {
                Console.WriteLine($"Всего добавлено стульев: {products[c].GetCount()}");
                Console.WriteLine($"Цена стула: {products[c].GetPrice()}");
                Console.WriteLine($"Общая цена стульев: {products[c].GetAllPrice()}");
            }
            //вилки
            if (f != -1) //проверка, что вилки есть среди товаров
            {
                Console.WriteLine($"Всего добавлено вилок: {products[f].GetCount()}");
                Console.WriteLine($"Цена вилки: {products[f].GetPrice()}");
                Console.WriteLine($"Общая цена вилок: {products[f].GetAllPrice()}");
            }
            //тарелки
            if (p != -1) //проверка, что тарелки есть среди товаров
            {
                Console.WriteLine($"Всего добавлено тарелок: {products[p].GetCount()}");
                Console.WriteLine($"Цена тарелки: {products[p].GetPrice()}");
                Console.WriteLine($"Общая цена тарелок: {products[p].GetAllPrice()}");
            }
        }
    }
}

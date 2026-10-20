using System.Windows;
using System.Windows.Controls;
namespace Task8
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Presenter p;
        public Button[] buttons;
        const string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            p = Presenter.GetInstance(this);
            buttons = new Button[33]; //инициализация массива из 33 кнопок
            int x = 30; //координаты для размещения кнопок
            int y = 120;
            for (int i = 0; i < buttons.Length; i++) //определение кнопок массива
            {
                buttons[i] = new Button(); //инициализация кнопки
                buttons[i].Name = $"button{i}";
                buttons[i].Width = 50;
                buttons[i].Height = 40;
                buttons[i].VerticalAlignment = VerticalAlignment.Bottom;
                buttons[i].HorizontalAlignment = HorizontalAlignment.Left;
                buttons[i].Visibility = Visibility.Visible;
                buttons[i].Tag = i; //определение тега
                buttons[i].Content = alphabet[i]; //определение буквы
                buttons[i].Click += Buttons_Click;
                buttons[i].IsEnabled = false; //по умолчанию все кнопки отключены
                buttons[i].Margin = new Thickness(x,0,0,y); //определение расположения
                x += 60; //перемещение вправо
                if ((i+1)%11==0) //проверка, что выведена одна строка
                {
                    x = 30;  //возвращение влево
                    y -= 50; //перемещение вниз
                }
                Grid1.Children.Add(buttons[i]); //добавление кнопки в сетку
            }

        }
        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            p.CheckLetter((int)b.Tag);
        }
        private void Button_make_new_word_Click(object sender, RoutedEventArgs e)
        {
            p.ResetAll();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
namespace task5_1
{
    public partial class Form1 : Form
    {
        class Drawing //класс для рисования
        {
            private static Drawing Instance; //объект класса
            private Graphics _g; //объект для рисования
            private Pen _p; //карандаш
            private Panel _panel; //ссылка на панель
            private Drawing(Panel panel) //конструктор
            {
                _panel = panel;
                _g = panel.CreateGraphics();
                _p = new Pen(Color.Black);
            }
            public static Drawing GetInstance(Panel panel)
            {
                if (Instance == null)
                {
                    Instance = new Drawing(panel);
                }
                return Instance;
            }
            public void ChangeColor(Color c) //изменение цвета
            {
                _p.Color = c;
            }
            public void DrawCircle(int x1, int y1, int x2, int y2) //рисование круга
            {
                int _x1=Math.Min(x1,x2); //определение координат 1 точки
                int _y1=Math.Min(y1,y2);
                int _x2=Math.Max(x2,x1); //определение координат 2 точки
                int _y2=Math.Max(y2,y1);
                int w = Math.Min(_x2 - _x1,_y2-_y1); //определение стороны как наименьшее из ширины и высоты
                _g.DrawEllipse(_p, _x1,_y1,w, w);
            }
            public void DrawLine(int x1, int y1, int x2, int y2) //рисование линии
            {
                _g.DrawLine(_p, x1, y1, x2, y2);
            }
            public void DrawSquare(int x1, int y1, int x2, int y2) //рисование квадрата
            {
                int _x1 = Math.Min(x1, x2); //определение координат 1 точки
                int _y1 = Math.Min(y1, y2);
                int _x2 = Math.Max(x2, x1); //определение координат 2 точки
                int _y2 = Math.Max(y2, y1);
                int w = Math.Min(_x2 - _x1, _y2 - _y1); //определение стороны как наименьшее из ширины и высоты
                _g.DrawRectangle(_p, _x1, _y1, w, w);
            }
            public void ChangeWidth(int value)
            {
                _p.Width= value;
            }
        }
        Drawing d; //объект для рисования
        int x1; int y1; //координаты первой точки
        int x2; int y2; //координаты второй точки
        byte choice; //выбор фигуры; 1 - линия, 2 - круг, 3 - квадрат
        public Form1()
        {
            InitializeComponent();
        }
        private void button_Color_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog(); //вызов окна для выбора цвета
            d.ChangeColor(colorDialog1.Color); //изменение цвета
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x2 = e.X; //определение координат второй точки
            y2 = e.Y;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            d = Drawing.GetInstance(panel1); //создание объекта для рисования
            colorDialog1.Color = Color.Black; //цвет по умолчанию - чёрный
            choice = 1;
            label1.Text = "Рисование линий";
            button_Line.BackColor = Color.CadetBlue; //по умолчанию выбрано рисование линий
            button_Circle.BackColor = Color.White;
            button_Square.BackColor = Color.White;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (choice)
            {
                case 1: //1 - линия
                    d.DrawLine(x1, y1, x2, y2);
                    break;
                case 2: //2 - круг
                    d.DrawCircle(x1, y1, x2, y2);
                    break;
                case 3: //3 - квадрат
                    d.DrawSquare(x1, y1, x2, y2);
                    break;
            }
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            x1 = e.X; //определение координат первой точки
            y1 = e.Y;
        }
        //выбор рисования линий
        private void button_Line_Click(object sender, EventArgs e)
        {
            choice = 1;
            label1.Text = "Рисование линий";
            button_Circle.BackColor = Color.White;
            button_Square.BackColor = Color.White;
            button_Line.BackColor = Color.CadetBlue;
        }
        //выбор рисования кругов
        private void button_Circle_Click(object sender, EventArgs e)
        {
            choice = 2;
            label1.Text = "Рисование кругов";
            button_Circle.BackColor = Color.CadetBlue;
            button_Square.BackColor = Color.White;
            button_Line.BackColor = Color.White;
        }
        //выбор рисования квадратов
        private void button_Square_Click(object sender, EventArgs e)
        {
            choice = 3;
            label1.Text = "Рисование квадратов";
            button_Circle.BackColor = Color.White;
            button_Square.BackColor = Color.CadetBlue;
            button_Line.BackColor = Color.White;
        }
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            d.ChangeWidth(trackBar1.Value);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text="Ширина: "+trackBar1.Value.ToString();
        }
    }
}

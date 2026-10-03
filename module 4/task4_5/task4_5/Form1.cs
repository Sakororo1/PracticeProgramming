using System;
using System.Drawing;
using System.Windows.Forms;
namespace task4_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        interface IDrawing //интерфейс для рисования
        {
            void DrawCircle(int x,int y,int R); //рисование круга
            void DrawLine(int x1,int y1,int x2, int y2); //рисование линии
            void DrawRectangle(int x,int y,int a, int b); //рисование прямоугольника
        }
        class DrawCanvas : IDrawing
        {
            private Panel panel; //ссылка на панель
            Graphics g; //объект для рисования
            Pen p;
            static private DrawCanvas Instance;
            private DrawCanvas(Panel d_panel) //конструктор, принимает ссылку на панель для рисования
            {
                panel=d_panel;
                g=d_panel.CreateGraphics(); //создание объекта для рисования
                p = new Pen(Color.Aqua,2f);
            }
            private void PenRandomColor() //случайное изменение цвета
            {
                Random r = new Random();
                switch (r.Next(1,11)) //выбор цвета на основе случайного значения
                {
                    case 1: 
                        p.Color= Color.Red;
                        break;
                    case 2:
                        p.Color = Color.Aqua;
                        break;
                    case 3:
                        p.Color = Color.Black;
                        break;
                    case 4:
                        p.Color = Color.PaleVioletRed;
                        break;
                    case 5:
                        p.Color = Color.Brown;
                        break;
                    case 6:
                        p.Color = Color.Orange;
                        break;
                    case 7:
                        p.Color = Color.Cyan;
                        break;
                    case 8:
                        p.Color = Color.Green;
                        break;
                    case 9:
                        p.Color = Color.Violet;
                        break;
                    case 10:
                        p.Color = Color.Pink;
                        break;
                }
            }

            static public DrawCanvas GetInstance(Panel d_panel)
            {
                if (Instance==null)
                {
                    Instance = new DrawCanvas(d_panel);
                }
                return Instance;
            }
            public void DrawCircle(int x, int y, int R) //рисование круга
            {
                PenRandomColor();
                Rectangle r = new Rectangle(x,y,R,R);
                g.DrawEllipse(p,r);
            }
            public void DrawLine(int x1, int y1, int x2, int y2) //рисование линии
            {
                PenRandomColor();
                Point point1=new Point(x1,y1);
                Point point2=new Point(x2,y2);
                g.DrawLine(p, point1, point2);
            }
            public void DrawRectangle(int x, int y, int a, int b) //рисование прямоугольника
            {
                PenRandomColor();
                g.DrawRectangle(p, new Rectangle(x, y, a, b));
            }
        }
        IDrawing drawing; //объект для рисования на форме
        private void Form1_Load(object sender, EventArgs e)
        {
            drawing = DrawCanvas.GetInstance(panel1);
        }
        private void button_Line_Click(object sender, EventArgs e)
        {
            drawing.DrawLine((int)numericUpDown_x1.Value,(int)numericUpDown_y1.Value,
                (int)numericUpDown_x2.Value,(int)numericUpDown_y2.Value);
        }
        private void button_Circle_Click(object sender, EventArgs e)
        {
            drawing.DrawCircle((int)numericUpDown_Circle_x.Value, (int)numericUpDown_Circle_y.Value,
                (int)numericUpDown_Radius.Value);
        }
        private void button_Rectangle_Click(object sender, EventArgs e)
        {
            drawing.DrawRectangle((int)numericUpDown_Rect_x.Value, (int)numericUpDown_Rect_y.Value,
                (int)numericUpDown_a.Value, (int)numericUpDown_b.Value);
        }
    }
}

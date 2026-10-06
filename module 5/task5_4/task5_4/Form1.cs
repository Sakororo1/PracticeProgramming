using System;
using System.Drawing;
using System.Windows.Forms;

namespace task5_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class PictureShowing
        {
            static PictureShowing Instance;
            private PictureShowing(PictureBox pb, Panel p) //конструктор, принимает ссылку на picturebox
            {
                _pb = pb;
                _p = p;
            }
            public static PictureShowing GetInstance(PictureBox pb, Panel p)
            {
                if (Instance == null)
                {
                    Instance = new PictureShowing(pb, p);
                }
                return Instance;
            }
            PictureBox _pb; //ссылка на объект с изображением
            Panel _p; //ссылка на панель
            public void OpenPicture(string path) //открытие изображения
            {
                _pb.Image = Image.FromFile(path); //открытие изображения по пути к файлу
                if (_pb.Image == null)
                {
                    return;
                }
                _pb.Width = _pb.Image.Width; //изменение ширины
                _pb.Height = _pb.Image.Height; //изменение высоты
                CenterPicture(); //перемещение изображения в центр
            }
            public void CenterPicture() //перемещение изображения в центр
            {
                _pb.Top = _p.Height / 2 - _pb.Height / 2;
                _pb.Left = _p.Width / 2 - _pb.Width / 2;
            }
            public void ResizePicture(int val) //масштабирование изображения
            {
                if (_pb.Image == null) //проверка, что изображение есть
                {
                    return;
                }
                _pb.Width = _pb.Image.Width * (val / 100);
                _pb.Height = _pb.Image.Height * (val / 100);
                CenterPicture();
            }
        }
        PictureShowing ps;
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Parent = panel1;
            ps = PictureShowing.GetInstance(pictureBox1, panel1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != "")
            {
                ps.OpenPicture(openFileDialog1.FileName);
            }
            trackBar1.Value = 100;
            label1.Text = "Масштаб: 100%";
        }
        private void panel1_Resize(object sender, EventArgs e)
        {
            ps.CenterPicture();
        }
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            ps.ResizePicture(trackBar1.Value);
            label1.Text = "Масштаб: " + trackBar1.Value + "%";
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Width>panel1.Width || pictureBox1.Height>panel1.Height)
            {
                pictureBox1.MouseMove += PictureBox1_MouseMove;
                pictureBox1.Cursor = Cursors.Default;
            }
        }
        int x; int y; //предыдущие координаты мыши
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (x > e.X)
            {
                pictureBox1.Left -= 10;
            }
            if (x < e.X)
            {
                pictureBox1.Left += 10;
            }
            if (y > e.Y)
            {
                pictureBox1.Top -= 10;
            }
            if (y < e.Y)
            {
                pictureBox1.Top += 10;
            }
            x = e.X;
            y = e.Y;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.MouseMove -= PictureBox1_MouseMove;
            pictureBox1.Cursor = Cursors.SizeAll;
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.MouseMove -= PictureBox1_MouseMove;
            pictureBox1.Cursor = Cursors.SizeAll;
        }
    }
}

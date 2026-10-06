using System;
using System.IO;
using System.Windows.Forms;
namespace task5_2
{
    public partial class Form1 : Form
    {
        class FileWork //класс для работы с файлами
        {
            static private FileWork Instance;
            TextBox textBox; //ссылка на текстовое поле
            public static FileWork GetInstance(TextBox tb)
            {
                if (Instance == null)
                {
                    Instance = new FileWork(tb);
                }
                return Instance;
            }
            private FileWork(TextBox tb) //конструктор, принимает ссылку на текстовое поле
            {
                textBox = tb;
            }
            public void OpenTextFile(string filename) //открытие файла
            {
                textBox.Text = File.ReadAllText(filename, System.Text.Encoding.UTF8);
            }
            public void SaveTextFile(string filename) //сохрание файла
            {
                File.WriteAllText(filename, textBox.Text, System.Text.Encoding.UTF8); //запись в файл
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        FileWork f;
        private void button_open_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.CheckFileExists&&openFileDialog1.FileName.EndsWith(".txt")) //проверка имени файла
            {
                f.OpenTextFile(openFileDialog1.FileName);
            }
        }
        private void button_save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                f.SaveTextFile(saveFileDialog1.FileName);
                       
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            f = FileWork.GetInstance(textBox1);
        }

    }
}

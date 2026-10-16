using SixLabors.ImageSharp.PixelFormats;
using SD=System.Drawing;
using SLI=SixLabors.ImageSharp;
using CTAA;
namespace WinFormsApp1
{
    public struct SymSet //тип набора символов
    {
        public string fName; //имя набора
        public string fSet; //строка набора
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SymSet[] sets; //наборы символов
        public bool IsGif; //является ли формат изображения gif
        public List<SD.Image> Images = new List<SD.Image>(); //список изображений
        public List<SLI.Image<Rgba32>> sharpImages = new List<SLI.Image<Rgba32>>(); //список изображений ImageSharp
        public int CurrentFrameIndex = 0; //номер текущего кадра
        FileWork fileWorker; //объект для работы с файлами
        ImageChanger imgChanger; //объект для изменения изображения
        public ResultCreator resCreator; //объект для составления результата
                   
        //нажатие кнопки для загрузки файла
        private void button_load_file_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK && openFileDialog1.FileName != "")
            {
                IsGif = false;
                if (fileWorker.LoadUserImage(openFileDialog1.FileName))
                {
                    button_copy.Enabled = true; //включение компонентов и изменение цвета кнопок
                    button_copy.BackColor = SD.Color.Indigo;
                    button_invert.Enabled = true;
                    button_invert.BackColor = SD.Color.Indigo;
                    button_save.Enabled = true;
                    button_save.BackColor = SD.Color.Indigo;
                    trackBar_contrast.Enabled = true;
                    trackBar_size.Enabled = true;
                    comboBox1.Enabled = true;
                    comboBox1.SelectedIndex = 0; //текущий выбранный 
                    numericUpDown1.Enabled = IsGif; //включение счётчика только если загружен файл GIF
                    numericUpDown1.Value = 0;
                    imgChanger.UpdateImage(trackBar_size.Value, 0, comboBox1.SelectedIndex, 0); //составление результата
                }
            }
        }
        //загрузка формы
        private void Form1_Load(object sender, EventArgs e)
        {
            fileWorker = new FileWork(this); //инициализация объектов
            imgChanger = new ImageChanger(this);
            resCreator = new ResultCreator(this);
            sets = fileWorker.LoadSetFile(); //загрузка наборов из файла
            for (byte i = 0; i < sets.Length; i++)
            {
                comboBox1.Items.Add(sets[i].fName); //добавление названия в меню
            }
        }
        //нажатие кнопки для сохранения TXT
        private void button_save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                fileWorker.SaveTXT(saveFileDialog1.FileName); //сохранение файла TXT
            }
        }
        private void trackBar_size_Scroll(object sender, EventArgs e)
        {
            label_size.Text = $"Размер: {trackBar_size.Value}";
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            imgChanger.UpdateImage(trackBar_size.Value, trackBar_contrast.Value, comboBox1.SelectedIndex, CurrentFrameIndex);
        }
        private void button_invert_Click(object sender, EventArgs e)
        {
            imgChanger.Invert(); //инвертирование изображения
        }
        private void trackBar_contrast_Scroll(object sender, EventArgs e)
        {
            label_contrast.Text = $"Контрастность: {trackBar_contrast.Value}";
        }
        //копирование результата
        private void button_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
        //перетаскивание файла в окно
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1) //проверка, что загружается только один файл
                    e.Effect = DragDropEffects.Copy; //копирование, иначе ничего
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;
        }
        //загрузка файла в окно
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); //массив строк путей с одним элементом
            if (files != null && files.Length > 0) //проверка, что файлы есть
            {
                fileWorker.LoadUserImage(files[0]); //загрузка файла
            }
        }
        //использование одного из ползунков
        private void trackBars_update_image(object sender, MouseEventArgs e)
        {
            imgChanger.UpdateImage(trackBar_size.Value, trackBar_contrast.Value, comboBox1.SelectedIndex, CurrentFrameIndex);
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (timer1.Enabled) return; //отмена действия, если таймер работает
            timer1.Enabled = true; //запуск таймера
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false; //отключение таймера
            CurrentFrameIndex = (int)numericUpDown1.Value; //изменение индекса кадра
            pictureBox1.Image = Images[CurrentFrameIndex]; //
            imgChanger.UpdateImage(trackBar_size.Value, trackBar_contrast.Value, comboBox1.SelectedIndex, CurrentFrameIndex);
        }
        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, "help.chm", HelpNavigator.Topic, "index.htm");
        }
    }
}
using System;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SDI = System.Drawing.Imaging;
using SLI = SixLabors.ImageSharp;
using WinFormsApp1;
using SD = System.Drawing;
using SixLabors.ImageSharp.Processing;
namespace CTAA
{
    public class FileWork //работа с файлами
    {
        private Form1 _form;
        public FileWork(Form1 form) //конструктор класса
        {
            _form = form; //ссылка на форму
        }
        IImageFormat detectedFormat; //определённый формат изображения
        public bool LoadUserImage(string fileName) //загрузка файла пользователем
        {
            if (IsImageCorrect(fileName)) //проверка корректности файла
            {
                _form.label_file_name.Text = "Изображение: " + fileName; //добавление имени файла
                SD.Image img = SD.Image.FromFile(fileName); //создание объекта файла
                if (_form.Images.Count != 0) //очистка списков изображений
                {
                    _form.Images.Clear();
                }
                if (_form.sharpImages.Count != 0)
                {
                    _form.sharpImages.Clear();
                }
                if (detectedFormat == GifFormat.Instance) //проверка, что файл имеет формат GIF
                {
                    SDI.FrameDimension dim = new SDI.FrameDimension(img.FrameDimensionsList[0]); //создание объекта кадра
                    int frameCount = img.GetFrameCount(dim); //количество кадров в файле
                    for (int i = 0; i < frameCount; i++) //добавление кадров в список изображений
                    {
                        img.SelectActiveFrame(dim, i); //выбор i-го кадра
                        Bitmap frame = new Bitmap(img.Width, img.Height); //создание объекта кадра для копии
                        using (Graphics g = Graphics.FromImage(frame))
                            g.DrawImage(img, 0, 0); //копия i-го кадра
                        _form.Images.Add(frame);  //добавление кадра
                        using (MemoryStream m = new MemoryStream())
                        {
                            frame.Save(m, SDI.ImageFormat.Png); //временное сохранение png-файла
                            m.Position = 0;
                            var imgSharp = SLI.Image.Load<Rgba32>(m); //загрузка сохранённого файла как объект ImageSharp 
                            imgSharp.Mutate(x => x.BackgroundColor(SLI.Color.White)); //добавление белого фона для изображения с альфа-каналом
                            _form.sharpImages.Add(imgSharp); //добавление изменённого изображения в список
                        }
                    }
                    _form.IsGif = true; //загружен файл GIF
                    _form.numericUpDown1.Maximum = frameCount - 1; //установка максимального значения счётчика как количество кадров
                    _form.numericUpDown1.Value = 0;
                }
                else
                {
                    Bitmap bmp = new Bitmap(img);
                    _form.Images.Add(bmp); //добавление изображения в список
                    using (var ms = new MemoryStream()) // Добавление в список SixLabors.ImageSharp.Image
                    {
                        bmp.Save(ms, SDI.ImageFormat.Png); //временное сохранение png-файла
                        ms.Position = 0;
                        var imgSharp = SLI.Image.Load<Rgba32>(ms);
                        imgSharp.Mutate(x => x.BackgroundColor(SLI.Color.White)); //добавление белого фона дляя изображения с альфа-каналом
                        _form.sharpImages.Add(imgSharp); //добавление изменённого изображения в список
                    }
                    _form.Images.Add(new Bitmap(img));
                    _form.IsGif = false;
                }
                _form.CurrentFrameIndex = 0; //текущий кадр - первый
                _form.pictureBox1.Image = _form.Images[0];
                //изменение размера
                if (_form.sharpImages[0].Width < 10) //проверка, что ширина изображения меньше 10
                {
                    int h_old = _form.sharpImages[0].Height; //получение значения текущей высоты
                    int h_new = h_old * 10 / _form.sharpImages[0].Width;//новая высота изображения
                    for (int i = 0; i < _form.sharpImages.Count; i++)
                    {
                        _form.sharpImages[i].Mutate(x => x.Resize(10, h_new)); //изменение размера каждого кадра в списке
                    }
                }
                else if (_form.sharpImages[0].Width > 500) //проверка, что ширина изображения больше 500
                {
                    int h_old = _form.sharpImages[0].Height; //получение значения текущей высоты
                    int h_new = h_old * 500 / _form.sharpImages[0].Width;//новая высота изображения
                    for (int i = 0; i < _form.sharpImages.Count; i++)
                    {
                        _form.sharpImages[i].Mutate(x => x.Resize(500, h_new)); //изменение размера каждого кадра в списке
                    }
                }
                _form.trackBar_size.Value = _form.sharpImages[0].Width; //значение ползунка равно значению ширины изображения
                _form.label_size.Text = $"Размер: {_form.sharpImages[0].Width}";
                _form.trackBar_contrast.Value = 0; //значение контрастности изначально 0
                _form.label_contrast.Text = "Контрастность: 0";
                _form.trackBar_contrast.Enabled = true; _form.trackBar_size.Enabled = true; //включение ползунков
                return true;
            }
            return false;

        }
        private bool IsImageCorrect(string fileName) //проверка корректности файла
        {
            try
            {
                byte[] data = File.ReadAllBytes(fileName); //чтение байтов из файла
                detectedFormat = SLI.Image.DetectFormat(data); //проверка формата
                if (detectedFormat == null) //неопределённый формат
                {
                    MessageBox.Show("Не удалось определить формат (файл повреждён или не изображение).");
                    return false;
                }
                if (detectedFormat != JpegFormat.Instance && detectedFormat != PngFormat.Instance && detectedFormat != GifFormat.Instance
                    && detectedFormat != BmpFormat.Instance)
                {
                    MessageBox.Show($"Формат {detectedFormat.Name} не поддерживается.");
                    return false;
                }
                SLI.ImageInfo info = SLI.Image.Identify(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: файл повреждён или не является изображением. {ex.Message}");
                return false;
            }
            return true;
        }
        private void CreateSetFile() //создание файла с наборами, вызывается только LoadSetFile
        {
            SymSet[] sets = new SymSet[6]; //инициализация наборов
            for (byte i = 0; i < sets.Length; i++) sets[i] = new SymSet();  //инициализация всех наборов
            sets[0].fName = "Смешанный"; //создание наборов
            sets[0].fSet = "@WHk/;:,. ";
            sets[1].fName = "Математический";
            sets[1].fSet = "801#%/:,. ";
            sets[2].fName = "Буквенный";
            sets[2].fSet = "wzxvunrjftli ";
            sets[3].fName = "Буквенный с цифрами";
            sets[3].fSet = "W967410OI ";
            sets[4].fName = "Точки";
            sets[4].fSet = ". ";
            sets[5].fName = "Решётки";
            sets[5].fSet = "# ";
            using (BinaryWriter writer = new BinaryWriter(File.Open("Sets.dat", FileMode.Create)))
            {
                writer.Write("SETSFILE"); //запись заголовка
                for (byte i = 0; i < sets.Length; i++) //последовательная запись структур
                {
                    writer.Write(sets[i].fName);
                    writer.Write(sets[i].fSet);
                }
            }
        }
        private SymSet[] LoadUserSetFile() //загрузка файла пользователя
        {
            List<SymSet> userSets = new List<SymSet>(); //инициализация спика пользовательских наборов
            if (File.Exists("userset.txt")) //проверка, что файл существует
            {
                try
                {
                    StreamReader reader = new StreamReader("userset.txt");
                    string usertext = reader.ReadToEnd();
                    if (usertext == "") //если файл пустой, возвращается пустой список
                    {
                        return userSets.ToArray();
                    }
                    int i = 3; //счётчик
                    string NewName = "";
                    string NewSet = "";
                    foreach (string line in usertext.Split("\n")) //просмотр всех строк в файле
                    {
                        //проверка, что строка закомментирована или пустая
                        if (line.StartsWith("//") || string.IsNullOrWhiteSpace(line))
                        {
                            continue; //переход к следующей итерации
                        }
                        if (i % 2 == 1) //если нечёт, определение названия набора
                        {
                            NewName = line.TrimEnd();
                        }
                        else //если чёт, определение строки набора
                        {
                            NewSet = line.TrimEnd();
                        }
                        if (i % 2 == 0) //если чёт, добавляется новый набор
                        {
                            if (NewName.Length <= 30 && NewSet.Length <= 15) //проверка длин названия и строки
                            {
                                userSets.Add(new SymSet { fName = NewName, fSet = NewSet }); //добавление нового набора
                            }
                        }
                        i++; //увеличение счётчика
                    }
                }
                catch
                {
                    File.Move("userset.txt", "old_userset.txt"); //переименование старого файла
                                                                 //создание нового файла для пользователя
                    using (StreamWriter writer = new StreamWriter("userset.txt"))
                    {
                        writer.Write("//данный файл предназначен для добавления собственных наборов символов\r\n//первая строка - название набора, может содержать до 30 символов\r\n//вторая строка - строка набора, может содержать до 15 символов\r\n//если файл содержит ошибочные данные, часть или все ваши наборы не будут добавлены\r\n//закомментированные строки (начинающиеся с символов \"//\") и пустые строки пропускаются программой\r\n//название файла должно остаться userset.txt\r\n//пробел для строки символов НЕ ДОБАВЛЯЕТСЯ\r\n\r\n//ниже представлен пример (для проверки раскомментируйте):\r\nТестовый\r\n@%/. ");
                    }
                }
            }
            else
            {
                //создание нового файла для пользователя
                using (StreamWriter writer = new StreamWriter("userset.txt"))
                {
                    writer.Write("//данный файл предназначен для добавления собственных наборов символов\r\n//первая строка - название набора, может содержать до 30 символов\r\n//вторая строка - строка набора, может содержать до 15 символов\r\n//если файл содержит ошибочные данные, часть или все ваши наборы не будут добавлены\r\n//закомментированные строки (начинающиеся с символов \"//\") и пустые строки пропускаются программой\r\n//название файла должно остаться userset.txt\r\n//пробел для строки символов НЕ ДОБАВЛЯЕТСЯ\r\n\r\n//ниже представлен пример (для проверки раскомментируйте):\r\nТестовый\r\n@%/. ");
                }
            }
            return userSets.ToArray();
        }
        public SymSet[] LoadSetFile() //загрузка файла наборов
        {
            List<SymSet> sets = new List<SymSet>(); //список
            if (!File.Exists("Sets.dat")) //проверка, что файл не существует
            {
                CreateSetFile(); //создание файла
            }
            try
            {
                //открытие файла
                using (BinaryReader reader = new BinaryReader(File.Open("Sets.dat", FileMode.Open)))
                {
                    string Header = reader.ReadString();
                    if (Header != "SETSFILE") //проверка заголовка
                    {
                        reader.Dispose(); //очистка ресурсов
                        reader.Close(); //закрытие файла
                        CreateSetFile(); //повторное создание файла с наборами
                        return LoadSetFile(); //загрузка файла
                    }
                    for (byte i = 0; i < 6; i++)
                    {
                        string name = reader.ReadString();
                        string set = reader.ReadString();
                        sets.Add(new SymSet { fName = name, fSet = set });
                    }
                }
                //чтение пользовательского файла
                SymSet[] userSets = LoadUserSetFile();
                if (userSets.Length > 0)
                {
                    for (int i = 0; i < userSets.Length; i++)
                    {
                        sets.Add(new SymSet { fName = userSets[i].fName, fSet = userSets[i].fSet }); //добавление i-го набора пользователя
                    }
                }
                return sets.ToArray();
            }
            catch
            {
                File.Delete("Sets.dat"); //удаление неверного файла
                return LoadSetFile();
            }
        }
        public void SaveTXT(string fileName) //сохранение файла с результатом
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(_form.textBox1.Text); //запись всего текста из поля в файл
            }
        }
    }
}

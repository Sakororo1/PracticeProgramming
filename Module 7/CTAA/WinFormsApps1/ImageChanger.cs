using System;
using SixLabors.ImageSharp.PixelFormats;
using SLI = SixLabors.ImageSharp;
using WinFormsApp1;
using SixLabors.ImageSharp.Processing;
namespace CTAA
{
    public class ImageChanger //изменение изображения
    {
        private Form1 _form;
        public SLI.Image<Rgba32> resultingImage; //объект с изображением
        public ushort width; //ширина изображения
        public sbyte contrast; //значение контрастности
        public string set; //строка с набором
        public int frame; //номер кадра
        public ImageChanger(Form1 form) //конструктор класса
        {
            _form = form; //ссылка на форму
        }
        //обновление изображения
        public void UpdateImage(int w, int c, int set_num, int frame)
        {
            resultingImage = _form.sharpImages[_form.CurrentFrameIndex].Clone(); //копирование изображения
            ChangeSet(set_num); //изменение текущего набора символов
            ChangeSize(w); //изменение размера
            ChangeContrast(c); //изменение контрастности
            _form.resCreator.CreateResult(resultingImage, set); //создание результата
        }
        //инвертирование изображения
        public void Invert()
        {
            for (ushort i = 0; i < _form.sharpImages.Count; i++)
            {
                _form.sharpImages[i].Mutate(x => x.Invert()); //инвертирование каждого кадра
            }
            //обновление изображения
            UpdateImage(width, contrast, _form.comboBox1.SelectedIndex, frame);
        }
        //изменение набора символов
        private void ChangeSet(int num)
        {
            set = _form.sets[num].fSet; //текущий набор - поле со строкой выбранного набора
        }
        //изменение размера по ширине
        private void ChangeSize(int w2)
        {
            width = (ushort)w2;
            int w1 = resultingImage.Width; //текущий размер
            int h1 = resultingImage.Height;
            int h2 = h1 * w2 / w1;
            resultingImage.Mutate(x => x.Resize(w2, h2));
        }//изменение размера
        //изменение контрастности
        private void ChangeContrast(int value)
        {
            contrast = (sbyte)value;
            float new_contrast = value / 100 + 1; //вычисление значения контрастности
            resultingImage.Mutate(x => x.Contrast(new_contrast));
        }
    }
}
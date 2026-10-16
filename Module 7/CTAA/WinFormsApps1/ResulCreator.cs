using System;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using WinFormsApp1;
using SLI = SixLabors.ImageSharp;
namespace CTAA
{
    public class ResultCreator //класс, предназначенный для создания результата
    {
        private Form1 _form;
        public ResultCreator(Form1 form) //конструктор класса
        {
            _form = form; //ссылка на форму
        }
        //Составление результата
        public void CreateResult(SLI.Image<Rgba32> img, string set)
        {
            System.Text.StringBuilder res = new System.Text.StringBuilder("");// строка с результатом
            img.Mutate(x => x.Resize(img.Width, img.Height / 2)); //уменьшение высоты в два раза
            double Y = 0; //значение яркости изображения
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Y = 0.3 * img[j, i].R + 0.59 * img[j, i].G + 0.11 * img[j, i].B;//вычисление яркости пикселя по формуле
                    byte num = Convert.ToByte(Y * (Convert.ToDouble(set.Length - 1)) / 255); //вычисление индекса
                    res.Append(set[num]); //добавление символа в строку
                }
                res.Append("\r\n"); //добавление переноса строки
            }
            _form.textBox1.Font = new Font(_form.textBox1.Font.FontFamily, CountTextSize(res.ToString())); //изменение размера
            _form.textBox1.Text = res.ToString(); //добавление строки результата в текстовое поле
        }
        //вычисление размера шрифта
        private float CountTextSize(string str)
        {
            int l = str.Split("\r\n")[0].Length; //массив строк
            if (l == 0) return 30;
            float s = 30f;//изначально размер шрифта - 30
            if (l > 0)
            {
                s = (float)((_form.textBox1.ClientSize.Width - 490) / (0.6 * l)); //вычисление размера шрифта
            }
            return Math.Max(2.5f, Math.Min(30, s)); //возвращение размера шрифта
        }
    }
}

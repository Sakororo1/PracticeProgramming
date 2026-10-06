using System;
using System.Windows.Forms;
using System.Collections.Generic;
namespace task5_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static class Calculator //класс калькулятор
        {
            static public double Calculate(string s)
            {
                try
                {
                    string signs = "+-/*";
                    char sign = '\0';
                    string[] soperands = new string[2];
                    double[] operands = new double[2];
                    bool done = false;
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (signs.Contains($"{s[i]}"))
                        {
                            soperands = s.Split(s[i]);
                            sign = s[i]; //запоминание знака
                            operands = new double[2];
                            operands[0] = Convert.ToDouble(soperands[0]);
                            operands[1] = Convert.ToDouble(soperands[1]);
                            done = true;
                            break;
                        }
                    }
                    if (!done) return 0;
                    switch (sign)
                    {
                        case '+': //сложение
                            return operands[0] + operands[1];
                        case '-': //вычитание
                            return operands[0] - operands[1];
                        case '*': //умножение
                            return operands[0] * operands[1];
                        case '/': //деление
                            return operands[0] / operands[1];
                    }
                    return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        Button[] b;
        private void Form1_Load(object sender, EventArgs e)
        {
            int ButtonWidth = (textBox1.Width - 30) / 4; //значение ширин кнопок
            int x = textBox1.Right - button_C.Width;
            int y = textBox1.Bottom + 10;
            button_C.Left = x; //изменение положения кнопки С
            button_C.Top = y;
            button_Equal.Left = x + 130; //изменение положения кнопки =
            button_Equal.Top = y;
            y += 110;
            button_plus.Left = x; //изменение положения кнопки +
            button_plus.Top = y;
            button_remove.Left = x + 130; //изменение положения кнопки <-
            button_remove.Top = y;
            y += 110;
            button_minus.Left = x; //изменение положения кнопки -
            button_minus.Top = y;
            y += 110;
            button_multiply.Left = x; //изменение положения кнопки *
            button_multiply.Top = y;
            y += 110;
            button_divide.Left = x; //изменение положения кнопки /
            button_divide.Top = y;
            b = new Button[10]; //инициализация массива кнопок
            x = textBox1.Left; y = textBox1.Bottom + 10; //координаты
            for (int i = 0; i < 10; i++)
            {
                b[i] = new Button(); //инициализация
                b[i].Parent = this; //установка формы как родителя
                b[i].Tag = i; //установка тега
                b[i].Click += Num_B_Click; //обработчик события нажатия
                b[i].Text = i.ToString(); //текст на кнопке
                b[i].Width = ButtonWidth; b[i].Height = 100; //размер
                if (i == 0) continue; //пропуск нулевой кнопки
                b[i].Left = x; b[i].Top = y; //определение положения
                x += ButtonWidth + 10;
                if (x >= button_C.Left)
                {
                    x = textBox1.Left;
                    y += 110;
                }
            }
            x += ButtonWidth + 10;
            b[0].Left = x; b[0].Top = y;
        }
        //добавление числа
        private void Num_B_Click(object sender, EventArgs e)
        {
            Button B = (Button)sender;
            textBox1.Text += B.Tag.ToString();
        }
        //добавление знака
        private void button_sign_Click(object sender, EventArgs e)
        {
            Button B = (Button)sender;
            //проверка, что в выражении уже есть знак
            if (textBox1.Text.Contains("\\") || textBox1.Text.Contains("+")
                || textBox1.Text.Contains("-")|| textBox1.Text.Contains("*"))
            {
                textBox1.Text = Calculator.Calculate(textBox1.Text).ToString();
            }
            else
            {
                textBox1.Text+=B.Text;
            }
        }
        //очистка поля
        private void button_C_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        //получение результата
        private void button_Equal_Click(object sender, EventArgs e)
        {
            textBox1.Text=Calculator.Calculate(textBox1.Text).ToString();
        }
        //удаление последнего символа
        private void button_remove_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength>0)
            {
                textBox1.Text=textBox1.Text.Substring(0,textBox1.Text.Length-1);
            }
        }
    }
}

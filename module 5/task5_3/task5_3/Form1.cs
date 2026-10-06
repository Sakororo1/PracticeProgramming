using System;
using System.Windows.Forms;
namespace task5_3
{
    public partial class Form1 : Form
    {
        class Tasks //работа с задачами
        {
            private static Tasks I;
            public static Tasks GetInstance(CheckedListBox tbox, CheckedListBox cbox)
            {
                if (I == null)
                {
                    I = new Tasks(tbox,cbox);
                }
                return I;
            }
            private Tasks(CheckedListBox tbox, CheckedListBox cbox) //конструктор, принимает ссылки на списки
            {
                _tbox = tbox;
                _cbox=cbox;
            }
            CheckedListBox _tbox; //ссылка на список задач
            CheckedListBox _cbox; //ссылка на список выполненных задач
            public void AddTask(string name) //добавление задачи
            {
                _tbox.Items.Add(name);
            }
            public void RemoveTask(int index) //удаление задачи
            {
                _tbox.Items.RemoveAt(_tbox.SelectedIndex);
            }
            public void CompleteTask(int index,Timer t) //выполнение задания
            {
                t.Stop(); //остановка таймера
                if (_tbox.CheckedItems.Count != 0) //проверка, что есть отмеченные задачи
                {
                    _cbox.Items.Add(_tbox.CheckedItems[index]); //добавление в нижний список
                    RemoveTask(index); //удаление задачи из верхнего списка
                }
            }
        }
        Tasks t; 
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //инициализация объекта для работы с задачами
            t = Tasks.GetInstance(checkedListBox_tasks,checkedListBox_completed);
        }
        private void button_addTask_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "") //проверка, что название введено
            {
                t.AddTask(textBox1.Text);
            }
            textBox1.Clear(); //удаление текста из текстового поля
        }
        private void button_deleteTask_Click(object sender, EventArgs e)
        {
            if (checkedListBox_tasks.SelectedIndex>=0) //проверка, что выбран элемент
            {
                t.RemoveTask(checkedListBox_tasks.SelectedIndex);
            }
        }
        private void checkedListBox_tasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            timer1.Start(); //начало таймера
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //выполнение задания, которое уже считается отмеченным
            t.CompleteTask(checkedListBox_tasks.CheckedItems.Count-1,timer1);
        }
    }
}

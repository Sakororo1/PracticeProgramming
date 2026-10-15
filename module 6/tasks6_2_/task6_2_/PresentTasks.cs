using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace task6_2_
{
    internal class PresentTasks
    {
        WorkTasks w;   //объект для выполнения задач
        ListBox c;     //ссылка на список
        MainWindow f;  //ссылка на форму
        private static PresentTasks Instance;
        public static PresentTasks GetInstance(MainWindow form)
        {
            if (Instance == null)
            {
                Instance = new PresentTasks(form);
            }
            return Instance;
        }
        private PresentTasks(MainWindow form)
        {
            f = form;
            c = f.checkedListBox1;
            w = WorkTasks.GetInstance();
            w.EOnGettingTasks += ShowTasks;
        }
        private List<string> Descriptions = new List<string>(); //список описаний
        //получение списка задач
        public void GetTasks()
        {
            w.GetTasks();
        }
        //вывод задач
        private void ShowTasks(List<Task> tasks)
        {
            if (Descriptions.Count > 0)
            {
                Descriptions.Clear(); //очистка списка описаний
            }
            if (c.Items.Count > 0)
            {
                c.Items.Clear(); //очистка списка задач
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                Descriptions.Add(tasks[i].description); //добавление i-того описания
                c.Items.Add(tasks[i]); //добавление задачи (шаблон сам отрисует CheckBox)
            }
        }
        //добавление задачи
        public void AddTask()
        {
            w.AddTask(f.textBox_name.Text, f.textBox_description.Text);
            f.textBox_name.Clear();
            f.textBox_description.Clear();
        }
        //удаление задачи
        public void DeleteTask()
        {
            w.DeleteTask(c.SelectedIndex);
        }
        //вывод информации о задаче
        public void ShowTask()
        {
            if (c.SelectedIndex >= 0)
            {
                Task item = (Task)c.SelectedItem;
                f.label3.Content = "Название: " + item.name;      //вывод названия
                f.label4.Content = "Описание:" + item.description; //вывод описания
            }
        }
        public void UpdateTaskCompletion(int ind)
        {
            w.UpdateTaskCompletion(ind);
        }
    }

}

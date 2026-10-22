using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
namespace task9_3
{
    internal class Presenter
    {
        private MainWindow mw; //ссылка на окно
        private Model m; //ссылка на модель
        private HashSet<int> notified = new HashSet<int>(); //номера задач, о которых были уведомления
        static Presenter Instance; //экземпляр
        public static Presenter GetInstance(MainWindow mw)
        {
            if (Instance == null) Instance = new Presenter(mw);
            return Instance;
        }
        private Presenter(MainWindow mw)
        {
            this.mw = mw;
            m = new Model();
            m.OnGotTasks += M_OnGotTasks;
        }
        public void GetData()
        {
            m.GetData();
        }
        public void AddTask() //добавление задачи
        {
            if (mw.Task_Name.Text == "") return;
            if (mw.Task_DueDate.SelectedDate == null) return;
            string name = mw.Task_Name.Text; //название
            string priority = "Средний"; //приоритет
            ComboBoxItem ci = mw.Task_Priority.SelectedItem as ComboBoxItem;
            if (ci != null) priority = ci.Content.ToString();
            string dueDate = mw.Task_DueDate.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm");
            m.AddData(new string[] { name, priority, dueDate, "0" });
            mw.Task_Name.Clear();
        }
        public void DeleteTask() //удаление задачи
        {
            m.Delete(mw.DataGridTasks.SelectedIndex);
        }
        public void ChangeTask(int col, string new_val) //изменение задачи
        {
            m.ChangeData(mw.DataGridTasks.SelectedIndex, col, new_val);
        }
        public void MarkDone() //отметить выбранную задачу выполненной
        {
            if (mw.DataGridTasks.SelectedIndex < 0) return;
            m.ChangeData(mw.DataGridTasks.SelectedIndex, 4, "100");
        }
        public void ChangeDueDate() //изменить срок выбранной задачи
        {
            if (mw.DataGridTasks.SelectedIndex < 0) return;
            if (mw.New_DueDate.SelectedDate == null) return;
            m.ChangeData(mw.DataGridTasks.SelectedIndex, 3,
                mw.New_DueDate.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm"));
        }
        public void CheckDeadlines() //проверка срока
        {
            List<Model.Task> upcoming = m.GetUpcoming(24); //список приближающихся сроков
            List<Model.Task> toNotify = new List<Model.Task>(); //список сроков, о которых нужно уведомить
            foreach (Model.Task t in upcoming)
            {
                if (!notified.Contains(t.Id)) toNotify.Add(t);
            }
            if (toNotify.Count == 0) return;
            string msg = "Приближаются сроки выполнения:\n";
            foreach (Model.Task t in toNotify) //составление уведобления
            {
                msg += $"| {t.Name} — до {t.DueDate:dd.MM.yyyy HH:mm} (прогресс {t.Progress}%)";
                notified.Add(t.Id); //добавление номера задачи
            }
            ShowNotification(msg); //всплывающее окно
        }
        private void ShowNotification(string msg) //всплывающее уведомление о сроке
        {
            MessageBox.Show(msg, "Напоминание о сроке", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void M_OnGotTasks(List<Model.Task> Data)
        {
            mw.DataGridTasks.ItemsSource = Data;
        }
    }
}
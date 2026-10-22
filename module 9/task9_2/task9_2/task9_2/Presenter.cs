using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace task9_2
{
    internal class Presenter
    {
        private MainWindow mw; //ссылка на окно
        private Model m; //ссылка на модель
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
            m.OnGotProjects += M_OnGotProjects;
            m.OnGotEmployees += M_OnGotEmployees;
        }
        public void GetData()
        {
            m.GetData(StructType.EEmployee);
            m.GetData(StructType.EProject);
            m.GetData(StructType.ETask);
        }
        public void DeleteEmployee() //удаление сотрудника
        {
            m.Delete(mw.DataGridEmployees.SelectedIndex,StructType.EEmployee);
        }
        public void DeleteProject() //удаление проекта
        {
            m.Delete(mw.DataGridProjects.SelectedIndex, StructType.EProject);
        }
        public void DeleteTask() //удаление задачи
        {
            m.Delete(mw.DataGridTasks.SelectedIndex, StructType.ETask);
        }
        public void ChangeEmployee(int col, string new_val) //изменение сотрудника
        {
            m.ChangeData(mw.DataGridEmployees.SelectedIndex, col, StructType.EEmployee, new_val);
        }
        public void ChangeProject(int col, string new_val) //изменение проекта
        {
            m.ChangeData(mw.DataGridProjects.SelectedIndex, col, StructType.EProject, new_val);
        }
        public void ChangeTask(int col, string new_val) //изменение задачи
        {
            m.ChangeData(mw.DataGridTasks.SelectedIndex, col, StructType.ETask, new_val);
        }
        public void AddEmployee() //добавление сотрудника
        {
            m.AddData(StructType.EEmployee, new string[] { mw.Employee_Name.Text });
            mw.Employee_Name.Clear();
        }
        public void AddProject() //добавление проекта
        {
            m.AddData(StructType.EProject, new string[] { mw.Project_Name.Text, "запланирован"});
            mw.Project_Name.Clear();
        }
        public void AddTask() //добавление задачи
        {
            if (mw.DataGridEmployees.SelectedIndex < 0||mw.Task_Name.Text=="") return;
            m.AddData(StructType.ETask, new string[]
            {
                mw.Task_Name.Text,
                mw.DataGridEmployees.SelectedIndex.ToString(),
                "0"
            });
        }
        private void M_OnGotEmployees(List<Model.Employee> Data)
        {
            mw.DataGridEmployees.ItemsSource= Data;
        }

        private void M_OnGotProjects(List<Model.Project> Data)
        {
            mw.DataGridProjects.ItemsSource = Data;
        }

        private void M_OnGotTasks(List<Model.Task> Data)
        {
            mw.DataGridTasks.ItemsSource = Data;
        }  
        public void CreateTaskDoc()
        {
            m.CreateDoc(StructType.ETask, mw.DataGridTasks.SelectedIndex);
        }
        public void CreateProjectDoc()
        {
            m.CreateDoc(StructType.EProject, mw.DataGridProjects.SelectedIndex);
        }
    }
}

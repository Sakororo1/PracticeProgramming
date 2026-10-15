using System;
namespace task6_4
{
    internal class Finance //запись о расходе/доходе
    {
        //конструктор
        public Finance(int id, string name, double sum, bool type)
        {
            fid = id;
            fname = name;
            fsum = sum;
            ftype = type;
        }
        public int fid { get; set; } //номер
        public string fname { get; set; } //название
        public double fsum { get; set; } //сумма
        public bool ftype { get; set; } //тип, false = 0 - трата, true = 1 - доход
        public string ftypeDisplay //отображение типа для combobox
        {
            get
            {
                if (ftype) return "Доход";
                else return "Расход";
            }
        }
    }

}

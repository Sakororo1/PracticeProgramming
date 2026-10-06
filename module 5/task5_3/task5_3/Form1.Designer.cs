namespace task5_3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_addTask = new System.Windows.Forms.Button();
            this.checkedListBox_tasks = new System.Windows.Forms.CheckedListBox();
            this.button_deleteTask = new System.Windows.Forms.Button();
            this.checkedListBox_completed = new System.Windows.Forms.CheckedListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button_addTask
            // 
            this.button_addTask.Location = new System.Drawing.Point(1083, 149);
            this.button_addTask.Name = "button_addTask";
            this.button_addTask.Size = new System.Drawing.Size(160, 72);
            this.button_addTask.TabIndex = 0;
            this.button_addTask.Text = "Добавить задачу";
            this.button_addTask.UseVisualStyleBackColor = true;
            this.button_addTask.Click += new System.EventHandler(this.button_addTask_Click);
            // 
            // checkedListBox_tasks
            // 
            this.checkedListBox_tasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBox_tasks.FormattingEnabled = true;
            this.checkedListBox_tasks.Location = new System.Drawing.Point(30, 22);
            this.checkedListBox_tasks.Name = "checkedListBox_tasks";
            this.checkedListBox_tasks.Size = new System.Drawing.Size(1008, 429);
            this.checkedListBox_tasks.TabIndex = 1;
            this.checkedListBox_tasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_tasks_ItemCheck);
            // 
            // button_deleteTask
            // 
            this.button_deleteTask.Location = new System.Drawing.Point(1083, 244);
            this.button_deleteTask.Name = "button_deleteTask";
            this.button_deleteTask.Size = new System.Drawing.Size(160, 72);
            this.button_deleteTask.TabIndex = 2;
            this.button_deleteTask.Text = "Удалить задачу";
            this.button_deleteTask.UseVisualStyleBackColor = true;
            this.button_deleteTask.Click += new System.EventHandler(this.button_deleteTask_Click);
            // 
            // checkedListBox_completed
            // 
            this.checkedListBox_completed.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.checkedListBox_completed.Enabled = false;
            this.checkedListBox_completed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBox_completed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkedListBox_completed.FormattingEnabled = true;
            this.checkedListBox_completed.Location = new System.Drawing.Point(30, 477);
            this.checkedListBox_completed.Name = "checkedListBox_completed";
            this.checkedListBox_completed.Size = new System.Drawing.Size(1008, 242);
            this.checkedListBox_completed.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1083, 110);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 22);
            this.textBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1080, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Название:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 711);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkedListBox_completed);
            this.Controls.Add(this.button_deleteTask);
            this.Controls.Add(this.checkedListBox_tasks);
            this.Controls.Add(this.button_addTask);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_addTask;
        private System.Windows.Forms.CheckedListBox checkedListBox_tasks;
        private System.Windows.Forms.Button button_deleteTask;
        private System.Windows.Forms.CheckedListBox checkedListBox_completed;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}


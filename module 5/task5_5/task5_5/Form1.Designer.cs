namespace task5_5
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_C = new System.Windows.Forms.Button();
            this.button_plus = new System.Windows.Forms.Button();
            this.button_minus = new System.Windows.Forms.Button();
            this.button_multiply = new System.Windows.Forms.Button();
            this.button_divide = new System.Windows.Forms.Button();
            this.button_Equal = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(15, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(510, 45);
            this.textBox1.TabIndex = 0;
            // 
            // button_C
            // 
            this.button_C.Location = new System.Drawing.Point(425, 82);
            this.button_C.Name = "button_C";
            this.button_C.Size = new System.Drawing.Size(120, 100);
            this.button_C.TabIndex = 1;
            this.button_C.Text = "C";
            this.button_C.UseVisualStyleBackColor = true;
            this.button_C.Click += new System.EventHandler(this.button_C_Click);
            // 
            // button_plus
            // 
            this.button_plus.Location = new System.Drawing.Point(425, 212);
            this.button_plus.Name = "button_plus";
            this.button_plus.Size = new System.Drawing.Size(120, 100);
            this.button_plus.TabIndex = 2;
            this.button_plus.Text = "+";
            this.button_plus.UseVisualStyleBackColor = true;
            this.button_plus.Click += new System.EventHandler(this.button_sign_Click);
            // 
            // button_minus
            // 
            this.button_minus.Location = new System.Drawing.Point(425, 332);
            this.button_minus.Name = "button_minus";
            this.button_minus.Size = new System.Drawing.Size(120, 100);
            this.button_minus.TabIndex = 3;
            this.button_minus.Text = "-";
            this.button_minus.UseVisualStyleBackColor = true;
            this.button_minus.Click += new System.EventHandler(this.button_sign_Click);
            // 
            // button_multiply
            // 
            this.button_multiply.Location = new System.Drawing.Point(425, 438);
            this.button_multiply.Name = "button_multiply";
            this.button_multiply.Size = new System.Drawing.Size(120, 100);
            this.button_multiply.TabIndex = 4;
            this.button_multiply.Text = "*";
            this.button_multiply.UseVisualStyleBackColor = true;
            this.button_multiply.Click += new System.EventHandler(this.button_sign_Click);
            // 
            // button_divide
            // 
            this.button_divide.Location = new System.Drawing.Point(425, 544);
            this.button_divide.Name = "button_divide";
            this.button_divide.Size = new System.Drawing.Size(120, 100);
            this.button_divide.TabIndex = 5;
            this.button_divide.Text = "/";
            this.button_divide.UseVisualStyleBackColor = true;
            this.button_divide.Click += new System.EventHandler(this.button_sign_Click);
            // 
            // button_Equal
            // 
            this.button_Equal.Location = new System.Drawing.Point(551, 82);
            this.button_Equal.Name = "button_Equal";
            this.button_Equal.Size = new System.Drawing.Size(120, 100);
            this.button_Equal.TabIndex = 6;
            this.button_Equal.Text = "=";
            this.button_Equal.UseVisualStyleBackColor = true;
            this.button_Equal.Click += new System.EventHandler(this.button_Equal_Click);
            // 
            // button_remove
            // 
            this.button_remove.Location = new System.Drawing.Point(551, 212);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(120, 100);
            this.button_remove.TabIndex = 7;
            this.button_remove.Text = "<-";
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 756);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.button_Equal);
            this.Controls.Add(this.button_divide);
            this.Controls.Add(this.button_multiply);
            this.Controls.Add(this.button_minus);
            this.Controls.Add(this.button_plus);
            this.Controls.Add(this.button_C);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_C;
        private System.Windows.Forms.Button button_plus;
        private System.Windows.Forms.Button button_minus;
        private System.Windows.Forms.Button button_multiply;
        private System.Windows.Forms.Button button_divide;
        private System.Windows.Forms.Button button_Equal;
        private System.Windows.Forms.Button button_remove;
    }
}


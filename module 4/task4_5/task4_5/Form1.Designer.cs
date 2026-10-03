namespace task4_5
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
            this.numericUpDown_Radius = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_x1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_y1 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Circle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown_a = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_b = new System.Windows.Forms.NumericUpDown();
            this.button_Line = new System.Windows.Forms.Button();
            this.button_Rectangle = new System.Windows.Forms.Button();
            this.numericUpDown_x2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_y2 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDown_Circle_x = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Circle_y = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDown_Rect_x = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Rect_y = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_b)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Circle_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Circle_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rect_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rect_y)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_Radius
            // 
            this.numericUpDown_Radius.Location = new System.Drawing.Point(76, 37);
            this.numericUpDown_Radius.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Radius.Name = "numericUpDown_Radius";
            this.numericUpDown_Radius.Size = new System.Drawing.Size(57, 22);
            this.numericUpDown_Radius.TabIndex = 0;
            // 
            // numericUpDown_x1
            // 
            this.numericUpDown_x1.Location = new System.Drawing.Point(252, 31);
            this.numericUpDown_x1.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDown_x1.Name = "numericUpDown_x1";
            this.numericUpDown_x1.Size = new System.Drawing.Size(59, 22);
            this.numericUpDown_x1.TabIndex = 1;
            // 
            // numericUpDown_y1
            // 
            this.numericUpDown_y1.Location = new System.Drawing.Point(252, 66);
            this.numericUpDown_y1.Maximum = new decimal(new int[] {
            450,
            0,
            0,
            0});
            this.numericUpDown_y1.Name = "numericUpDown_y1";
            this.numericUpDown_y1.Size = new System.Drawing.Size(59, 22);
            this.numericUpDown_y1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel1.Location = new System.Drawing.Point(12, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 450);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Круг";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Радиус:";
            // 
            // button_Circle
            // 
            this.button_Circle.Location = new System.Drawing.Point(32, 94);
            this.button_Circle.Name = "button_Circle";
            this.button_Circle.Size = new System.Drawing.Size(75, 23);
            this.button_Circle.TabIndex = 6;
            this.button_Circle.Text = "круг";
            this.button_Circle.UseVisualStyleBackColor = true;
            this.button_Circle.Click += new System.EventHandler(this.button_Circle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(301, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Линия";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "x1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "y1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(317, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "x2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "y2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(639, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Прямоугольник";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(539, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "a:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(539, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "b:";
            // 
            // numericUpDown_a
            // 
            this.numericUpDown_a.Location = new System.Drawing.Point(563, 37);
            this.numericUpDown_a.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_a.Name = "numericUpDown_a";
            this.numericUpDown_a.Size = new System.Drawing.Size(60, 22);
            this.numericUpDown_a.TabIndex = 15;
            // 
            // numericUpDown_b
            // 
            this.numericUpDown_b.Location = new System.Drawing.Point(563, 72);
            this.numericUpDown_b.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_b.Name = "numericUpDown_b";
            this.numericUpDown_b.Size = new System.Drawing.Size(60, 22);
            this.numericUpDown_b.TabIndex = 16;
            // 
            // button_Line
            // 
            this.button_Line.Location = new System.Drawing.Point(285, 94);
            this.button_Line.Name = "button_Line";
            this.button_Line.Size = new System.Drawing.Size(75, 23);
            this.button_Line.TabIndex = 17;
            this.button_Line.Text = "Линия";
            this.button_Line.UseVisualStyleBackColor = true;
            this.button_Line.Click += new System.EventHandler(this.button_Line_Click);
            // 
            // button_Rectangle
            // 
            this.button_Rectangle.Location = new System.Drawing.Point(611, 100);
            this.button_Rectangle.Name = "button_Rectangle";
            this.button_Rectangle.Size = new System.Drawing.Size(137, 23);
            this.button_Rectangle.TabIndex = 18;
            this.button_Rectangle.Text = "Прямоугольник";
            this.button_Rectangle.UseVisualStyleBackColor = true;
            this.button_Rectangle.Click += new System.EventHandler(this.button_Rectangle_Click);
            // 
            // numericUpDown_x2
            // 
            this.numericUpDown_x2.Location = new System.Drawing.Point(344, 31);
            this.numericUpDown_x2.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDown_x2.Name = "numericUpDown_x2";
            this.numericUpDown_x2.Size = new System.Drawing.Size(66, 22);
            this.numericUpDown_x2.TabIndex = 19;
            // 
            // numericUpDown_y2
            // 
            this.numericUpDown_y2.Location = new System.Drawing.Point(344, 64);
            this.numericUpDown_y2.Maximum = new decimal(new int[] {
            450,
            0,
            0,
            0});
            this.numericUpDown_y2.Name = "numericUpDown_y2";
            this.numericUpDown_y2.Size = new System.Drawing.Size(66, 22);
            this.numericUpDown_y2.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "x:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(107, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "y:";
            // 
            // numericUpDown_Circle_x
            // 
            this.numericUpDown_Circle_x.Location = new System.Drawing.Point(34, 64);
            this.numericUpDown_Circle_x.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDown_Circle_x.Name = "numericUpDown_Circle_x";
            this.numericUpDown_Circle_x.Size = new System.Drawing.Size(54, 22);
            this.numericUpDown_Circle_x.TabIndex = 23;
            // 
            // numericUpDown_Circle_y
            // 
            this.numericUpDown_Circle_y.Location = new System.Drawing.Point(130, 66);
            this.numericUpDown_Circle_y.Maximum = new decimal(new int[] {
            450,
            0,
            0,
            0});
            this.numericUpDown_Circle_y.Name = "numericUpDown_Circle_y";
            this.numericUpDown_Circle_y.Size = new System.Drawing.Size(62, 22);
            this.numericUpDown_Circle_y.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(667, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "x:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(667, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 16);
            this.label14.TabIndex = 26;
            this.label14.Text = "y:";
            // 
            // numericUpDown_Rect_x
            // 
            this.numericUpDown_Rect_x.Location = new System.Drawing.Point(709, 39);
            this.numericUpDown_Rect_x.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDown_Rect_x.Name = "numericUpDown_Rect_x";
            this.numericUpDown_Rect_x.Size = new System.Drawing.Size(69, 22);
            this.numericUpDown_Rect_x.TabIndex = 27;
            // 
            // numericUpDown_Rect_y
            // 
            this.numericUpDown_Rect_y.Location = new System.Drawing.Point(709, 74);
            this.numericUpDown_Rect_y.Maximum = new decimal(new int[] {
            450,
            0,
            0,
            0});
            this.numericUpDown_Rect_y.Name = "numericUpDown_Rect_y";
            this.numericUpDown_Rect_y.Size = new System.Drawing.Size(69, 22);
            this.numericUpDown_Rect_y.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 595);
            this.Controls.Add(this.numericUpDown_Rect_y);
            this.Controls.Add(this.numericUpDown_Rect_x);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.numericUpDown_Circle_y);
            this.Controls.Add(this.numericUpDown_Circle_x);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericUpDown_y2);
            this.Controls.Add(this.numericUpDown_x2);
            this.Controls.Add(this.button_Rectangle);
            this.Controls.Add(this.button_Line);
            this.Controls.Add(this.numericUpDown_b);
            this.Controls.Add(this.numericUpDown_a);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_Circle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numericUpDown_y1);
            this.Controls.Add(this.numericUpDown_x1);
            this.Controls.Add(this.numericUpDown_Radius);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_b)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_x2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_y2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Circle_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Circle_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rect_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rect_y)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown_Radius;
        private System.Windows.Forms.NumericUpDown numericUpDown_x1;
        private System.Windows.Forms.NumericUpDown numericUpDown_y1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Circle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown_a;
        private System.Windows.Forms.NumericUpDown numericUpDown_b;
        private System.Windows.Forms.Button button_Line;
        private System.Windows.Forms.Button button_Rectangle;
        private System.Windows.Forms.NumericUpDown numericUpDown_x2;
        private System.Windows.Forms.NumericUpDown numericUpDown_y2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDown_Circle_x;
        private System.Windows.Forms.NumericUpDown numericUpDown_Circle_y;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericUpDown_Rect_x;
        private System.Windows.Forms.NumericUpDown numericUpDown_Rect_y;
    }
}


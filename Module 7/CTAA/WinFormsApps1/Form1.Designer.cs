namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button_load_file = new Button();
            label_size = new Label();
            trackBar_contrast = new TrackBar();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            button_copy = new Button();
            trackBar_size = new TrackBar();
            button_invert = new Button();
            numericUpDown1 = new NumericUpDown();
            label_contrast = new Label();
            label_file_name = new Label();
            button_save = new Button();
            label1 = new Label();
            label2 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)trackBar_contrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_size).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // button_load_file
            // 
            button_load_file.BackColor = Color.Indigo;
            button_load_file.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_load_file.ForeColor = SystemColors.ButtonHighlight;
            button_load_file.Location = new Point(30, 21);
            button_load_file.Name = "button_load_file";
            button_load_file.Size = new Size(448, 68);
            button_load_file.TabIndex = 0;
            button_load_file.Text = "Загрузить изображение";
            button_load_file.UseVisualStyleBackColor = false;
            button_load_file.Click += button_load_file_Click;
            // 
            // label_size
            // 
            label_size.AutoSize = true;
            label_size.ForeColor = SystemColors.ButtonHighlight;
            label_size.Location = new Point(614, 21);
            label_size.Name = "label_size";
            label_size.Size = new Size(67, 20);
            label_size.TabIndex = 1;
            label_size.Text = "Размер: ";
            // 
            // trackBar_contrast
            // 
            trackBar_contrast.Enabled = false;
            trackBar_contrast.Location = new Point(614, 133);
            trackBar_contrast.Maximum = 100;
            trackBar_contrast.Minimum = -100;
            trackBar_contrast.Name = "trackBar_contrast";
            trackBar_contrast.Size = new Size(474, 56);
            trackBar_contrast.TabIndex = 2;
            trackBar_contrast.Scroll += trackBar_contrast_Scroll;
            trackBar_contrast.MouseUp += trackBars_update_image;
            // 
            // comboBox1
            // 
            comboBox1.Enabled = false;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(946, 205);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 3;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectionChangeCommitted;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox1.Location = new Point(12, 268);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Результат";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(1100, 673);
            textBox1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(1103, 12);
            pictureBox1.MaximumSize = new Size(310, 250);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(310, 250);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Изображения|*.png; *.jpg; *.bmp; *.gif|PNG|*.png|JPEG,JPG|*.jpg|GIF|*.gif|BMP|*.bmp|Все файлы|*.*";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "Текстовые файлы|*.txt";
            // 
            // button_copy
            // 
            button_copy.BackColor = Color.DarkOrchid;
            button_copy.Enabled = false;
            button_copy.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_copy.ForeColor = SystemColors.ButtonHighlight;
            button_copy.Location = new Point(30, 144);
            button_copy.Name = "button_copy";
            button_copy.Size = new Size(221, 68);
            button_copy.TabIndex = 6;
            button_copy.Text = "Скопировать";
            button_copy.UseVisualStyleBackColor = false;
            button_copy.Click += button_copy_Click;
            // 
            // trackBar_size
            // 
            trackBar_size.Enabled = false;
            trackBar_size.LargeChange = 1;
            trackBar_size.Location = new Point(614, 53);
            trackBar_size.Maximum = 500;
            trackBar_size.Minimum = 10;
            trackBar_size.Name = "trackBar_size";
            trackBar_size.Size = new Size(474, 56);
            trackBar_size.TabIndex = 7;
            trackBar_size.Value = 10;
            trackBar_size.Scroll += trackBar_size_Scroll;
            trackBar_size.MouseUp += trackBars_update_image;
            // 
            // button_invert
            // 
            button_invert.BackColor = Color.DarkOrchid;
            button_invert.Enabled = false;
            button_invert.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_invert.ForeColor = SystemColors.ButtonHighlight;
            button_invert.Location = new Point(685, 182);
            button_invert.Name = "button_invert";
            button_invert.Size = new Size(221, 68);
            button_invert.TabIndex = 8;
            button_invert.Text = "Инвертировать цвет";
            button_invert.UseVisualStyleBackColor = false;
            button_invert.Click += button_invert_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Enabled = false;
            numericUpDown1.Location = new Point(495, 207);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.ReadOnly = true;
            numericUpDown1.Size = new Size(88, 27);
            numericUpDown1.TabIndex = 9;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label_contrast
            // 
            label_contrast.AutoSize = true;
            label_contrast.ForeColor = SystemColors.ButtonHighlight;
            label_contrast.Location = new Point(614, 112);
            label_contrast.Name = "label_contrast";
            label_contrast.Size = new Size(118, 20);
            label_contrast.TabIndex = 10;
            label_contrast.Text = "Контрастность: ";
            // 
            // label_file_name
            // 
            label_file_name.AutoSize = true;
            label_file_name.ForeColor = SystemColors.ButtonHighlight;
            label_file_name.Location = new Point(30, 92);
            label_file_name.Name = "label_file_name";
            label_file_name.Size = new Size(206, 20);
            label_file_name.TabIndex = 11;
            label_file_name.Text = "Изображение не загружено";
            // 
            // button_save
            // 
            button_save.BackColor = Color.DarkOrchid;
            button_save.Enabled = false;
            button_save.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_save.ForeColor = SystemColors.ButtonHighlight;
            button_save.Location = new Point(257, 144);
            button_save.Name = "button_save";
            button_save.Size = new Size(221, 68);
            button_save.TabIndex = 12;
            button_save.Text = "Сохранить в TXT";
            button_save.UseVisualStyleBackColor = false;
            button_save.Click += button_save_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(495, 171);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 13;
            label1.Text = "Кадр (для GIF): ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(957, 182);
            label2.Name = "label2";
            label2.Size = new Size(131, 20);
            label2.TabIndex = 14;
            label2.Text = "Набор символов:";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1432, 953);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button_save);
            Controls.Add(label_file_name);
            Controls.Add(label_contrast);
            Controls.Add(numericUpDown1);
            Controls.Add(button_invert);
            Controls.Add(trackBar_size);
            Controls.Add(button_copy);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Controls.Add(trackBar_contrast);
            Controls.Add(label_size);
            Controls.Add(button_load_file);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(2000, 1200);
            MinimizeBox = false;
            MinimumSize = new Size(1400, 1000);
            Name = "Form1";
            Text = "Convert to ASCII-art";
            HelpButtonClicked += Form1_HelpButtonClicked;
            Load += Form1_Load;
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            ((System.ComponentModel.ISupportInitialize)trackBar_contrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_size).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Button button_invert;
        private Label label1;
        private Label label2;
        private System.Windows.Forms.Timer timer1;
        public TrackBar trackBar_contrast;
        public TrackBar trackBar_size;
        public Button button_load_file;
        public ComboBox comboBox1;
        public TextBox textBox1;
        public PictureBox pictureBox1;
        public Button button_copy;
        public NumericUpDown numericUpDown1;
        public Button button_save;
        public Label label_size;
        public Label label_contrast;
        public Label label_file_name;
    }
}

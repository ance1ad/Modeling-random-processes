namespace firstLab
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
            teacher = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBoxStudentCount = new TextBox();
            textBoxArrivalInterval = new TextBox();
            textBoxQuestionTimeAssistant = new TextBox();
            textBoxQuestionTimeProfessor = new TextBox();
            textBoxQuestionsProfessor = new TextBox();
            textBoxQuestionsAssistant = new TextBox();
            startModelingBtn = new Button();
            assistant = new PictureBox();
            student = new PictureBox();
            label7 = new Label();
            label8 = new Label();
            pictureBox2 = new PictureBox();
            label9 = new Label();
            label10 = new Label();
            trackBarSpeed = new TrackBar();
            progressBar1 = new ProgressBar();
            labelGradeProf = new Label();
            labelGradeAss = new Label();
            examStatusLbl = new Label();
            studentBindingSource = new BindingSource(components);
            dataGridView1 = new DataGridView();
            numericUpDownScore2 = new NumericUpDown();
            numericUpDownScore3 = new NumericUpDown();
            numericUpDownScore4 = new NumericUpDown();
            numericUpDownScore5 = new NumericUpDown();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)teacher).BeginInit();
            ((System.ComponentModel.ISupportInitialize)assistant).BeginInit();
            ((System.ComponentModel.ISupportInitialize)student).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // teacher
            // 
            teacher.Image = (Image)resources.GetObject("teacher.Image");
            teacher.Location = new Point(721, 294);
            teacher.Name = "teacher";
            teacher.Size = new Size(85, 139);
            teacher.TabIndex = 0;
            teacher.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(39, 39);
            label1.Name = "label1";
            label1.Size = new Size(186, 25);
            label1.TabIndex = 1;
            label1.Text = "Кол-во студентов M";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(39, 87);
            label2.Name = "label2";
            label2.Size = new Size(326, 25);
            label2.TabIndex = 2;
            label2.Text = "Интервал поступления в очередь t0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(39, 131);
            label3.Name = "label3";
            label3.Size = new Size(373, 25);
            label3.TabIndex = 3;
            label3.Text = "Время на ответ 1 вопроса у ассистента t1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(39, 273);
            label4.Name = "label4";
            label4.Size = new Size(339, 25);
            label4.TabIndex = 6;
            label4.Text = "Кол-во вопросов у преподавателя n2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F);
            label5.Location = new Point(39, 225);
            label5.Name = "label5";
            label5.Size = new Size(302, 25);
            label5.TabIndex = 5;
            label5.Text = "Кол-во вопросов у ассистента n1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F);
            label6.Location = new Point(39, 183);
            label6.Name = "label6";
            label6.Size = new Size(410, 25);
            label6.TabIndex = 4;
            label6.Text = "Время на ответ 1 вопроса у преподавателя t2";
            // 
            // textBoxStudentCount
            // 
            textBoxStudentCount.Location = new Point(511, 39);
            textBoxStudentCount.Name = "textBoxStudentCount";
            textBoxStudentCount.Size = new Size(55, 23);
            textBoxStudentCount.TabIndex = 7;
            // 
            // textBoxArrivalInterval
            // 
            textBoxArrivalInterval.Location = new Point(453, 87);
            textBoxArrivalInterval.Name = "textBoxArrivalInterval";
            textBoxArrivalInterval.Size = new Size(113, 23);
            textBoxArrivalInterval.TabIndex = 8;
            // 
            // textBoxQuestionTimeAssistant
            // 
            textBoxQuestionTimeAssistant.Location = new Point(453, 131);
            textBoxQuestionTimeAssistant.Name = "textBoxQuestionTimeAssistant";
            textBoxQuestionTimeAssistant.Size = new Size(113, 23);
            textBoxQuestionTimeAssistant.TabIndex = 9;
            // 
            // textBoxQuestionTimeProfessor
            // 
            textBoxQuestionTimeProfessor.Location = new Point(453, 183);
            textBoxQuestionTimeProfessor.Name = "textBoxQuestionTimeProfessor";
            textBoxQuestionTimeProfessor.Size = new Size(113, 23);
            textBoxQuestionTimeProfessor.TabIndex = 10;
            // 
            // textBoxQuestionsProfessor
            // 
            textBoxQuestionsProfessor.Location = new Point(511, 273);
            textBoxQuestionsProfessor.Name = "textBoxQuestionsProfessor";
            textBoxQuestionsProfessor.Size = new Size(55, 23);
            textBoxQuestionsProfessor.TabIndex = 12;
            // 
            // textBoxQuestionsAssistant
            // 
            textBoxQuestionsAssistant.Location = new Point(511, 225);
            textBoxQuestionsAssistant.Name = "textBoxQuestionsAssistant";
            textBoxQuestionsAssistant.Size = new Size(55, 23);
            textBoxQuestionsAssistant.TabIndex = 11;
            // 
            // startModelingBtn
            // 
            startModelingBtn.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            startModelingBtn.Location = new Point(161, 348);
            startModelingBtn.Name = "startModelingBtn";
            startModelingBtn.Size = new Size(288, 33);
            startModelingBtn.TabIndex = 13;
            startModelingBtn.Text = "Начать моделирование";
            startModelingBtn.UseVisualStyleBackColor = true;
            startModelingBtn.Click += startModelingBtn_Click;
            // 
            // assistant
            // 
            assistant.Image = (Image)resources.GetObject("assistant.Image");
            assistant.Location = new Point(1037, 310);
            assistant.Name = "assistant";
            assistant.Size = new Size(74, 123);
            assistant.TabIndex = 14;
            assistant.TabStop = false;
            // 
            // student
            // 
            student.Image = (Image)resources.GetObject("student.Image");
            student.Location = new Point(859, 39);
            student.Name = "student";
            student.Size = new Size(96, 132);
            student.TabIndex = 15;
            student.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label7.Location = new Point(708, 436);
            label7.Name = "label7";
            label7.Size = new Size(118, 25);
            label7.TabIndex = 16;
            label7.Text = "Профессор";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label8.Location = new Point(1023, 436);
            label8.Name = "label8";
            label8.Size = new Size(104, 25);
            label8.TabIndex = 17;
            label8.Text = "Ассистент";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1196, -8);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(169, 195);
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label9.Location = new Point(1250, 162);
            label9.Name = "label9";
            label9.Size = new Size(74, 25);
            label9.TabIndex = 19;
            label9.Text = "Выход";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label10.Location = new Point(782, 11);
            label10.Name = "label10";
            label10.Size = new Size(114, 25);
            label10.TabIndex = 20;
            label10.Text = "Аудитория";
            // 
            // trackBarSpeed
            // 
            trackBarSpeed.Location = new Point(192, 427);
            trackBarSpeed.Name = "trackBarSpeed";
            trackBarSpeed.Size = new Size(220, 45);
            trackBarSpeed.TabIndex = 21;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(192, 499);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(220, 23);
            progressBar1.TabIndex = 22;
            // 
            // labelGradeProf
            // 
            labelGradeProf.AutoSize = true;
            labelGradeProf.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelGradeProf.Location = new Point(684, 471);
            labelGradeProf.Name = "labelGradeProf";
            labelGradeProf.Size = new Size(0, 25);
            labelGradeProf.TabIndex = 23;
            // 
            // labelGradeAss
            // 
            labelGradeAss.AutoSize = true;
            labelGradeAss.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelGradeAss.Location = new Point(1011, 471);
            labelGradeAss.Name = "labelGradeAss";
            labelGradeAss.Size = new Size(0, 25);
            labelGradeAss.TabIndex = 24;
            // 
            // examStatusLbl
            // 
            examStatusLbl.AutoSize = true;
            examStatusLbl.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            examStatusLbl.Location = new Point(236, 384);
            examStatusLbl.Name = "examStatusLbl";
            examStatusLbl.Size = new Size(0, 25);
            examStatusLbl.TabIndex = 25;
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Student);
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(511, 499);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(934, 652);
            dataGridView1.TabIndex = 26;
            // 
            // numericUpDownScore2
            // 
            numericUpDownScore2.Location = new Point(236, 580);
            numericUpDownScore2.Name = "numericUpDownScore2";
            numericUpDownScore2.Size = new Size(120, 23);
            numericUpDownScore2.TabIndex = 27;
            // 
            // numericUpDownScore3
            // 
            numericUpDownScore3.Location = new Point(236, 623);
            numericUpDownScore3.Name = "numericUpDownScore3";
            numericUpDownScore3.Size = new Size(120, 23);
            numericUpDownScore3.TabIndex = 28;
            // 
            // numericUpDownScore4
            // 
            numericUpDownScore4.Location = new Point(236, 668);
            numericUpDownScore4.Name = "numericUpDownScore4";
            numericUpDownScore4.Size = new Size(120, 23);
            numericUpDownScore4.TabIndex = 29;
            // 
            // numericUpDownScore5
            // 
            numericUpDownScore5.Location = new Point(236, 717);
            numericUpDownScore5.Name = "numericUpDownScore5";
            numericUpDownScore5.Size = new Size(120, 23);
            numericUpDownScore5.TabIndex = 30;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14.25F);
            label11.Location = new Point(12, 580);
            label11.Name = "label11";
            label11.Size = new Size(206, 25);
            label11.TabIndex = 1;
            label11.Text = "Процент для оценки 2";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 14.25F);
            label12.Location = new Point(12, 623);
            label12.Name = "label12";
            label12.Size = new Size(206, 25);
            label12.TabIndex = 31;
            label12.Text = "Процент для оценки 3";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 14.25F);
            label13.Location = new Point(12, 668);
            label13.Name = "label13";
            label13.Size = new Size(206, 25);
            label13.TabIndex = 32;
            label13.Text = "Процент для оценки 4";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 14.25F);
            label14.Location = new Point(12, 717);
            label14.Name = "label14";
            label14.Size = new Size(206, 25);
            label14.TabIndex = 33;
            label14.Text = "Процент для оценки 5";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 14.25F);
            label15.Location = new Point(81, 427);
            label15.Name = "label15";
            label15.Size = new Size(105, 25);
            label15.TabIndex = 1;
            label15.Text = "Ускорение";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14.25F);
            label16.Location = new Point(12, 497);
            label16.Name = "label16";
            label16.Size = new Size(163, 25);
            label16.TabIndex = 1;
            label16.Text = "Шкала прогресса";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(961, 39);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(96, 132);
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(649, 39);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(96, 132);
            pictureBox3.TabIndex = 15;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(751, 39);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(96, 132);
            pictureBox4.TabIndex = 15;
            pictureBox4.TabStop = false;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button1.Location = new Point(12, 813);
            button1.Name = "button1";
            button1.Size = new Size(183, 32);
            button1.TabIndex = 34;
            button1.Text = "Тест 3лр";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1630, 1126);
            Controls.Add(button1);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(numericUpDownScore5);
            Controls.Add(numericUpDownScore4);
            Controls.Add(numericUpDownScore3);
            Controls.Add(numericUpDownScore2);
            Controls.Add(dataGridView1);
            Controls.Add(examStatusLbl);
            Controls.Add(labelGradeAss);
            Controls.Add(labelGradeProf);
            Controls.Add(progressBar1);
            Controls.Add(trackBarSpeed);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(pictureBox2);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(student);
            Controls.Add(assistant);
            Controls.Add(startModelingBtn);
            Controls.Add(textBoxQuestionsProfessor);
            Controls.Add(textBoxQuestionsAssistant);
            Controls.Add(textBoxQuestionTimeProfessor);
            Controls.Add(textBoxQuestionTimeAssistant);
            Controls.Add(textBoxArrivalInterval);
            Controls.Add(textBoxStudentCount);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label11);
            Controls.Add(label1);
            Controls.Add(teacher);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)teacher).EndInit();
            ((System.ComponentModel.ISupportInitialize)assistant).EndInit();
            ((System.ComponentModel.ISupportInitialize)student).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScore5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox teacher;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBoxStudentCount;
        private TextBox textBoxArrivalInterval;
        private TextBox textBoxQuestionTimeAssistant;
        private TextBox textBoxQuestionTimeProfessor;
        private TextBox textBoxQuestionsProfessor;
        private TextBox textBoxQuestionsAssistant;
        private Button startModelingBtn;
        private PictureBox assistant;
        private PictureBox student;
        private Label label7;
        private Label label8;
        private PictureBox pictureBox2;
        private Label label9;
        private Label label10;
        private TrackBar trackBarSpeed;
        private ProgressBar progressBar1;
        private Label labelGradeProf;
        private Label labelGradeAss;
        private Label examStatusLbl;
        private BindingSource studentBindingSource;
        private DataGridView dataGridView1;
        private NumericUpDown numericUpDownScore2;
        private NumericUpDown numericUpDownScore3;
        private NumericUpDown numericUpDownScore4;
        private NumericUpDown numericUpDownScore5;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Button button1;
    }
}

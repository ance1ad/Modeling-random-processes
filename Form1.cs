using System.Collections;
using Aspose.Cells; //  Excel

namespace firstLab
{
    public partial class Form1 : Form
    {
        private Queue<Student> studentQueue = new Queue<Student>();
        private Random random = new Random();
        private int totalStudents = 15; // M - ���������� ���������
        private static double t0 = 10; // �������� ������� ���������
        private double t0L; // ���� ������� ����� ��������
        private static double t1 = 5; // ����� �� ������ ���������� 
        private double t1L; // ���� �� ����� ������ ���������� 
        private static double t2 = 7; // ����� �� ������ ���������� 
        private static double old_t2 = 7; // �������� ����� �� ������ ���������� ��� �������� � ����� �������
        private double t2L;
        private double t0_2; // ����� �������� ������ � �������
        private int n1 = 5; // ���������� �������� � ����������
        private int n2 = 5; // ���������� �������� � ����������
        private int exiteStudents = 0; // ������� ������������ ���������
        private int visitedStudents = 0; // ���� ����� ������, ���� �� ���� > �������

        private Point startPosition = new Point(859, 39); // ���������
        private Point assistantPosition = new Point(1037, 310); // ���������
        private Point professorPosition = new Point(683, 310); // ���������
        private Point exitPosition = new Point(1246, 3); // �����
        private int id = 0; // ���������� ��������� �� ��������������
        Student nextStudent; // ��������� ������ ��� ������ � �������
        Student proffStudent; // ����������� ����������
        Student doljnik; // ����������� ����������
        bool doljnikWaiting = false;

        private bool assistantAvailable = true; // ���� ��� �������� ��������� ����������
        private bool professorAvailable = true; // ���� ��� �������� ��������� ����������
        private double dT = 0.5; // delta time
        private double T = 0; // global time

        // 2�� �������������� ������
        // ����� ������ �� ������ ���������� 
        private double sigmaT1 = 1;
        private double mT1 = 5;
        // ����� ������ �� ������ ����������
        private double sigmaT2 = 1;
        private double mT2 = 4;
        // ����� �� ������ �������� - ���������������� �����
        private double lambdaT0 = 0.1;
        // ����� ��� ������


        double[] C1 = { 0.11, 0.024, 0.766, 0.30 }; // C1 ����������� ��� ����������
        double[] C2 = { 0.159, 0.03, 1.07, 0.429 }; // �2 ����������� ��� ����������
        StationaryGenerator disturbAssist;
        StationaryGenerator disturbProff;

        Generator generator = new Generator();


        public Form1()
        {
            InitializeComponent();
            trackBarSpeed.Value = 10;
            trackBarSpeed.Minimum = 0;
            trackBarSpeed.Maximum = 100;

            disturbAssist = new StationaryGenerator(C1, 0, 5);
            disturbProff = new StationaryGenerator(C2, 0, 7);


            //  ���� �� ���������
            double speedFactor = 1.0; // ��������� �������� ������������� (���������� �����)

            trackBarSpeed.Scroll += (s, e) =>
            {
                // ������������� speedFactor � ����������� �� ������� ��������
                speedFactor = trackBarSpeed.Value / 10.0; // ��� ������������ �������� �� 1 �� 10
            };


            progressBar1.Value = 0;
            label10.Text = $"�������� ��������� � ���������: {totalStudents}";

            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "��������� �����";
            dataGridView1.Columns[1].Name = "ID ��������";
            dataGridView1.Columns[2].Name = "������ ����������";
            dataGridView1.Columns[3].Name = "������ ����������";
            dataGridView1.Columns[4].Name = "������";
            dataGridView1.Columns[5].Name = "���� �������������";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxStudentCount.Text = "15";
            textBoxQuestionTimeAssistant.Text = t1.ToString();
            textBoxQuestionTimeProfessor.Text = t2.ToString();
            textBoxArrivalInterval.Text = t0.ToString();
            textBoxQuestionsAssistant.Text = n1.ToString();
            textBoxQuestionsProfessor.Text = n2.ToString();


            // ��������� �������� �� ��������� ��� ����� ���������� (����� ������ � ����������)
            numericUpDownScore2.Value = 30;
            numericUpDownScore3.Value = 50;
            numericUpDownScore4.Value = 70;
            numericUpDownScore5.Value = 100;
        }


        private void startModelingBtn_Click(object sender, EventArgs e)
        {
            // ����������� ���������
            SimulationSetup();
            StartModeling();
        }


        private void SimulationSetup()
        {
            examStatusLbl.Text = "������� ���";
            totalStudents = int.Parse(textBoxStudentCount.Text);
            progressBar1.Maximum = totalStudents;


            t0 = generator.ExponentialFunction(lambdaT0);

            t1 = generator.NormalFunction(sigmaT1, mT1) + disturbAssist.GetStartValue();
            t2 = generator.NormalFunction(sigmaT2, mT2) + disturbProff.GetStartValue();

            n1 = int.Parse(textBoxQuestionsAssistant.Text);
            n2 = int.Parse(textBoxQuestionsProfessor.Text);


            studentQueue.Clear();
            dataGridView1.Rows.Clear();
            studentQueue.Clear();
            exiteStudents = 0;
            progressBar1.Value = 0;
            T = 0;
            t1L = t0 + generator.NormalFunction(sigmaT1, mT1); ; // ���� �� ����� ������ ���������� 
            t0L = t0;
            t2L = 0;
            assistantAvailable = true;
            professorAvailable = true;
            visitedStudents = 0;
            id = 0;
            exiteStudents = 0;
            label10.Text = $"�������� ��������� � ���������: {totalStudents}";
        }


        private async void StartModeling()
        {
            while (exiteStudents < totalStudents)
            {

                label15.Text = trackBarSpeed.Value.ToString();
                // �������� ��������
                if (T >= t0_2 && doljnikWaiting)
                {
                    studentQueue.Enqueue(doljnik);
                    doljnikWaiting = false;
                }
                // �������� ��������� � ���������� t�=10 ����� � �������
                if (T >= t0L && exiteStudents < totalStudents)
                {
                    if (visitedStudents < totalStudents) // ��������� ������
                    {
                        var student = new Student { Id = id + 1, Picture = CreateStudentPicture() }; id++;
                        AddStudentResultToDataGrid(T, student, "������ �������", "-");

                        studentQueue.Enqueue(student);
                        Controls.Add(student.Picture);
                        visitedStudents++;
                        label10.Text = $"��������� � ���������: {studentQueue.Count}";
                    }

                    // �������� ���� � ������� ����������� � ����������
                    await ProcessNextStudent();
                    t0 = generator.ExponentialFunction(lambdaT0);
                    textBoxArrivalInterval.Text = t0.ToString();
                    t0L += t0; // ������ ����� ��������
                    await Task.Delay(trackBarSpeed.Value / 5);

                }

                if (!assistantAvailable && T >= t1L)
                {
                    await ProcessWithAssistant();
                }
                if (!professorAvailable && T >= t2L)
                {
                    await ProcessWithProfessor();
                }

                T += dT;

            }
            AddStudentResultToDataGrid(T, nextStudent, "����� ��������", "-");
            label10.Text = $"�������� ��������� � ���������: {0}";
        }

        // 2 ������
        private void GeneratorTest()
        {
            const int N = 2000;
            Generator generator = new Generator();

            ArrayList NormalArray = new ArrayList();
            ArrayList ExponentialArray = new ArrayList();

            double exp, normal;

            for (int i = 0; i < N; i++)
            {
                exp = generator.ExponentialFunction(lambdaT0);
                ExponentialArray.Add(exp);

                normal = generator.NormalFunction(sigmaT1, mT1);
                NormalArray.Add(normal);
            }

            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets.Add("������");
            sheet.Cells.ImportArrayList(NormalArray, 1, 1, true);
            sheet.Cells.ImportArrayList(ExponentialArray, 1, 0, true);
            sheet.AutoFitColumns();
            workbook.Save("�������� ����������.xlsx");
        }


        private async Task ProcessNextStudent()
        {
            if (exiteStudents == totalStudents || studentQueue.Count == 0) return;
            if (!assistantAvailable || !professorAvailable) return;


            assistantAvailable = false;
            nextStudent = studentQueue.Dequeue();
            AddStudentResultToDataGrid(T, nextStudent, "�� ����� ����������", "������ � ����������");
            await MoveToPosition(nextStudent.Picture, assistantPosition);
            await ProcessWithAssistant();
        }


        private async Task ProcessWithAssistant()
        {
            double t2Info;
            // �� t1 ����� ����� �� ������
            if (T >= t1L)
            {
                t2Info = disturbAssist.GetNextValue();
                AddStudentResultToDataGrid(T, nextStudent, "������� �������� ����������", $"����� ��� ������: {t2Info}");

                t1 = generator.NormalFunction(sigmaT1, mT1) + t2Info;
                textBoxQuestionTimeAssistant.Text = t1.ToString();
                t1L = T + t1; ; // ����� ����� �� ������

                if (nextStudent.AssistQuestoCount < n1)
                {
                    bool isCorrect = random.NextDouble() < 0.5;
                    if (isCorrect) nextStudent.CorrectAnswerCount++;
                    nextStudent.AssistQuestoCount++;
                    await Task.Delay(trackBarSpeed.Value / 5);

                }

                if (nextStudent.AssistQuestoCount == n1) // �� ������� �������
                {
                    int score = CalculateScore(nextStudent.CorrectAnswerCount, n1);
                    nextStudent.AssistantScore = score;

                    AddStudentResultToDataGrid(T, nextStudent, "������ �����������", $"������: {score}");
                    assistantAvailable = true;

                    if (score == 3 || score == 4)
                    {
                        await MoveToPosition(nextStudent.Picture, exitPosition);
                        CompleteExamProcess(nextStudent);
                        AddStudentResultToDataGrid(T, nextStudent, "������� �������", "�������� ������� �� ����� ����������");
                    }
                    else // �������� � �����������
                    {
                        if (!professorAvailable) return;
                        professorAvailable = false;
                        proffStudent = nextStudent;// ����������� ������, ���� ����� ������� ������ ��������
                        await MoveToPosition(nextStudent.Picture, professorPosition);


                        /////////////////////////////////// ��� ��������� ��� 3 �� ��������� + ����� ��� ������ � ����������
                        t2Info = disturbProff.GetNextValue();

                        t2 = generator.NormalFunction(sigmaT1, mT1) + t2Info;
                        textBoxQuestionTimeProfessor.Text = t2.ToString();
                        t2L = T + t2; // ����� ���������� ��� ����������
                        proffStudent.CorrectAnswerCount = 0;
                        AddStudentResultToDataGrid(T, proffStudent, "�� ����� ����������", "������ � ����������");
                        await ProcessWithProfessor();
                    }
                }
            }
        }



        private async Task ProcessWithProfessor()
        {
            if (T >= t2L)
            {

                /////////////////////////////////// ��� ��������� ��� 3 ��, + ����� ��� ������ ����������
                double t2Info = disturbProff.GetNextValue();
                AddStudentResultToDataGrid(T, proffStudent, "������� �������� ����������", $"����� ��� ������: {t2Info}");


                t2 = generator.NormalFunction(sigmaT2, mT2) + t2Info;
                textBoxQuestionTimeProfessor.Text = t2.ToString();
                t2L = T + t2; // ����� �� ��������� ����� � �
                if (proffStudent.ProfessorQuestoCount < n2)
                {
                    bool isCorrect = random.NextDouble() < 0.5;
                    if (isCorrect) proffStudent.CorrectAnswerCount++;
                    proffStudent.ProfessorQuestoCount++;
                }
                if (proffStudent.ProfessorQuestoCount == n2)
                {
                    int score = CalculateScore(proffStudent.CorrectAnswerCount, n2);
                    proffStudent.ProfessorScore = score;

                    AddStudentResultToDataGrid(T, proffStudent, "������ �����������", $"������: {score}");

                    if (score == 2)
                    {
                        await MoveToPosition(proffStudent.Picture, startPosition);
                        doljnik = proffStudent;
                        t0_2 = T + 2 * old_t2; // ����� �������� � ����� �������
                        doljnikWaiting = true;
                        AddStudentResultToDataGrid(T, proffStudent, "���������", "�������� �� ���������");
                        proffStudent.ProfessorQuestoCount = 0;
                        proffStudent.AssistantScore = 0;
                    }
                    else
                    {
                        await MoveToPosition(proffStudent.Picture, exitPosition);
                        CompleteExamProcess(proffStudent);
                        AddStudentResultToDataGrid(T, proffStudent, "������� �������", "�������� ������� � ����������");
                    }
                    professorAvailable = true;
                }
            }
        }


        private async Task MoveToPosition(PictureBox picture, Point targetPosition)
        {
            int steps = 20;
            int deltaX = (targetPosition.X - picture.Location.X) / steps;
            int deltaY = (targetPosition.Y - picture.Location.Y) / steps;

            for (int i = 0; i < steps; i++)
            {
                picture.Location = new Point(picture.Location.X + deltaX, picture.Location.Y + deltaY);
                await Task.Delay(trackBarSpeed.Value / 5);
            }
        }


        async private void CompleteExamProcess(Student student)
        {
            Controls.Remove(student.Picture);
            label10.Text = $"�������� ��������� � ���������: {studentQueue.Count}";
            progressBar1.Value++;
            exiteStudents++;
            if (exiteStudents == totalStudents)
            {
                examStatusLbl.Text = "������� ��������";
            }
        }


        private void AddStudentResultToDataGrid(double T, Student student, string status, string stepDescription = "")
        {
            // ��������� ������ � ��������� � ������
            int rowIndex = dataGridView1.Rows.Add(T,
                                                  student.Id,
                                                  student.AssistantScore > 0 ? student.AssistantScore.ToString() : "-",
                                                  student.ProfessorScore > 0 ? student.ProfessorScore.ToString() : "-",
                                                  status,
                                                  stepDescription);

            if (status == "���������")
            {
                dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            }
            if (status == "������� �������")
            {
                dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Green;
            }
        }


        private PictureBox CreateStudentPicture()
        {
            var picture = new PictureBox
            {
                Size = new Size(30, 30),
                BackColor = Color.Blue,
                Location = startPosition
            };
            return picture;
        }


        private int CalculateScore(int correctAnswers, int totalQuestions)
        {
            decimal percentage = (decimal)correctAnswers / totalQuestions * 100;

            if (percentage < numericUpDownScore2.Value) return 2;
            if (percentage < numericUpDownScore3.Value) return 3;
            if (percentage < numericUpDownScore4.Value) return 4;
            return 5;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // ������� ���� ������ ��� �������� ���� �������� ��������
            ArrayList StationaryArray1 = new ArrayList();
            ArrayList StationaryArray2 = new ArrayList();

            // ������� ��������� ���������� ������������� ���������� ��������
            StationaryGenerator stationaryGenerator1 = new StationaryGenerator(C2, 0, 7);
            StationaryGenerator stationaryGenerator2 = new StationaryGenerator(C2, 0, 7);

            for (int i = 0; i < 2000; i++)
            {
                if(i%2==0)
                    StationaryArray1.Add(stationaryGenerator1.GetNextValue());
                else
                    StationaryArray2.Add(stationaryGenerator2.GetNextValue());
            }

            // ������� ����� Excel-���� � ��������� ���� ��� ������ ������
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets.Add("������������ �������");

            // ������
            sheet.Cells.ImportArrayList(StationaryArray1, 1, 0, true);
            sheet.Cells.ImportArrayList(StationaryArray2, 1, 1, true);

            // ������������� ��������� ������ ������� ��� ����������
            sheet.AutoFitColumns();

            // ������� ������ StationaryArray
            StationaryArray1.Clear();
            StationaryArray2.Clear();

            // ��������� Excel-���� � ������ "StationaryProcess.xlsx"
            workbook.Save("���� ����������.xlsx");
        }
    }


    public class Student
    {
        public int Id { get; set; }
        public int AssistantScore { get; set; } = 0;
        public int ProfessorScore { get; set; } = 0;
        public int CorrectAnswerCount { get; set; } = 0;
        public int AssistQuestoCount { get; set; } = 0;
        public int ProfessorQuestoCount { get; set; } = 0;
        public PictureBox Picture { get; set; }
    }

    public class Generator
    {
        private readonly Random random = new Random();

        public double ExponentialFunction(double lambda) =>  -(1 / lambda) * Math.Log(random.NextDouble());
        
        public double NormalFunction(double sigma, double m) => (sigma * Math.Cos(2 * Math.PI * random.NextDouble())
                * Math.Sqrt(-2 * Math.Log(random.NextDouble()))) + m;
    }

    public class StationaryGenerator
    {
        private List<double> q; 
        private double[] C;
        private double M;

        private Generator generator = new Generator(); 
        public StationaryGenerator(double[] C, double min, double max)
        {
            M = (max + min) / 2;
            q = new List<double>();
            this.C = C;

            // ��������� ������ q ���������� ���������� � ������� ������� � ��������� ����������
            for (int i = 0; i < C.Length; i++) { q.Add(generator.NormalFunction(1, 0)); } // ���������� ��������� �������� qi 
        }

        public double GetStartValue() => Enumerable.Range(0, C.Length).Select(i => C[i] * q[i]).Sum() + M;

        public double GetNextValue()
        {
            q.Remove(q.FirstOrDefault());
            q.Add(generator.NormalFunction(1, 0));

            double sum = 0;

            for (int i = 0; i < C.Length; i++)
            {
                sum += C[i] * q[i];
            }
            return sum + M;

        }
    }
}
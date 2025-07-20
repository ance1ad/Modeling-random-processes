using System.Collections;
using Aspose.Cells; //  Excel

namespace firstLab
{
    public partial class Form1 : Form
    {
        private Queue<Student> studentQueue = new Queue<Student>();
        private Random random = new Random();
        private int totalStudents = 15; // M - количество студентов
        private static double t0 = 10; // интервал прихода студентов
        private double t0L; // часы прихода некст студента
        private static double t1 = 5; // время на вопрос ассистента 
        private double t1L; // часы на некст вопрос ассистента 
        private static double t2 = 7; // время на вопрос профессора 
        private static double old_t2 = 7; // сохраним время на вопрос профессора для перехода в конец очереди
        private double t2L;
        private double t0_2; // когда двоешник пойдет в очередь
        private int n1 = 5; // количество вопросов у ассистента
        private int n2 = 5; // количество вопросов у профессора
        private int exiteStudents = 0; // счетчик обработанных студентов
        private int visitedStudents = 0; // скок всего пришло, чтоб не было > нужного

        private Point startPosition = new Point(859, 39); // аудитория
        private Point assistantPosition = new Point(1037, 310); // ассистент
        private Point professorPosition = new Point(683, 310); // профессор
        private Point exitPosition = new Point(1246, 3); // выход
        private int id = 0; // заполнение студентов по идентификатору
        Student nextStudent; // временная ссылка для работы с челиком
        Student proffStudent; // освобождает ассистента
        Student doljnik; // освобождает ассистента
        bool doljnikWaiting = false;

        private bool assistantAvailable = true; // флаг для проверки занятости ассистента
        private bool professorAvailable = true; // флаг для проверки занятости профессора
        private double dT = 0.5; // delta time
        private double T = 0; // global time

        // 2лр Стохастическая модель
        // Время ответа на вопрос ассистента 
        private double sigmaT1 = 1;
        private double mT1 = 5;
        // Время ответа на вопрос профессора
        private double sigmaT2 = 1;
        private double mT2 = 4;
        // Время на приход студента - экспоненциальный закон
        private double lambdaT0 = 0.1;
        // пауза при ответе


        double[] C1 = { 0.11, 0.024, 0.766, 0.30 }; // C1 вычисленные для ассистента
        double[] C2 = { 0.159, 0.03, 1.07, 0.429 }; // С2 вычисленные для профессора
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


            //  Пока не учитываем
            double speedFactor = 1.0; // Начальная скорость моделирования (нормальное время)

            trackBarSpeed.Scroll += (s, e) =>
            {
                // Устанавливаем speedFactor в зависимости от позиции слайдера
                speedFactor = trackBarSpeed.Value / 10.0; // Для нормализации значения от 1 до 10
            };


            progressBar1.Value = 0;
            label10.Text = $"Осталось студентов в аудитории: {totalStudents}";

            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Модельное время";
            dataGridView1.Columns[1].Name = "ID Студента";
            dataGridView1.Columns[2].Name = "Оценка Ассистента";
            dataGridView1.Columns[3].Name = "Оценка Профессора";
            dataGridView1.Columns[4].Name = "Статус";
            dataGridView1.Columns[5].Name = "Шаги моделирования";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxStudentCount.Text = "15";
            textBoxQuestionTimeAssistant.Text = t1.ToString();
            textBoxQuestionTimeProfessor.Text = t2.ToString();
            textBoxArrivalInterval.Text = t0.ToString();
            textBoxQuestionsAssistant.Text = n1.ToString();
            textBoxQuestionsProfessor.Text = n2.ToString();


            // Установка значений по умолчанию для шкалы оценивания (можно менять в интерфейсе)
            numericUpDownScore2.Value = 30;
            numericUpDownScore3.Value = 50;
            numericUpDownScore4.Value = 70;
            numericUpDownScore5.Value = 100;
        }


        private void startModelingBtn_Click(object sender, EventArgs e)
        {
            // Настраиваем параметры
            SimulationSetup();
            StartModeling();
        }


        private void SimulationSetup()
        {
            examStatusLbl.Text = "Экзамен идёт";
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
            t1L = t0 + generator.NormalFunction(sigmaT1, mT1); ; // часы на некст вопрос ассистента 
            t0L = t0;
            t2L = 0;
            assistantAvailable = true;
            professorAvailable = true;
            visitedStudents = 0;
            id = 0;
            exiteStudents = 0;
            label10.Text = $"Осталось студентов в аудитории: {totalStudents}";
        }


        private async void StartModeling()
        {
            while (exiteStudents < totalStudents)
            {

                label15.Text = trackBarSpeed.Value.ToString();
                // Проверка должника
                if (T >= t0_2 && doljnikWaiting)
                {
                    studentQueue.Enqueue(doljnik);
                    doljnikWaiting = false;
                }
                // Студенты поступают с интервалом tо=10 минут в очередь
                if (T >= t0L && exiteStudents < totalStudents)
                {
                    if (visitedStudents < totalStudents) // добавляем нового
                    {
                        var student = new Student { Id = id + 1, Picture = CreateStudentPicture() }; id++;
                        AddStudentResultToDataGrid(T, student, "Прибыл студент", "-");

                        studentQueue.Enqueue(student);
                        Controls.Add(student.Picture);
                        visitedStudents++;
                        label10.Text = $"Студентов в аудитории: {studentQueue.Count}";
                    }

                    // Студенты идут в порядке очередности к ассистенту
                    await ProcessNextStudent();
                    t0 = generator.ExponentialFunction(lambdaT0);
                    textBoxArrivalInterval.Text = t0.ToString();
                    t0L += t0; // приход некст студента
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
            AddStudentResultToDataGrid(T, nextStudent, "Конец экзамена", "-");
            label10.Text = $"Осталось студентов в аудитории: {0}";
        }

        // 2 работа
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
            Worksheet sheet = workbook.Worksheets.Add("Замеры");
            sheet.Cells.ImportArrayList(NormalArray, 1, 1, true);
            sheet.Cells.ImportArrayList(ExponentialArray, 1, 0, true);
            sheet.AutoFitColumns();
            workbook.Save("Проверка генератора.xlsx");
        }


        private async Task ProcessNextStudent()
        {
            if (exiteStudents == totalStudents || studentQueue.Count == 0) return;
            if (!assistantAvailable || !professorAvailable) return;


            assistantAvailable = false;
            nextStudent = studentQueue.Dequeue();
            AddStudentResultToDataGrid(T, nextStudent, "На этапе ассистента", "Прибыл к ассистенту");
            await MoveToPosition(nextStudent.Picture, assistantPosition);
            await ProcessWithAssistant();
        }


        private async Task ProcessWithAssistant()
        {
            double t2Info;
            // По t1 минут время на вопрос
            if (T >= t1L)
            {
                t2Info = disturbAssist.GetNextValue();
                AddStudentResultToDataGrid(T, nextStudent, "Студент отвечает ассистенту", $"Пауза при ответе: {t2Info}");

                t1 = generator.NormalFunction(sigmaT1, mT1) + t2Info;
                textBoxQuestionTimeAssistant.Text = t1.ToString();
                t1L = T + t1; ; // некст время на вопрос

                if (nextStudent.AssistQuestoCount < n1)
                {
                    bool isCorrect = random.NextDouble() < 0.5;
                    if (isCorrect) nextStudent.CorrectAnswerCount++;
                    nextStudent.AssistQuestoCount++;
                    await Task.Delay(trackBarSpeed.Value / 5);

                }

                if (nextStudent.AssistQuestoCount == n1) // на вопросы ответил
                {
                    int score = CalculateScore(nextStudent.CorrectAnswerCount, n1);
                    nextStudent.AssistantScore = score;

                    AddStudentResultToDataGrid(T, nextStudent, "Оценен ассистентом", $"Оценка: {score}");
                    assistantAvailable = true;

                    if (score == 3 || score == 4)
                    {
                        await MoveToPosition(nextStudent.Picture, exitPosition);
                        CompleteExamProcess(nextStudent);
                        AddStudentResultToDataGrid(T, nextStudent, "Покинул экзамен", "Завершил экзамен на этапе ассистента");
                    }
                    else // работаем с профессором
                    {
                        if (!professorAvailable) return;
                        professorAvailable = false;
                        proffStudent = nextStudent;// присваиваем ссылку, чтоб потом вызвать нового студента
                        await MoveToPosition(nextStudent.Picture, professorPosition);


                        /////////////////////////////////// тут изменения для 3 лр добавляем + паузу для ответа у ассистента
                        t2Info = disturbProff.GetNextValue();

                        t2 = generator.NormalFunction(sigmaT1, mT1) + t2Info;
                        textBoxQuestionTimeProfessor.Text = t2.ToString();
                        t2L = T + t2; // сразу просчитаем для профессора
                        proffStudent.CorrectAnswerCount = 0;
                        AddStudentResultToDataGrid(T, proffStudent, "На этапе профессора", "Прибыл к профессору");
                        await ProcessWithProfessor();
                    }
                }
            }
        }



        private async Task ProcessWithProfessor()
        {
            if (T >= t2L)
            {

                /////////////////////////////////// тут изменения для 3 лр, + пауза для ответа профессору
                double t2Info = disturbProff.GetNextValue();
                AddStudentResultToDataGrid(T, proffStudent, "Студент отвечает профессору", $"Пауза при ответе: {t2Info}");


                t2 = generator.NormalFunction(sigmaT2, mT2) + t2Info;
                textBoxQuestionTimeProfessor.Text = t2.ToString();
                t2L = T + t2; // время на следующий ответ у П
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

                    AddStudentResultToDataGrid(T, proffStudent, "Оценен профессором", $"Оценка: {score}");

                    if (score == 2)
                    {
                        await MoveToPosition(proffStudent.Picture, startPosition);
                        doljnik = proffStudent;
                        t0_2 = T + 2 * old_t2; // время перехода в конец очереди
                        doljnikWaiting = true;
                        AddStudentResultToDataGrid(T, proffStudent, "Пересдача", "Вернулся на пересдачу");
                        proffStudent.ProfessorQuestoCount = 0;
                        proffStudent.AssistantScore = 0;
                    }
                    else
                    {
                        await MoveToPosition(proffStudent.Picture, exitPosition);
                        CompleteExamProcess(proffStudent);
                        AddStudentResultToDataGrid(T, proffStudent, "Покинул экзамен", "Завершил экзамен у профессора");
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
            label10.Text = $"Осталось студентов в аудитории: {studentQueue.Count}";
            progressBar1.Value++;
            exiteStudents++;
            if (exiteStudents == totalStudents)
            {
                examStatusLbl.Text = "Экзамен завершён";
            }
        }


        private void AddStudentResultToDataGrid(double T, Student student, string status, string stepDescription = "")
        {
            // Добавляем строку и сохраняем её индекс
            int rowIndex = dataGridView1.Rows.Add(T,
                                                  student.Id,
                                                  student.AssistantScore > 0 ? student.AssistantScore.ToString() : "-",
                                                  student.ProfessorScore > 0 ? student.ProfessorScore.ToString() : "-",
                                                  status,
                                                  stepDescription);

            if (status == "Пересдача")
            {
                dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            }
            if (status == "Покинул экзамен")
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
            // Создаем один список для хранения всех значений процесса
            ArrayList StationaryArray1 = new ArrayList();
            ArrayList StationaryArray2 = new ArrayList();

            // Создаем экземпляр генератора стационарного случайного процесса
            StationaryGenerator stationaryGenerator1 = new StationaryGenerator(C2, 0, 7);
            StationaryGenerator stationaryGenerator2 = new StationaryGenerator(C2, 0, 7);

            for (int i = 0; i < 2000; i++)
            {
                if(i%2==0)
                    StationaryArray1.Add(stationaryGenerator1.GetNextValue());
                else
                    StationaryArray2.Add(stationaryGenerator2.GetNextValue());
            }

            // Создаем новый Excel-файл и добавляем лист для записи данных
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets.Add("Стационарный процесс");

            // Импорт
            sheet.Cells.ImportArrayList(StationaryArray1, 1, 0, true);
            sheet.Cells.ImportArrayList(StationaryArray2, 1, 1, true);

            // Автоматически подгоняем ширину колонок под содержимое
            sheet.AutoFitColumns();

            // Очищаем массив StationaryArray
            StationaryArray1.Clear();
            StationaryArray2.Clear();

            // Сохраняем Excel-файл с именем "StationaryProcess.xlsx"
            workbook.Save("Тест генератора.xlsx");
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

            // Заполняем список q случайными значениями с нулевым средним и единичной дисперсией
            for (int i = 0; i < C.Length; i++) { q.Add(generator.NormalFunction(1, 0)); } // генерируем начальные значения qi 
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
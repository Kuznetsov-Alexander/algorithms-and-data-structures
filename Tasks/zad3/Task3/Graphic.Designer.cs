namespace Task3
{
    partial class Graphic
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            zedGraphControl = new ZedGraph.ZedGraphControl();
            comboBoxOfTests = new ComboBox();
            comboBoxOfSorts = new ComboBox();
            generate = new Button();
            run = new Button();
            save = new Button();
            SuspendLayout();

            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            ClientSize = new Size(1350, 720);
            Text = "Анализ алгоритмов сортировки";

            zedGraphControl.Location = new Point(400, 40);
            zedGraphControl.Size = new Size(900, 620);
            zedGraphControl.UseExtendedPrintDialog = true;

            var pane = zedGraphControl.GraphPane;
            pane.Title.Text = "Зависимость времени сортировки от размера массива";
            pane.Title.FontSpec.FontColor = Color.White;
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время (мс)";

            pane.Chart.Fill = new ZedGraph.Fill(Color.FromArgb(45, 45, 45));
            pane.Fill = new ZedGraph.Fill(Color.FromArgb(30, 30, 30));

            pane.XAxis.Color = Color.White;
            pane.YAxis.Color = Color.White;
            pane.XAxis.Title.FontSpec.FontColor = Color.White;
            pane.YAxis.Title.FontSpec.FontColor = Color.White;
            pane.XAxis.Scale.FontSpec.FontColor = Color.White;
            pane.YAxis.Scale.FontSpec.FontColor = Color.White;

            comboBoxOfSorts.Location = new Point(40, 40);
            comboBoxOfSorts.Size = new Size(300, 28);
            comboBoxOfSorts.Items.AddRange(new object[]
            {
                "Первая группа",
                "Вторая группа",
                "Третья группа"
            });
            comboBoxOfSorts.Text = "Выберите группу сортировок";
            comboBoxOfSorts.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfSorts.IntegralHeight = false;
            comboBoxOfSorts.DropDownHeight = 120;
            comboBoxOfSorts.BackColor = Color.FromArgb(45, 45, 45);
            comboBoxOfSorts.ForeColor = Color.White;

            comboBoxOfTests.Location = new Point(40, 100);
            comboBoxOfTests.Size = new Size(300, 28);
            comboBoxOfTests.Items.AddRange(new object[]
            {
                "Случайные числа",
                "Разбитые на подмассивы",
                "Отсортированные массивы",
                "Смешанные массивы"
            });
            comboBoxOfTests.Text = "Выберите тестовые данные";
            comboBoxOfTests.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfTests.IntegralHeight = false;
            comboBoxOfTests.DropDownHeight = 120;
            comboBoxOfTests.BackColor = Color.FromArgb(45, 45, 45);
            comboBoxOfTests.ForeColor = Color.White;
            comboBoxOfTests.SelectedIndexChanged += comboBoxOfTests_SelectedIndexChanged;

            generate.Location = new Point(40, 500);
            generate.Size = new Size(300, 35);
            generate.Text = "Сгенерировать массивы";
            generate.BackColor = Color.FromArgb(70, 70, 70);
            generate.ForeColor = Color.White;
            generate.FlatStyle = FlatStyle.Flat;
            generate.Click += generate_Click;

            run.Location = new Point(40, 555);
            run.Size = new Size(300, 35);
            run.Text = "Запустить тесты";
            run.BackColor = Color.FromArgb(90, 90, 90);
            run.ForeColor = Color.White;
            run.FlatStyle = FlatStyle.Flat;
            run.Click += run_Click;

            save.Location = new Point(40, 610);
            save.Size = new Size(300, 35);
            save.Text = "Сохранить результаты";
            save.BackColor = Color.FromArgb(60, 60, 60);
            save.ForeColor = Color.White;
            save.FlatStyle = FlatStyle.Flat;
            save.Click += save_Click;

            Controls.Add(zedGraphControl);
            Controls.Add(comboBoxOfSorts);
            Controls.Add(comboBoxOfTests);
            Controls.Add(generate);
            Controls.Add(run);
            Controls.Add(save);

            ResumeLayout(false);
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
        private ComboBox comboBoxOfTests;
        private ComboBox comboBoxOfSorts;
        private Button generate;
        private Button run;
        private Button save;
    }
}

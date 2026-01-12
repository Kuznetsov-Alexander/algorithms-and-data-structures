using ZedGraph;

namespace Task4
{
    partial class Graphics
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
            zedGraphControl = new ZedGraphControl();
            comboBoxOfTests = new ComboBox();
            comboBoxOfSorts = new ComboBox();
            comboBoxOfTypes = new ComboBox();
            generate = new Button();
            run = new Button();
            save = new Button();
            SuspendLayout();

            // ================= FORM =================
            BackColor = Color.FromArgb(32, 32, 32);
            ForeColor = Color.White;
            ClientSize = new Size(1400, 720);
            Text = "Анализ эффективности сортировок";

            // ================= ZEDGRAPH =================
            zedGraphControl.Location = new Point(420, 35);
            zedGraphControl.Margin = new Padding(4, 5, 4, 5);
            zedGraphControl.Name = "zedGraphControl";
            zedGraphControl.Size = new Size(900, 560);
            zedGraphControl.UseExtendedPrintDialog = true;

            // ❗ график НЕ трогаем специально

            // ================= COMBOBOX SORTS =================
            comboBoxOfSorts.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfSorts.Font = new Font("Segoe UI", 12F);
            comboBoxOfSorts.Items.AddRange(new object[]
            {
                "Первая группа сортировок",
                "Вторая группа сортировок",
                "Третья группа сортировок"
            });
            comboBoxOfSorts.Location = new Point(40, 40);
            comboBoxOfSorts.Size = new Size(350, 36);
            comboBoxOfSorts.BackColor = Color.FromArgb(45, 45, 45);
            comboBoxOfSorts.ForeColor = Color.White;
            comboBoxOfSorts.FlatStyle = FlatStyle.Flat;

            // ================= COMBOBOX TESTS =================
            comboBoxOfTests.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfTests.Font = new Font("Segoe UI", 12F);
            comboBoxOfTests.Items.AddRange(new object[]
            {
                "Случайные числа",
                "Разбитые на подмассивы",
                "Отсортированные массивы",
                "Смешанные массивы"
            });
            comboBoxOfTests.Location = new Point(40, 100);
            comboBoxOfTests.Size = new Size(350, 36);
            comboBoxOfTests.BackColor = Color.FromArgb(45, 45, 45);
            comboBoxOfTests.ForeColor = Color.White;
            comboBoxOfTests.FlatStyle = FlatStyle.Flat;
            comboBoxOfTests.SelectedIndexChanged += comboBoxOfTests_SelectedIndexChanged;

            // ================= COMBOBOX TYPES =================
            comboBoxOfTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfTypes.Font = new Font("Segoe UI", 12F);
            comboBoxOfTypes.Items.AddRange(new object[]
            {
                "Целые числа",
                "Вещественные числа",
                "Строки",
                "Даты"
            });
            comboBoxOfTypes.SelectedItem = "Целые числа";
            comboBoxOfTypes.Location = new Point(40, 160);
            comboBoxOfTypes.Size = new Size(350, 36);
            comboBoxOfTypes.BackColor = Color.FromArgb(45, 45, 45);
            comboBoxOfTypes.ForeColor = Color.White;
            comboBoxOfTypes.FlatStyle = FlatStyle.Flat;

            // ================= BUTTON GENERATE =================
            generate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            generate.Location = new Point(40, 480);
            generate.Size = new Size(350, 60);
            generate.Text = "Сгенерировать массивы";
            generate.BackColor = Color.FromArgb(70, 130, 180); // SteelBlue
            generate.ForeColor = Color.White;
            generate.FlatStyle = FlatStyle.Flat;
            generate.FlatAppearance.BorderSize = 0;
            generate.Click += generate_Click;

            // ================= BUTTON RUN =================
            run.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            run.Location = new Point(40, 550);
            run.Size = new Size(350, 60);
            run.Text = "Запустить тесты";
            run.BackColor = Color.FromArgb(60, 179, 113); // MediumSeaGreen
            run.ForeColor = Color.White;
            run.FlatStyle = FlatStyle.Flat;
            run.FlatAppearance.BorderSize = 0;
            run.Click += run_Click;

            // ================= BUTTON SAVE =================
            save.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            save.Location = new Point(40, 620);
            save.Size = new Size(350, 60);
            save.Text = "Сохранить результаты";
            save.BackColor = Color.FromArgb(205, 92, 92); // IndianRed
            save.ForeColor = Color.White;
            save.FlatStyle = FlatStyle.Flat;
            save.FlatAppearance.BorderSize = 0;
            save.Click += save_Click;

            // ================= ADD CONTROLS =================
            Controls.Add(zedGraphControl);
            Controls.Add(comboBoxOfSorts);
            Controls.Add(comboBoxOfTests);
            Controls.Add(comboBoxOfTypes);
            Controls.Add(generate);
            Controls.Add(run);
            Controls.Add(save);

            ResumeLayout(false);
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
        private ComboBox comboBoxOfTests;
        private ComboBox comboBoxOfSorts;
        private ComboBox comboBoxOfTypes;
        private Button generate;
        private Button run;
        private Button save;
    }
}

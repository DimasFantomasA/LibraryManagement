
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace LibraryManagement
{
    public partial class PopularBooksReportForm : Form
    {
        private DataTable? reportTable;

        public PopularBooksReportForm()
        {
            InitializeComponent();

            Text = "Отчёт — Самые популярные книги";

            dataGridViewReport.ReadOnly = true;
            dataGridViewReport.AllowUserToAddRows = false;
            dataGridViewReport.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;


            GenerateReport();
        }

        private void GenerateReport()
        {
            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                string sql = @"
                    SELECT
                        ROW_NUMBER() OVER (
                            ORDER BY COUNT(j.id) DESC, b.name
                        ) AS ""Место"",
                        b.name AS ""Название книги"",
                        bt.type AS ""Тип книги"",
                        COUNT(j.id) AS ""Количество выдач""
                    FROM books b
                    JOIN book_types bt ON b.type_id = bt.id
                    LEFT JOIN journal j ON j.book_id = b.id
                    GROUP BY b.id, b.name, bt.type
                    ORDER BY ""Количество выдач"" DESC, b.name;
                ";

                using var command = new NpgsqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                reportTable = new DataTable();
                reportTable.Load(reader);

                dataGridViewReport.DataSource = reportTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка формирования отчёта:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonGenerate_Click(object? sender, EventArgs e)
        {
            GenerateReport();
        }


        private void buttonExport_Click(object? sender, EventArgs e)
        {
            if (reportTable == null || reportTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            using var dialog = new SaveFileDialog();

            dialog.Filter = "Текстовый файл (*.txt)|*.txt";
            dialog.FileName = "popular_books.txt";
            dialog.Title = "Сохранить отчёт";

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                using var writer = new StreamWriter(
                    dialog.FileName,
                    false,
                    new UTF8Encoding(true));

                writer.WriteLine("ОТЧЁТ: САМЫЕ ПОПУЛЯРНЫЕ КНИГИ");
                writer.WriteLine("Дата формирования: " +
                                 DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                writer.WriteLine(new string('=', 70));

                foreach (DataRow row in reportTable.Rows)
                {
                    writer.WriteLine(
                        $"Место: {row["Место"]} | " +
                        $"Книга: {row["Название книги"]} | " +
                        $"Тип: {row["Тип книги"]} | " +
                        $"Выдач: {row["Количество выдач"]}");
                }

                writer.WriteLine(new string('=', 70));
                writer.WriteLine(
                    $"Всего книг в отчёте: {reportTable.Rows.Count}");

                MessageBox.Show("Отчёт успешно сохранён в TXT!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка экспорта:\n" + ex.Message);
            }
        }

        private void buttonClose_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}

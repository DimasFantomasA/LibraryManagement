
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace LibraryManagement
{
    public partial class FinesReportForm : Form
    {
        private DataTable? reportTable;

        public FinesReportForm()
        {
            InitializeComponent();

            Text = "Отчёт — Штрафы за просрочку";

            dataGridViewReport.ReadOnly = true;
            dataGridViewReport.AllowUserToAddRows = false;
            dataGridViewReport.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;
            dataGridViewReport.ScrollBars = ScrollBars.Both;

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
                        j.id AS ""ID"",
                        CONCAT_WS(' ',
                            c.last_name,
                            c.first_name,
                            c.father_name
                        ) AS ""Читатель"",
                        b.name AS ""Книга"",
                        j.date_end AS ""Вернуть до"",
                        j.date_ret AS ""Дата возврата"",
                        GREATEST(
                            COALESCE(j.date_ret, CURRENT_DATE)
                            - j.date_end,
                            0
                        ) AS ""Дней просрочки"",
                        bt.fine AS ""Штраф за день"",
                        GREATEST(
                            COALESCE(j.date_ret, CURRENT_DATE)
                            - j.date_end,
                            0
                        ) * bt.fine AS ""Общий штраф""
                    FROM journal j
                    JOIN clients c ON j.client_id = c.id
                    JOIN books b ON j.book_id = b.id
                    JOIN book_types bt ON b.type_id = bt.id
                    WHERE COALESCE(j.date_ret, CURRENT_DATE)
                          > j.date_end
                    ORDER BY ""Общий штраф"" DESC, j.id;
                ";

                using var command = new NpgsqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                reportTable = new DataTable();
                reportTable.Load(reader);

                dataGridViewReport.DataSource = reportTable;

                dataGridViewReport.Columns["ID"].Width = 55;
                dataGridViewReport.Columns["Читатель"].Width = 260;
                dataGridViewReport.Columns["Книга"].Width = 170;
                dataGridViewReport.Columns["Вернуть до"].Width = 110;
                dataGridViewReport.Columns["Дата возврата"].Width = 115;
                dataGridViewReport.Columns["Дней просрочки"].Width = 130;
                dataGridViewReport.Columns["Штраф за день"].Width = 130;
                dataGridViewReport.Columns["Общий штраф"].Width = 130;
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
            dialog.FileName = "fines_report.txt";
            dialog.Title = "Сохранить отчёт";

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                using var writer = new StreamWriter(
                    dialog.FileName,
                    false,
                    new UTF8Encoding(true));

                writer.WriteLine("ОТЧЁТ: ШТРАФЫ ЗА ПРОСРОЧКУ");
                writer.WriteLine(
                    "Дата формирования: " +
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm"));

                writer.WriteLine(new string('=', 70));

                foreach (DataRow row in reportTable.Rows)
                {
                    foreach (DataColumn column in reportTable.Columns)
                    {
                        writer.WriteLine(
                            $"{column.ColumnName}: {row[column]}");
                    }

                    writer.WriteLine(new string('-', 70));
                }

                writer.WriteLine(
                    $"Всего записей: {reportTable.Rows.Count}");

                MessageBox.Show("Отчёт успешно сохранён в TXT!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка экспорта:\n" + ex.Message);
            }
        }


        private void buttonClose_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}

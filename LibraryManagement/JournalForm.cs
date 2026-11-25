using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace LibraryManagement
{
    public partial class JournalForm : Form
    {
        public JournalForm()
        {
            InitializeComponent();
            buttonDelete.Visible = UserSession.IsAdmin;
            LoadJournal();
        }

        private void LoadJournal()
        {
            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                string sql = @"
            SELECT
                j.id AS ""ID"",
                b.name AS ""Название книги"",
                CONCAT_WS(' ',
                    c.last_name,
                    c.first_name,
                    c.father_name
                ) AS ""Читатель"",
                j.date_beg AS ""Дата выдачи"",
                j.date_end AS ""Вернуть до"",
                j.date_ret AS ""Дата возврата"",
                CASE
                    WHEN j.date_ret IS NOT NULL
                        THEN 'Возвращена'
                    WHEN j.date_end < CURRENT_DATE
                        THEN 'Просрочена'
                    ELSE 'На руках'
                END AS ""Статус""
            FROM journal j
            JOIN books b ON j.book_id = b.id
            JOIN clients c ON j.client_id = c.id
            ORDER BY j.id DESC;
        ";

                using var command = new NpgsqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                dataGridViewJournal.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки журнала:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void dataGridViewJournal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonIssue_Click(object sender, EventArgs e)
        {
            using var form = new BookIssueForm();

            if (form.ShowDialog(this) == DialogResult.OK)
                LoadJournal();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (dataGridViewJournal.CurrentRow == null)
            {
                MessageBox.Show("Выберите запись журнала.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewJournal.CurrentRow.Cells["ID"].Value);

            try
            {
                using var form = new BookReturnForm(id);

                if (form.ShowDialog(this) == DialogResult.OK)
                    LoadJournal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Невозможно открыть возврат:\n" + ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show(
                    "Недостаточно прав для удаления записей.",
                    "Доступ запрещён",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            if (dataGridViewJournal.CurrentRow == null)
            {
                MessageBox.Show("Выберите запись журнала.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewJournal.CurrentRow.Cells["ID"].Value);

            var answer = MessageBox.Show(
                "Вы действительно хотите удалить запись из журнала?\n" +
                "Это действие нельзя отменить.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
                return;

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                using var command = new NpgsqlCommand(@"
            DELETE FROM journal
            WHERE id = @id
              AND date_ret IS NOT NULL;
        ", connection);

                command.Parameters.AddWithValue("id", id);

                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    MessageBox.Show(
                        "Нельзя удалить запись: книга ещё не возвращена " +
                        "или запись не найдена.");
                    return;
                }

                MessageBox.Show("Запись успешно удалена.");

                LoadJournal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка удаления записи:\n" + ex.Message);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

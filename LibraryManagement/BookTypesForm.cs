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
    public partial class BookTypesForm : Form
    {
        public BookTypesForm()
        {
            InitializeComponent();
            buttonDelete.Visible = UserSession.IsAdmin;
            LoadBookTypes();
        }


        private void LoadBookTypes()
        {
            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                string sql = @"
            SELECT
                id AS ""ID"",
                type AS ""Тип книги"",
                fine AS ""Штраф за день"",
                day_count AS ""Срок выдачи (дней)""
            FROM book_types
            ORDER BY id;
        ";

                using var command = new NpgsqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                dataGridViewTypes.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки типов книг:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using var form = new BookTypeEditForm();

            if (form.ShowDialog(this) == DialogResult.OK)
                LoadBookTypes();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewTypes.CurrentRow == null)
            {
                MessageBox.Show("Выберите тип книги.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewTypes.CurrentRow.Cells["ID"].Value);

            using var form = new BookTypeEditForm(id);

            if (form.ShowDialog(this) == DialogResult.OK)
                LoadBookTypes();
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
            if (dataGridViewTypes.CurrentRow == null)
            {
                MessageBox.Show("Выберите тип книги.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewTypes.CurrentRow.Cells["ID"].Value);

            var answer = MessageBox.Show(
                "Удалить выбранный тип книги?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
                return;

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                using var transaction = connection.BeginTransaction();

                using var check = new NpgsqlCommand(
                    "SELECT EXISTS(SELECT 1 FROM books WHERE type_id = @id)",
                    connection, transaction);

                check.Parameters.AddWithValue("id", id);

                bool hasBooks = (bool)check.ExecuteScalar()!;

                if (hasBooks)
                {
                    MessageBox.Show(
                        "Нельзя удалить тип: к нему привязаны книги.");

                    transaction.Rollback();
                    return;
                }

                using var command = new NpgsqlCommand(
                    "DELETE FROM book_types WHERE id = @id",
                    connection, transaction);

                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();

                transaction.Commit();

                LoadBookTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления:\n" + ex.Message);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

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
    public partial class BooksForm : Form
    {
        public BooksForm()
        {
            InitializeComponent();
            buttonDelete.Visible = UserSession.IsAdmin;
            LoadBooks();
        }
        private void LoadBooks()
        {
            try
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                SELECT
                    b.id AS ""ID"",
                    b.name AS ""Название книги"",
                    bt.type AS ""Тип книги"",
                    b.cnt AS ""Количество""
                FROM books b
                JOIN book_types bt ON b.type_id = bt.id
                ORDER BY b.id;
            ";

                    using (var command = new NpgsqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);

                        dataGridViewBooks.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки книг:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            if (dataGridViewBooks.CurrentRow == null)
            {
                MessageBox.Show("Выберите книгу.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewBooks.CurrentRow.Cells["ID"].Value);

            string name = Convert.ToString(
                dataGridViewBooks.CurrentRow.Cells["Название книги"].Value
            ) ?? "";

            var answer = MessageBox.Show(
                $"Удалить книгу «{name}»?",
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
                    @"SELECT
                EXISTS(SELECT 1 FROM journal WHERE book_id = @id)
                OR
                EXISTS(SELECT 1 FROM donations WHERE book_id = @id)",
                    connection, transaction);

                check.Parameters.AddWithValue("id", id);

                bool hasHistory = (bool)check.ExecuteScalar()!;

                if (hasHistory)
                {
                    MessageBox.Show(
                        "Нельзя удалить книгу: у неё есть история " +
                        "выдач или пожертвований.");

                    transaction.Rollback();
                    return;
                }

                using var command = new NpgsqlCommand(
                    "DELETE FROM books WHERE id = @id",
                    connection, transaction);

                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();

                transaction.Commit();

                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка удаления:\n" + ex.Message);
            }

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using var form = new BookEditForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadBooks();
            }

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.CurrentRow == null)
            {
                MessageBox.Show("Выберите книгу.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewBooks.CurrentRow.Cells["ID"].Value);

            using var form = new BookEditForm(id);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

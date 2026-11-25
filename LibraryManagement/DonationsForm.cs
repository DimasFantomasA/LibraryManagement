using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using Npgsql;

namespace LibraryManagement
{
    public partial class DonationsForm : Form
    {
        public DonationsForm()
        {
            InitializeComponent();
            buttonDelete.Visible = UserSession.IsAdmin;
            LoadDonations();
        }


        private void LoadDonations()
        {
            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                string sql = @"
            SELECT
                d.id AS ""ID"",
                CONCAT_WS(' ',
                    c.last_name,
                    c.first_name,
                    c.father_name
                ) AS ""Читатель"",
                b.name AS ""Название книги"",
                d.quantity AS ""Количество"",
                d.donation_date AS ""Дата пожертвования""
            FROM donations d
            JOIN clients c ON d.client_id = c.id
            JOIN books b ON d.book_id = b.id
            ORDER BY d.id DESC;
        ";

                using var command = new NpgsqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                dataGridViewDonations.DataSource = table;

                dataGridViewDonations.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.None;

                dataGridViewDonations.ScrollBars = ScrollBars.Both;

                dataGridViewDonations.Columns["ID"].Width = 60;
                dataGridViewDonations.Columns["Читатель"].Width = 280;
                dataGridViewDonations.Columns["Название книги"].Width = 200;
                dataGridViewDonations.Columns["Количество"].Width = 120;
                dataGridViewDonations.Columns["Дата пожертвования"].Width = 160;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки пожертвований:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using var form = new DonationEditForm();

            if (form.ShowDialog(this) == DialogResult.OK)
                LoadDonations();
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
            if (dataGridViewDonations.CurrentRow == null)
            {
                MessageBox.Show("Выберите пожертвование.");
                return;
            }

            int donationId = Convert.ToInt32(
                dataGridViewDonations.CurrentRow.Cells["ID"].Value);

            var answer = MessageBox.Show(
                "Удалить выбранное пожертвование?\n" +
                "Количество экземпляров книги будет уменьшено.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
                return;

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                using var transaction = connection.BeginTransaction();

                using var find = new NpgsqlCommand(@"
            SELECT book_id, quantity
            FROM donations
            WHERE id = @id;
        ", connection, transaction);

                find.Parameters.AddWithValue("id", donationId);

                int bookId;
                int quantity;

                using (var reader = find.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Пожертвование не найдено.");
                        transaction.Rollback();
                        return;
                    }

                    bookId = reader.GetInt32(0);
                    quantity = reader.GetInt32(1);
                }

                using var check = new NpgsqlCommand(@"
            SELECT
                b.cnt -
                (
                    SELECT COUNT(*)
                    FROM journal j
                    WHERE j.book_id = b.id
                      AND j.date_ret IS NULL
                )
            FROM books b
            WHERE b.id = @bookId
            FOR UPDATE OF b;
        ", connection, transaction);

                check.Parameters.AddWithValue("bookId", bookId);

                object? result = check.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    MessageBox.Show("Книга не найдена.");
                    transaction.Rollback();
                    return;
                }

                long available = Convert.ToInt64(result);

                if (available < quantity)
                {
                    MessageBox.Show(
                        "Нельзя удалить пожертвование.\n" +
                        "Часть экземпляров сейчас находится у читателей.");

                    transaction.Rollback();
                    return;
                }

                using var update = new NpgsqlCommand(@"
            UPDATE books
            SET cnt = cnt - @quantity
            WHERE id = @bookId;
        ", connection, transaction);

                update.Parameters.AddWithValue("quantity", quantity);
                update.Parameters.AddWithValue("bookId", bookId);
                update.ExecuteNonQuery();

                using var delete = new NpgsqlCommand(@"
            DELETE FROM donations
            WHERE id = @id;
        ", connection, transaction);

                delete.Parameters.AddWithValue("id", donationId);
                delete.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Пожертвование успешно удалено.");

                LoadDonations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка удаления пожертвования:\n" + ex.Message);
            }
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

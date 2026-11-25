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
    public partial class BookIssueForm : Form
    {
        public BookIssueForm()
        {
            InitializeComponent();

            comboBoxBook.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxClient.DropDownStyle = ComboBoxStyle.DropDownList;

            dateTimePickerIssue.Value = DateTime.Today;
            dateTimePickerDue.Enabled = false;

            LoadBooks();
            LoadClients();

            comboBoxBook.SelectedIndexChanged +=
                (sender, e) => CalculateDueDate();

            dateTimePickerIssue.ValueChanged +=
                (sender, e) => CalculateDueDate();

            CalculateDueDate();


        }

        private void LoadBooks()
        {
            using var connection = Database.GetConnection();
            connection.Open();

            string sql = @"
        SELECT
            b.id,
            b.name,
            bt.day_count
        FROM books b
        JOIN book_types bt ON b.type_id = bt.id
        WHERE b.cnt > 0
        ORDER BY b.name;
    ";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            comboBoxBook.DataSource = table;
            comboBoxBook.DisplayMember = "name";
            comboBoxBook.ValueMember = "id";
        }

        private void LoadClients()
        {
            using var connection = Database.GetConnection();
            connection.Open();

            string sql = @"
        SELECT
            id,
            CONCAT_WS(' ', last_name, first_name, father_name)
                AS full_name
        FROM clients
        ORDER BY last_name, first_name;
    ";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            comboBoxClient.DataSource = table;
            comboBoxClient.DisplayMember = "full_name";
            comboBoxClient.ValueMember = "id";
        }

        private void CalculateDueDate()
        {
            if (comboBoxBook.SelectedItem is DataRowView row)
            {
                int days = Convert.ToInt32(row["day_count"]);

                dateTimePickerDue.Value =
                    dateTimePickerIssue.Value.Date.AddDays(days);
            }
        }


        private void buttonSave_Click(object? sender, EventArgs e)
        {
            if (comboBoxBook.SelectedValue is not int bookId ||
                comboBoxClient.SelectedValue is not int clientId)
            {
                MessageBox.Show("Выберите книгу и читателя.");
                return;
            }

            DateTime issueDate = dateTimePickerIssue.Value.Date;
            DateTime dueDate = dateTimePickerDue.Value.Date;

            buttonSave.Enabled = false;

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                using var transaction = connection.BeginTransaction();

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

                if (available <= 0)
                {
                    MessageBox.Show(
                        "Нет свободных экземпляров выбранной книги.");

                    transaction.Rollback();
                    return;
                }

                using var command = new NpgsqlCommand(@"
            INSERT INTO journal
                (book_id, client_id, date_beg, date_end)
            VALUES
                (@bookId, @clientId, @issueDate, @dueDate);
        ", connection, transaction);

                command.Parameters.AddWithValue("bookId", bookId);
                command.Parameters.AddWithValue("clientId", clientId);
                command.Parameters.AddWithValue(
                    "issueDate", NpgsqlTypes.NpgsqlDbType.Date,
                    issueDate);
                command.Parameters.AddWithValue(
                    "dueDate", NpgsqlTypes.NpgsqlDbType.Date,
                    dueDate);

                command.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Книга успешно выдана!");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка выдачи книги:\n" + ex.Message);
            }
            finally
            {
                buttonSave.Enabled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBook_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerDue_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

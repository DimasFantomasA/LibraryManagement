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
using NpgsqlTypes;

namespace LibraryManagement
{
    public partial class BookReturnForm : Form
    {
        private readonly int journalId;
        private DateTime issueDate;

        public BookReturnForm(int id)
        {
            InitializeComponent();

            journalId = id;
            Text = "Возврат книги";

            dateTimePickerReturn.Value = DateTime.Today;

            LoadLoan();

        }

        private void LoadLoan()
        {
            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(@"
                SELECT
                    b.name,
                    CONCAT_WS(' ',
                        c.last_name,
                        c.first_name,
                        c.father_name),
                    j.date_beg,
                    j.date_ret
                FROM journal j
                JOIN books b ON j.book_id = b.id
                JOIN clients c ON j.client_id = c.id
                WHERE j.id = @id;
            ", connection);

            command.Parameters.AddWithValue("id", journalId);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                throw new Exception("Запись журнала не найдена.");

            if (!reader.IsDBNull(3))
                throw new Exception("Эта книга уже возвращена.");

            labelBook.Text = "Книга: " + reader.GetString(0);
            labelClient.Text = "Читатель: " + reader.GetString(1);

            issueDate = reader.GetDateTime(2).Date;

            dateTimePickerReturn.MinDate = issueDate;
        }

        private void dateTimePickerReturn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DateTime returnDate = dateTimePickerReturn.Value.Date;

            if (returnDate < issueDate)
            {
                MessageBox.Show(
                    "Дата возврата не может быть раньше даты выдачи.");
                return;
            }

            if (returnDate > DateTime.Today)
            {
                MessageBox.Show(
                    "Нельзя указать будущую дату возврата.");
                return;
            }

            buttonSave.Enabled = false;

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                using var command = new NpgsqlCommand(@"
                    UPDATE journal
                    SET date_ret = @returnDate
                    WHERE id = @id
                      AND date_ret IS NULL;
                ", connection);

                command.Parameters.AddWithValue("id", journalId);
                command.Parameters.AddWithValue(
                    "returnDate",
                    NpgsqlDbType.Date,
                    returnDate);

                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    MessageBox.Show(
                        "Запись не найдена или книга уже возвращена.");
                    return;
                }

                MessageBox.Show("Возврат книги зарегистрирован!");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка возврата книги:\n" + ex.Message);
            }
            finally
            {
                buttonSave.Enabled = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

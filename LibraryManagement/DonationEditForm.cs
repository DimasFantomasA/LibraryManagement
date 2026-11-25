using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace LibraryManagement
{
    public partial class DonationEditForm : Form
    {
        public DonationEditForm()
        {
            InitializeComponent();

            Text = "Добавление пожертвования";

            comboBoxClient.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBook.DropDownStyle = ComboBoxStyle.DropDownList;

            numericUpDownQuantity.Minimum = 1;
            numericUpDownQuantity.Maximum = 5;

            dateTimePickerDonation.Value = DateTime.Today;
            dateTimePickerDonation.MaxDate = DateTime.Today;

            LoadClients();
            LoadBooks();
        }

        private void LoadClients()
        {
            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(@"
                SELECT
                    id,
                    CONCAT_WS(' ', last_name, first_name, father_name)
                        AS full_name
                FROM clients
                ORDER BY last_name, first_name;
            ", connection);

            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            comboBoxClient.DisplayMember = "full_name";
            comboBoxClient.ValueMember = "id";
            comboBoxClient.DataSource = table;
        }

        private void LoadBooks()
        {
            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(@"
                SELECT id, name
                FROM books
                ORDER BY name;
            ", connection);

            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            comboBoxBook.DisplayMember = "name";
            comboBoxBook.ValueMember = "id";
            comboBoxBook.DataSource = table;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxClient.SelectedValue is not int clientId ||
                comboBoxBook.SelectedValue is not int bookId)
            {
                MessageBox.Show("Выберите читателя и книгу.");
                return;
            }

            int quantity = (int)numericUpDownQuantity.Value;
            DateTime donationDate = dateTimePickerDonation.Value.Date;

            buttonSave.Enabled = false;

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                using var command = new NpgsqlCommand(@"
                    INSERT INTO donations
                        (client_id, book_id, quantity, donation_date)
                    VALUES
                        (@clientId, @bookId, @quantity, @donationDate);
                ", connection);

                command.Parameters.AddWithValue("clientId", clientId);
                command.Parameters.AddWithValue("bookId", bookId);
                command.Parameters.AddWithValue("quantity", quantity);
                command.Parameters.AddWithValue(
                    "donationDate",
                    NpgsqlDbType.Date,
                    donationDate);

                command.ExecuteNonQuery();

                MessageBox.Show("Пожертвование успешно добавлено!");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(
                    "PostgreSQL отклонил пожертвование:\n" +
                    ex.MessageText,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка добавления пожертвования:\n" +
                    ex.Message);
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

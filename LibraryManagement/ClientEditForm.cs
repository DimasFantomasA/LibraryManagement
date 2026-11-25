
using System;
using System.Windows.Forms;
using Npgsql;

namespace LibraryManagement
{
    public partial class ClientEditForm : Form
    {
        private readonly int? clientId;

        public ClientEditForm(int? id = null)
        {
            InitializeComponent();

            clientId = id;

            Text = id == null
                ? "Добавление читателя"
                : "Редактирование читателя";

            if (id != null)
                LoadClient(id.Value);

        }

        private void LoadClient(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(
                @"SELECT first_name, last_name, father_name,
                         passport_seria, passport_number
                  FROM clients
                  WHERE id = @id",
                connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                throw new Exception("Читатель не найден.");

            textBoxFirstName.Text = reader.GetString(0);
            textBoxLastName.Text = reader.GetString(1);
            textBoxFatherName.Text = reader.IsDBNull(2)
                ? "" : reader.GetString(2);
            textBoxPassportSeries.Text = reader.GetString(3);
            textBoxPassportNumber.Text = reader.GetString(4);
        }

        private void buttonSave_Click(object? sender, EventArgs e)
        {
            string firstName = textBoxFirstName.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string fatherName = textBoxFatherName.Text.Trim();
            string series = textBoxPassportSeries.Text.Trim();
            string number = textBoxPassportNumber.Text.Trim();

            if (firstName.Length == 0 || lastName.Length == 0)
            {
                MessageBox.Show("Введите имя и фамилию.");
                return;
            }

            if (series.Length != 4 || number.Length != 6 ||
                !series.All(char.IsDigit) ||
                !number.All(char.IsDigit))
            {
                MessageBox.Show(
                    "Серия паспорта должна содержать 4 цифры, " +
                    "а номер — 6 цифр.");
                return;
            }

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                string sql = clientId == null
                    ? @"INSERT INTO clients
                        (first_name, last_name, father_name,
                         passport_seria, passport_number)
                        VALUES
                        (@first, @last, @father, @series, @number)"
                    : @"UPDATE clients
                        SET first_name = @first,
                            last_name = @last,
                            father_name = @father,
                            passport_seria = @series,
                            passport_number = @number
                        WHERE id = @id";

                using var command =
                    new NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("first", firstName);
                command.Parameters.AddWithValue("last", lastName);
                command.Parameters.AddWithValue(
                    "father", (object?)fatherName == "" ?
                    DBNull.Value : fatherName);
                command.Parameters.AddWithValue("series", series);
                command.Parameters.AddWithValue("number", number);

                if (clientId != null)
                    command.Parameters.AddWithValue(
                        "id", clientId.Value);

                command.ExecuteNonQuery();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (PostgresException ex) when (ex.SqlState == "P0001" ||
                                              ex.SqlState == "23505")
            {
                MessageBox.Show(
                    "Не удалось сохранить читателя. " +
                    "Возможно, такой паспорт уже зарегистрирован.\n" +
                    ex.MessageText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка сохранения:\n" + ex.Message);
            }
        }

        private void buttonCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFatherName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassportSeries_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassportNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

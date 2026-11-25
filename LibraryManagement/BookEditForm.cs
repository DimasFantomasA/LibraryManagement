using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace LibraryManagement
{
    public partial class BookEditForm : Form
    {
        private readonly int? bookId;

        public BookEditForm(int? id = null)
        {
            InitializeComponent();

            bookId = id;

            Text = id == null
                ? "Добавление книги"
                : "Редактирование книги";

            LoadBookTypes();

            if (id != null)
                LoadBook(id.Value);

        }

        private void LoadBookTypes()
        {
            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(
                "SELECT id, type FROM book_types ORDER BY id",
                connection);

            using var reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            comboBoxType.DataSource = table;
            comboBoxType.DisplayMember = "type";
            comboBoxType.ValueMember = "id";
        }

        private void LoadBook(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(
                "SELECT name, cnt, type_id FROM books WHERE id = @id",
                connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                throw new Exception("Книга не найдена.");
            }

            textBoxName.Text = reader.GetString(0);
            numericUpDownCount.Value = reader.GetInt32(1);
            comboBoxType.SelectedValue = reader.GetInt32(2);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите название книги.");
                return;
            }

            if (name.Length > 20)
            {
                MessageBox.Show(
                    "Название не должно превышать 20 символов.");
                return;
            }

            if (comboBoxType.SelectedValue is not int typeId)
            {
                MessageBox.Show("Выберите тип книги.");
                return;
            }

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                string sql = bookId == null
                    ? @"INSERT INTO books (name, cnt, type_id)
                        VALUES (@name, @cnt, @type)"
                    : @"UPDATE books
                        SET name = @name,
                            cnt = @cnt,
                            type_id = @type
                        WHERE id = @id";

                using var command =
                    new NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue(
                    "cnt", (int)numericUpDownCount.Value);
                command.Parameters.AddWithValue("type", typeId);

                if (bookId != null)
                    command.Parameters.AddWithValue(
                        "id", bookId.Value);

                command.ExecuteNonQuery();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка сохранения:\n" + ex.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

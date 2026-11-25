using Npgsql.Internal.Postgres;
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
    public partial class BookTypeEditForm : Form
    {
        private readonly int? typeId;
        public BookTypeEditForm(int? id = null)
        {
            InitializeComponent();

            typeId = id;

            Text = id == null
                ? "Добавление типа книги"
                : "Редактирование типа книги";

            if (id != null)
                LoadBookType(id.Value);

            this.typeId = typeId;
        }

        private void LoadBookType(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(
                @"SELECT type, fine, day_count
                  FROM book_types
                  WHERE id = @id",
                connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                throw new Exception("Тип книги не найден.");

            textBoxType.Text = reader.GetString(0);
            numericUpDownFine.Value = reader.GetInt32(1);
            numericUpDownDays.Value = reader.GetInt32(2);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string typeName = textBoxType.Text.Trim();

            if (string.IsNullOrWhiteSpace(typeName))
            {
                MessageBox.Show("Введите название типа книги.");
                return;
            }

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                string sql = typeId == null
                    ? @"INSERT INTO book_types (type, fine, day_count)
                        VALUES (@type, @fine, @days)"
                    : @"UPDATE book_types
                        SET type = @type,
                            fine = @fine,
                            day_count = @days
                        WHERE id = @id";

                using var command = new NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("type", typeName);
                command.Parameters.AddWithValue(
                    "fine", (int)numericUpDownFine.Value);
                command.Parameters.AddWithValue(
                    "days", (int)numericUpDownDays.Value);

                if (typeId != null)
                    command.Parameters.AddWithValue("id", typeId.Value);

                command.ExecuteNonQuery();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                MessageBox.Show(
                    "Тип книги с таким названием уже существует.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения:\n" + ex.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void numericUpDownFine_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBoxType_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

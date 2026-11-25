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
    public partial class ClientsForm : Form
    {
        public ClientsForm()
        {
            InitializeComponent();
            buttonDelete.Visible = UserSession.IsAdmin;
            this.Load += ClientsForm_Load;
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            LoadClients();
        }
        private void LoadClients()
        {
            try
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                SELECT
                    id AS ""ID"",
                    first_name AS ""Имя"",
                    last_name AS ""Фамилия"",
                    father_name AS ""Отчество"",
                    passport_seria AS ""Серия паспорта"",
                    passport_number AS ""Номер паспорта""
                FROM clients
                ORDER BY id;
            ";

                    using (var command = new NpgsqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);

                        dataGridViewClients.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки читателей:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
            if (dataGridViewClients.CurrentRow == null)
            {
                MessageBox.Show("Выберите читателя.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewClients.CurrentRow.Cells["ID"].Value);

            var answer = MessageBox.Show(
                "Удалить выбранного читателя?",
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
                EXISTS(SELECT 1 FROM journal WHERE client_id = @id)
                OR
                EXISTS(SELECT 1 FROM donations WHERE client_id = @id)",
                    connection, transaction);

                check.Parameters.AddWithValue("id", id);

                bool hasHistory = (bool)check.ExecuteScalar()!;

                if (hasHistory)
                {
                    MessageBox.Show(
                        "Нельзя удалить читателя: у него есть история " +
                        "выдач или пожертвований.");

                    transaction.Rollback();
                    return;
                }

                using var command = new NpgsqlCommand(
                    "DELETE FROM clients WHERE id = @id",
                    connection, transaction);

                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();

                transaction.Commit();

                LoadClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления:\n" + ex.Message);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.CurrentRow == null)
            {
                MessageBox.Show("Выберите читателя.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridViewClients.CurrentRow.Cells["ID"].Value);

            using var form = new ClientEditForm(id);

            if (form.ShowDialog(this) == DialogResult.OK)
                LoadClients();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using var form = new ClientEditForm();

            if (form.ShowDialog(this) == DialogResult.OK)
                LoadClients();
        }
    }
}

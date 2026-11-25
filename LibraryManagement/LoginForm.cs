
using System;
using System.Windows.Forms;
using Npgsql;

namespace LibraryManagement
{
    public partial class LoginForm : Form
    {
        public string UserRole { get; private set; } = "";
        public string Username { get; private set; } = "";

        public LoginForm()
        {
            InitializeComponent();

            Text = "Авторизация";

            textBoxPassword.UseSystemPasswordChar = true;

            AcceptButton = buttonLogin;
            CancelButton = buttonCancel;

        }

        private void buttonLogin_Click(object? sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            buttonLogin.Enabled = false;

            try
            {
                using var connection = Database.GetConnection();
                connection.Open();

                using var command = new NpgsqlCommand(@"
                    SELECT
                        username,
                        password_hash,
                        password_salt,
                        password_iterations,
                        role
                    FROM app_users
                    WHERE username = @username;
                ", connection);

                command.Parameters.AddWithValue("username", username);

                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }

                string storedUsername = reader.GetString(0);
                byte[] storedHash = (byte[])reader.GetValue(1);
                byte[] salt = (byte[])reader.GetValue(2);
                int iterations = reader.GetInt32(3);
                string role = reader.GetString(4);

                bool valid = PasswordHelper.VerifyPassword(
                    password,
                    salt,
                    storedHash,
                    iterations);

                if (!valid)
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }

                Username = storedUsername;
                UserRole = role;

                UserSession.Username = storedUsername;
                UserSession.Role = role;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка авторизации:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                buttonLogin.Enabled = true;
            }
        }

        private void buttonCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

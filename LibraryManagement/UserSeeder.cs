
using System;
using System.Security.Cryptography;
using Npgsql;
using NpgsqlTypes;

namespace LibraryManagement
{
    internal static class UserSeeder
    {
        public static void CreateUser(
            string username,
            string password,
            string role)
        {
            byte[] salt = PasswordHelper.GenerateSalt();

            byte[] hash = PasswordHelper.HashPassword(
                password,
                salt);

            using var connection = Database.GetConnection();
            connection.Open();

            using var command = new NpgsqlCommand(@"
                INSERT INTO app_users
                    (username, password_hash, password_salt,
                     password_iterations, role)
                VALUES
                    (@username, @hash, @salt, @iterations, @role)
                ON CONFLICT (username) DO NOTHING;
            ", connection);

            command.Parameters.AddWithValue(
                "username", username);

            command.Parameters.AddWithValue(
                "hash", NpgsqlDbType.Bytea, hash);

            command.Parameters.AddWithValue(
                "salt", NpgsqlDbType.Bytea, salt);

            command.Parameters.AddWithValue(
                "iterations", PasswordHelper.Iterations);

            command.Parameters.AddWithValue(
                "role", role);

            command.ExecuteNonQuery();
        }
    }
}

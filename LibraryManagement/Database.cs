using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class Database
    {
        public static NpgsqlConnection GetConnection()
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, "database.ini");
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("Файл не найден");
            }
            var builder = new NpgsqlConnectionStringBuilder();

            foreach (string line in File.ReadAllLines(configPath))
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine) || 
                    trimmedLine.StartsWith("#") ||
                    trimmedLine.StartsWith(";"))
                {
                    continue;
                }

                string[] parts = trimmedLine.Split('=', 2);

                if (parts.Length == 2)
                {
                    builder[parts[0].Trim()] = parts[1].Trim();
                }
            }
            return new NpgsqlConnection(builder.ConnectionString);
        }
    }
}

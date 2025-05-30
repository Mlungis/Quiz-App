using Microsoft.Data.Sqlite;
using Microsoft.Maui.Storage;
using System.IO;
using System.Threading.Tasks;

namespace The_Quiz.Users
{
    public class UserDatabase
    {
        private readonly string dbPath;

        public UserDatabase()
        {
            var dataFolder = Path.Combine(FileSystem.AppDataDirectory, "Data");
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            dbPath = Path.Combine(dataFolder, "users.db");

            InitializeDatabaseAsync().Wait();
        }

        public string GetDbPath() => dbPath;

        private async Task InitializeDatabaseAsync()
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            await connection.OpenAsync();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    Email TEXT NOT NULL,
                    Password TEXT NOT NULL
                );";
            await tableCmd.ExecuteNonQueryAsync();
        }

        public async Task AddUserAsync(string username, string email, string password)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            await connection.OpenAsync();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = @"
                INSERT INTO Users (Username, Email, Password) 
                VALUES ($username, $email, $password);";
            insertCmd.Parameters.AddWithValue("$username", username);
            insertCmd.Parameters.AddWithValue("$email", email);
            insertCmd.Parameters.AddWithValue("$password", password);

            await insertCmd.ExecuteNonQueryAsync();
        }
    }
}

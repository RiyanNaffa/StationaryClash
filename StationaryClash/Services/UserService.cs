using StationaryClash.Models;
using Microsoft.Data.SqlClient;
using StationaryClash.Interfaces.Services;

namespace StationaryClash.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\CSharp\\StationaryClash\\StationaryClash.mdf;Integrated Security=True;Connect Timeout=30";

        public async Task<User?> AuthAsync(string username, string password)
        {
            User? user = null;

            // Query SQL untuk mencari user berdasarkan username dan password
            var query = "SELECT id, username, password, description, token, created FROM [Account] WHERE username = @Username AND password = @Password";

            // Menggunakan SqlConnection dan SqlCommand
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    // Tambahkan parameter untuk menghindari SQL Injection
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    // Eksekusi queryInsert
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // Cek jika data ditemukan
                        {
                            user = new User
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString(reader.GetOrdinal("description")),
                                Token = reader.GetInt32(reader.GetOrdinal("token")),
                                Created = reader.GetDateTime(reader.GetOrdinal("created"))
                            };
                        }
                    }
                }
            }

            return user;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var valQuery = "SELECT COUNT(*) FROM [Account] WHERE Username = @Username";
            var insertQuery = @"
                INSERT INTO [Account] (username, password, token, created)
                VALUES (@Username, @Password, @Token, @CreatedAt)";

            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Cek apakah username sudah dipakai
                using (SqlCommand checkCommand = new(valQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Username", username);
                    var result = await checkCommand.ExecuteScalarAsync();
                    int userCount = result is not null ? Convert.ToInt32(result) : 0;

                    if (userCount > 0)
                    {
                        return false;
                    }
                }
                // Insert akun baru ke table
                using(SqlCommand insertCommand = new(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@Password", password);
                    // Starting pack: 100 token pemula
                    insertCommand.Parameters.AddWithValue("@Token", 100);
                    insertCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    int rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");

                    return (rowsAffected > 0);
                }
            }
        }
    }
}

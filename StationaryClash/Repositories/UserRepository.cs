using StationaryClash.Interfaces.Repositories;
using StationaryClash.Models;
using Microsoft.Data.SqlClient;

namespace StationaryClash.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\CSharp\\StationaryClash\\StationaryClash.mdf;Integrated Security=True;Connect Timeout=30";

        public async Task<User?> GetUserByID(int id)
        {
            User? user = null;

            var query = "SELECT id, username, password, description, token, created FROM [Account] WHERE id = @ID";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // Cek jika data ditemukan
                        {
                            user = new User
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Token = reader.GetInt32(reader.GetOrdinal("token")),
                                Created = reader.GetDateTime(reader.GetOrdinal("created"))
                            };
                        }
                    }
                }
            }
            return user;
        }

        public async Task<User?> GetUserbyUsername(string username)
        {
            User? user = null;

            var query = "SELECT id, username, password, description, token, created FROM [Account] WHERE username = @Username";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // Cek jika data ditemukan
                        {
                            user = new User
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("id")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Token = reader.GetInt32(reader.GetOrdinal("token")),
                                Created = reader.GetDateTime(reader.GetOrdinal("created"))
                            };
                        }
                    }
                }
            }
            return user;
        }

        public async Task<int> GetUserTokenAsync(int id)
        {
            int userToken = 0;

            // Query SQL untuk mencari user berdasarkan Id
            var query = "SELECT id, token FROM [Account] WHERE id = @UserID";

            // Menggunakan SqlConnection dan SqlCommand
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    // Tambahkan parameter untuk menghindari SQL Injection
                    command.Parameters.AddWithValue("@UserID", id);

                    // Eksekusi query
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // Cek jika data ditemukan
                        {
                            userToken = reader.GetInt32(reader.GetOrdinal("token"));
                        }
                    }
                }
            }

            return userToken;
        }

        public async Task DecrementUserToken(int id)
        {
            var query = "UPDATE [Account] SET token = token - 1 WHERE id = @UserID";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task UpdateDescription(int id, string description)
        {
            var query = "UPDATE [Account] SET description = @Description WHERE id = @UserID";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", id);
                    command.Parameters.AddWithValue("@Description", description);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

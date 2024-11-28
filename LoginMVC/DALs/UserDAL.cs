using LoginMVC.Models;
using LoginMVC.Tools;
using Microsoft.Data.SqlClient;

namespace LoginMVC.DALs
{
    public class UserDAL
    {
        private readonly string _connectionString =
            "Data Source=85.208.21.117,54321;" +
            "Initial Catalog=DavidSanzLoginMVC;" +
            "User ID=sa;" +
            "Password=Sql#123456789;" +
            "TrustServerCertificate=True";
        public User GetUserLogin(string username, string pwd)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"SELECT * FROM Usuario
                                                WHERE UserName = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        var passwordHash = (byte[])reader["PasswordHash"];
                        var passwordSalt = (byte[])reader["PasswordSalt"];

                        if (PasswordHelper.VerifyPasswordHash(pwd, passwordHash, passwordSalt))
                        {
                            return new User
                            {
                                IdUsuario = (int)reader["IdUsuario"],
                                Username = (string)reader["UserName"],
                                PasswordHash = passwordHash,
                                PasswordSalt = passwordSalt,
                                Apellido = reader["Apellido"] != DBNull.Value ? (string)reader["Apellido"] : null,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? (DateTime?)reader["FechaNacimiento"] : null,
                                Telefono = reader["Telefono"] != DBNull.Value ? (string)reader["Telefono"] : null,
                                Direccion = reader["Direccion"] != DBNull.Value ? (string)reader["Direccion"] : null,
                                Ciudad = reader["Ciudad"] != DBNull.Value ? (string)reader["Ciudad"] : null,
                                Estado = reader["Estado"] != DBNull.Value ? (string)reader["Estado"] : null,
                                CodigoPostal = reader["CodigoPostal"] != DBNull.Value ? (string)reader["CodigoPostal"] : null,
                                FechaRegistro = reader["FechaRegistro"] != DBNull.Value ? (DateTime)reader["FechaRegistro"] : default,
                                Activo = reader["Activo"] != DBNull.Value ? (bool)reader["Activo"] : false
                            };
                        }
                    }
                    return null;
                }
            }
        }

        internal void CreateUser(User user, string password)
        {
            PasswordHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    INSERT INTO Usuario (UserName, PasswordHash, PasswordSalt)
                    VALUES (@Username,@PasswordHash, @PasswordSalt)", connection);

                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@PasswordSalt", passwordSalt);

                connection.Open();
                
                command.ExecuteNonQuery();
            }
        }
    }
}

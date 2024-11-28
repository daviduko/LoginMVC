using LoginMVC.Models;
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
                                                WHERE UserName = @Username AND Pword = @Pwd", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Pwd", pwd);

                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        return new User
                        {
                            IdUsuario = (int)reader["IdUsuario"],
                            Username = (string)reader["UserName"],
                            Pword = (string)reader["Pword"],
                            Apellido = (string)reader["Apellido"],
                            Email = (string)reader["Email"],
                            FechaNacimiento = (DateTime?)reader["FechaNacimiento"],
                            Telefono = (string)reader["Telefono"],
                            Direccion = (string)reader["Direccion"],
                            Ciudad = (string)reader["Ciudad"],
                            Estado = (string)reader["Estado"],
                            CodigoPostal = (string)reader["CodigoPostal"],
                            FechaRegistro = (DateTime)reader["FechaRegistro"],
                            Activo = (bool)reader["Activo"]
                        };
                    }
                    return null;
                }
            }
        }

        internal void CreateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}

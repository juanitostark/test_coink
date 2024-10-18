using Npgsql;
using MinimalApiCoink.Models;
using MinimalApiCoink.Interfaces;

namespace MinimalApiCoink.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM get_users();", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Telefono = reader.GetString(2),
                                Direccion = reader.GetString(3),
                                IdMunicipio = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return users;
        }

        public User GetUserById(int id)
        {
            User user = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM get_user_by_id(@id)", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Telefono = reader.GetString(2),
                                Direccion = reader.GetString(3),
                                IdMunicipio = reader.GetInt32(4)
                            };
                        }
                    }
                }
            }

            return user;
        }

        public void AddUser(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT insert_user(@nombre, @telefono, @direccion, @idMunicipio)", connection))
                {
                    // Agregar los parámetros al comando
                    command.Parameters.AddWithValue("@nombre", user.Nombre);
                    command.Parameters.AddWithValue("@telefono", user.Telefono);
                    command.Parameters.AddWithValue("@direccion", user.Direccion);
                    command.Parameters.AddWithValue("@idMunicipio", user.IdMunicipio);
                    // Ejecutar la función y obtener el id generado
                    var result = command.ExecuteScalar();

                    // Verificar si el resultado es nulo
                    if (result != null)
                    {
                        user.IdUsuario = (int)result;
                    }
                    else
                    {
                        throw new InvalidOperationException("The insert_user function did not return a valid id.");
                    }
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("CALL update_user(@id, @nombre, @telefono, @direccion, @id_municipio)", connection))
                {
                    command.Parameters.AddWithValue("id", user.IdUsuario);
                    command.Parameters.AddWithValue("nombre", user.Nombre);
                    command.Parameters.AddWithValue("telefono", user.Telefono);
                    command.Parameters.AddWithValue("direccion", user.Direccion);
                    command.Parameters.AddWithValue("id_municipio", user.IdMunicipio);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("CALL delete_user(@id)", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}


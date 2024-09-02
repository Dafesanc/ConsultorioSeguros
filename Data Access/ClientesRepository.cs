using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.IO;

namespace ConsultorioSeguros.Data
{
    public class ClientesRepository
    {
        private String ConnectionString = "Data Source=DESKTOP-7C1Q7LR\\SQLEXPRESS;Initial Catalog=ConsultorioSeguros;User=sa;Password=daniel;Trusted_Connection=True;TrustServerCertificate=True;";


        public bool Ok()
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }

            return true;
        }

        public IEnumerable<Clientes> GetAll()
        {
            var clientes = new List<Clientes>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT * FROM Clientes", connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Clientes
                            {
                                Id = (int)reader["Id"],
                                Cedula = (string)reader["Cedula"],
                                Nombre = (string)reader["Nombre"],
                                Telefono = (string)reader["Telefono"],
                                Edad = (int)reader["Edad"]

                            });
                        }
                        reader.Close();

                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return clientes;
        }

        public void AgregarCliente(Clientes cliente)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("INSERT INTO Clientes (Cedula, Nombre, Telefono, Edad) VALUES (@Cedula, @Nombre, @Telefono, @Edad)", connection);

                command.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@Edad", cliente.Edad);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
        }


        public Clientes GetById(int id)
        {
            Clientes Cliente = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT * FROM Clientes WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Cliente = new Clientes
                        {
                            Id = (int)reader["Id"],
                            Cedula = (string)reader["Cedula"],
                            Nombre = (string)reader["Nombre"],
                            Telefono = (string)reader["Telefono"],
                            Edad = (int)reader["Edad"]
                        };
                    }
                }
            }
            return Cliente;
        }

        public void Actualizar(Clientes cliente)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Clientes SET Cedula = @Cedula, Nombre = @Nombre, Telefono = @Telefono, Edad = @Edad WHERE Id = @Id",
                    connection
                );

                command.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@Edad", cliente.Edad);
                command.Parameters.AddWithValue("@Id", cliente.Id);


                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("DELETE FROM Clientes WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ProcesarExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Asume que los datos están en la primera hoja
                int rowCount = worksheet.Dimension.Rows;

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    for (int row = 2; row <= rowCount; row++) // Empieza desde 2 asumiendo que la fila 1 tiene encabezados
                    {
                        var cliente = new Clientes
                        {
                            Cedula = worksheet.Cells[row, 1].Text,
                            Nombre = worksheet.Cells[row, 2].Text,
                            Telefono = worksheet.Cells[row, 3].Text,
                            Edad = int.Parse(worksheet.Cells[row, 4].Text)
                        };

                        using (var command = new SqlCommand("INSERT INTO Clientes (Cedula, Nombre, Telefono, Edad) VALUES (@Cedula, @Nombre, @Telefono, @Edad)", connection))
                        {
                            command.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                            command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                            command.Parameters.AddWithValue("@Edad", cliente.Edad);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


    }
}

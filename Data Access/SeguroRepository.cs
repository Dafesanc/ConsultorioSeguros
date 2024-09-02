using System.Configuration;
using System.Linq;
using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
namespace ConsultorioSeguros.Data
{
    public class SeguroRepository
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

        public IEnumerable<Seguros> GetAll()
        {
            var seguros = new List<Seguros>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT * FROM Seguros", connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            seguros.Add(new Seguros
                            {
                                SeguroId = (int)reader["SeguroId"],
                                NombreSeguro = (string)reader["NombreSeguro"],
                                CodigoSeguro = (string)reader["CodigoSeguro"],
                                SumaAsegurada = (decimal)reader["SumaAsegurada"],
                                Prima = (decimal)reader["Prima"]

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
            return seguros;
        }

        public void Agregar(Seguros seguro)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("INSERT INTO Seguros (NombreSeguro, CodigoSeguro, SumaAsegurada, Prima) VALUES (@NombreSeguro, @CodigoSeguro, @SumaAsegurada, @Prima)", connection);

                command.Parameters.AddWithValue("@NombreSeguro", seguro.NombreSeguro);
                command.Parameters.AddWithValue("@CodigoSeguro", seguro.CodigoSeguro);
                command.Parameters.AddWithValue("@SumaAsegurada", seguro.SumaAsegurada);
                command.Parameters.AddWithValue("@Prima", seguro.Prima);

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


        public Seguros GetById(int id)
        {
            Seguros seguro = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT * FROM Seguros WHERE SeguroId = @SeguroId", connection);
                command.Parameters.AddWithValue("@SeguroId", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        seguro = new Seguros
                        {
                            SeguroId = (int)reader["SeguroId"],
                            NombreSeguro = (string)reader["NombreSeguro"],
                            CodigoSeguro = (string)reader["CodigoSeguro"],
                            SumaAsegurada = (decimal)reader["SumaAsegurada"],
                            Prima = (decimal)reader["Prima"]
                        };
                    }
                }
            }
            return seguro;
        }

        public void Actualizar(Seguros seguro)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Seguros SET NombreSeguro = @NombreSeguro, CodigoSeguro = @CodigoSeguro, SumaAsegurada = @SumaAsegurada, Prima = @Prima WHERE SeguroId = @SeguroId",
                    connection
                );

                command.Parameters.AddWithValue("@NombreSeguro", seguro.NombreSeguro);
                command.Parameters.AddWithValue("@CodigoSeguro", seguro.CodigoSeguro);
                command.Parameters.AddWithValue("@SumaAsegurada", seguro.SumaAsegurada);
                command.Parameters.AddWithValue("@Prima", seguro.Prima);
                command.Parameters.AddWithValue("@SeguroId", seguro.SeguroId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("DELETE FROM Seguros WHERE SeguroId = @SeguroId", connection);
                command.Parameters.AddWithValue("@SeguroId", id);
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
                        var seguro = new Seguros
                        {
                            NombreSeguro = worksheet.Cells[row, 1].Text,
                            CodigoSeguro = worksheet.Cells[row, 2].Text,
                            Prima = decimal.Parse(worksheet.Cells[row, 3].Text),
                            SumaAsegurada = decimal.Parse(worksheet.Cells[row, 4].Text)
                        };

                        using (var command = new SqlCommand("INSERT INTO Seguros (NombreSeguro, CodigoSeguro, Prima, SumaAsegurada) VALUES (@NombreSeguro, @CodigoSeguro, @Prima, @SumaAsegurada)", connection))
                        {
                            command.Parameters.AddWithValue("@NombreSeguro", seguro.NombreSeguro);
                            command.Parameters.AddWithValue("@CodigoSeguro", seguro.CodigoSeguro);
                            command.Parameters.AddWithValue("@Prima", seguro.Prima);
                            command.Parameters.AddWithValue("@SumaAsegurada", seguro.SumaAsegurada);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }



    }
}

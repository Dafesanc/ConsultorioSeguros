using Microsoft.Data.SqlClient;
using ConsultorioSeguros.Models;

namespace ConsultorioSeguros.Data
{
    public class RegistroVentaRepository
    {
        private readonly string _connectionString = "Data Source=DESKTOP-7C1Q7LR\\SQLEXPRESS;Initial Catalog=ConsultorioSeguros;User=sa;Password=daniel;Trusted_Connection=True;TrustServerCertificate=True;";

        public IEnumerable<RegistroVenta> GetAseguradoSeguroData()
        {
            var aseguradoSeguroList = new List<RegistroVenta>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT 
                            c.Nombre AS NombreCliente,
                            c.Cedula AS CedulaCliente,
                            s.NombreSeguro AS NombreSeguro,
                            asg.FechaContratacion AS FechaContratacion,
                            s.SumaAsegurada AS SumaAsegurada,
                            s.CodigoSeguro As CodSeguro
                          FROM 
                            AseguradoSeguro asg
                          INNER JOIN Clientes c ON asg.AseguradoId = c.Id
                          INNER JOIN Seguros s ON asg.SeguroId = s.SeguroId
                          ORDER BY 
                            c.Nombre;";

                var command = new SqlCommand(query, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aseguradoSeguro = new RegistroVenta
                        {
                            NombreCliente = reader["NombreCliente"].ToString(),
                            CedulaCliente = reader["CedulaCliente"].ToString(),
                            NombreSeguro = reader["NombreSeguro"].ToString(),
                            FechaContratacion = Convert.ToDateTime(reader["FechaContratacion"]),
                            SumaAsegurada = Convert.ToDecimal(reader["SumaAsegurada"]),
                            CodigoSeguro = reader["CodSeguro"].ToString()
                        };

                        aseguradoSeguroList.Add(aseguradoSeguro);
                    }
                }
            }

            return aseguradoSeguroList;
        }
    }
}

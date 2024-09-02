using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;

namespace ConsultorioSeguros.Data
{
    public class AseguradoSeguroRepository
    {
        private String ConnectionString = "Data Source=DESKTOP-7C1Q7LR\\SQLEXPRESS;Initial Catalog=ConsultorioSeguros;User=sa;Password=daniel;Trusted_Connection=True;TrustServerCertificate=True;";


        public void InsertarAseguradoSeguro(AseguradoSeguro aseguradoSeguro)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "INSERT INTO AseguradoSeguro (AseguradoId, SeguroId, FechaContratacion, CedulaAsegurado) VALUES (@AseguradoId, @SeguroId, @FechaContratacion, @CedulaAsegurado)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AseguradoId", aseguradoSeguro.AseguradoId);
                    command.Parameters.AddWithValue("@SeguroId", aseguradoSeguro.SeguroId);
                    command.Parameters.AddWithValue("@FechaContratacion", aseguradoSeguro.FechaContratacion);
                    command.Parameters.AddWithValue("@CedulaASegurado", aseguradoSeguro.CedulaAsegurado);

                    try
                    {
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                    
                }
            }
        }


    }
}

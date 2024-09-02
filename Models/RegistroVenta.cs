namespace ConsultorioSeguros.Models
{
    public class RegistroVenta
    {
        public string NombreCliente { get; set; }
        public string CedulaCliente { get; set; }
        public string NombreSeguro { get; set; }

        

        public DateTime FechaContratacion { get; set; }

        public decimal SumaAsegurada { get; set; }

        public string CodigoSeguro { get; set; }

        public RegistroVenta() { }


        public RegistroVenta(string NombreCliente, string CedulaCliente, string NombreSeguro, DateTime FechaContratacion, decimal SumaAsegurada, string codigoSeguro)
        {
            this.NombreCliente = NombreCliente;
            this.CedulaCliente = CedulaCliente;
            this.NombreSeguro = NombreSeguro;
            this.FechaContratacion = FechaContratacion;
            this.SumaAsegurada = SumaAsegurada;
            CodigoSeguro = codigoSeguro;
        }



    }
}

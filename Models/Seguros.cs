namespace ConsultorioSeguros.Models
{
    public class Seguros
    {
        public int SeguroId { get; set; }
        public string? NombreSeguro { get; set; }
        public string? CodigoSeguro { get; set; }
        public decimal SumaAsegurada { get; set; }
        public decimal Prima { get; set; }

        public Seguros() { 
        }
        public Seguros(int seguroId, string nombreSeguro, string codigoSeguro, decimal sumaAsegurada, decimal prima)
        {
            SeguroId = seguroId;
            NombreSeguro = nombreSeguro;
            CodigoSeguro = codigoSeguro;
            SumaAsegurada = sumaAsegurada;
            Prima = prima;
        }
    }
}

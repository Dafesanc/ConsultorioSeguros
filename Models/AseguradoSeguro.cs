namespace ConsultorioSeguros.Models
{
    public class AseguradoSeguro
    {
        public int AseguradoId { get; set; }
        public int SeguroId { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string CedulaAsegurado { get; set; }

        public AseguradoSeguro()
        {

        }
        public AseguradoSeguro(int aseguradoId, int seguroId, DateTime fechaContratacion, string cedulaAsegurado)
        {
            AseguradoId = aseguradoId;
            SeguroId = seguroId;
            FechaContratacion = fechaContratacion;
            CedulaAsegurado = cedulaAsegurado;
        }
    }


}

namespace ConsultorioSeguros.Models
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public int  Edad { get; set; }

        public Clientes()
        {

        }
        public Clientes (int Id, string Cedula, string Nombre, string Telefono, int Edad) {
            this.Id = Id;
            this.Cedula = Cedula;
            this.Nombre = Nombre;
            this.Telefono = Telefono;
            this.Edad = Edad;

        }
}
}

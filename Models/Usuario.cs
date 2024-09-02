namespace ConsultorioSeguros.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
        public Usuario()
        {

        }

        public Usuario(int IdUsuario, string NombreCompleto, string Correo, string User, string Password)
        {
                this.IdUsuario = IdUsuario;
               this.NombreCompleto = NombreCompleto;
               this.Correo = Correo;
               this.User = User;
               this.Password = Password;
        }
    }


}

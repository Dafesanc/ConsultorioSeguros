namespace ConsultorioSeguros.ViewModels
{
    public class UsuarioVM
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UsuarioVM()
        {

        }

        public UsuarioVM(int IdUsuario, string NombreCompleto, string Correo, string User, string Password, string ConfirmPassword)
        {
            this.IdUsuario = IdUsuario;
            this.NombreCompleto = NombreCompleto;
            this.Correo = Correo;
            this.User = User;
            this.Password = Password;
            this.ConfirmPassword = ConfirmPassword;
        }
    }
}

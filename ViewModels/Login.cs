namespace ConsultorioSeguros.ViewModels
{
    public class Login
    {
        public string Correo { get; set; }
        public string Password { get; set; }

       public Login() { }
        public Login(string Correo, string Password)
        {
           this.Correo = Correo;
            this.Password = Password;
        }
    }

    
}

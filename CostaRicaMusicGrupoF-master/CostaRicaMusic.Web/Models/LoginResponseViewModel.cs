namespace CostaRicaMusic.Web.Models
{
    public class LoginResponseViewModel
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
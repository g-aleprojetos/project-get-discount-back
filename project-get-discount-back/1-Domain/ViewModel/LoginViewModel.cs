using System.Text.Json.Serialization;

namespace project_get_discount_back.ViewModel
{
    public class LoginViewModel
    {
        public Boolean? Auth { get; set; }
        public string? AccessToken { get; set; }
        public string? Mensagem { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }

    public class RefreshToken
    {
        public Guid? Id { get; set; }
        public int? ExpiresIn { get; set; }
        public Guid? UsuarioId { get; set; }

    }
}



namespace DalilAcademy.Core.DtOs
{
    public class Auth
    {
        public record LoginRequestDTO(string Email ,string password);
        public record LoginReponsetDTO(string Token, DateTime ExpiresAt);
        public record RegisterRequestDTO(string FullName, string Email, string password);
        public record RegisterResponsetDTO(string FullName, string Email, string Role, string Token, DateTime ExpiresAt);

    }
}

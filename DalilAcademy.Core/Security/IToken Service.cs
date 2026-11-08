using DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Security
{
   public interface IToken_Service
    {
        (string Token, DateTime ExpiresAt) CreateToken(User user);
    }
}

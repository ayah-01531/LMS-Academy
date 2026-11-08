using DalilAcademy.Core.DtOs;
using static DalilAcademy.Core.DtOs.Auth;

namespace DalilAcademy.Core.Service
{
   public interface IAuthServices
    {
        Task<LoginReponsetDTO> LoginAsync(LoginRequestDTO dto);
        Task<RegisterResponsetDTO> RegisterAsync(RegisterRequestDTO dto);
    }
}

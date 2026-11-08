using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Security;
using DalilAcademy.Core.Service;
using static DalilAcademy.Core.DtOs.Auth;

namespace DalilAcademy.Infra.Service
{
    public class AuthServices(IUserRepository users, IPasswordHasher hasher, IToken_Service tokens) : IAuthServices
    {
        public async Task<LoginReponsetDTO> LoginAsync(LoginRequestDTO dto)
        {
            var user = await users.GetByEmail(dto.Email);
            if (user is null || !hasher.Verfiy(dto.password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invaild email or password");
           var(token , expiresAt) = tokens.CreateToken(user);
            return new LoginReponsetDTO(token, expiresAt);
        }

        public async Task<RegisterResponsetDTO> RegisterAsync(RegisterRequestDTO dto)
        {
            if(await users.GetByEmail(dto.Email)is not null)
                throw new UnauthorizedAccessException("Email is already registered");
            var user = new User
            {
                FullName = dto.FullName.Trim(),
                Email = dto.Email,
                PasswordHash = hasher.Hash(dto.password),
                Role = Roles.Student
            };
            await users.AddAsync(user);
            var(token, expiresAt) = tokens.CreateToken(user);

            return new RegisterResponsetDTO(user.FullName, user.Email, user.Role, token, expiresAt);

        }
    }
}

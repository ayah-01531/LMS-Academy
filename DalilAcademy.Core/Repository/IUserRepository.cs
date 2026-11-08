using DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task AddAsync(User user);




    }
}

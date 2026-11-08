
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace DalilAcademy.Infra.Repository
{
    public class UserRepository (AppDbContext db): IUserRepository
    {
        public Task<User?> GetByEmail(string email) 
            => db.Users.AsNoTracking()//no snapshot
            .FirstOrDefaultAsync(u => u.Email == email);
        //هون اخيرا بالتسجل ال user بال database
        public async Task AddAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();

        }
  
    }
}

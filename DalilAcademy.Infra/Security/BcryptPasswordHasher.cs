using DalilAcademy.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Infra.Security
{
   public class BcryptPasswordHasher :IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verfiy(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);

    }
}

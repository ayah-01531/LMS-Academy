using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verfiy(string password, string hash);

    }
}

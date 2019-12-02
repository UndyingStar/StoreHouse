using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
   public interface IUsersRepository
    {
        IEnumerable<Users> Users { get; }
        void SaveUser(Users user);
        void DeleteUser(Users user);
        void CreateUser(Users user);
        bool FindUser(Users user);
    }
}

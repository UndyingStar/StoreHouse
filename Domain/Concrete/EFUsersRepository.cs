using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFUsersRepository: IUsersRepository
    {
            EFDbContext context = new EFDbContext();
            public IEnumerable<Users> Users
            {
                get { return context.Users; }
            }

        public bool FindUser(Users user)
        {
            Users dbEntry = context.Users.Find(user.UserID);
            if(dbEntry != null)
            {
                return true;
            }
            return false;
        }
            
            public void SaveUser(Users users)
            {
                if (users.UserID == 0)
                {
                    context.Users.Add(users);
                }
                else
                {
                    Users dbEntry = context.Users.Find(users.UserID);
                    if (dbEntry != null)
                    {
                        dbEntry.Login = users.Login;
                        dbEntry.Password = users.Password;
                        dbEntry.Role = users.Role;
                        dbEntry.FirstName = users.FirstName;
                        dbEntry.LastName = users.LastName;
                    }
                }
                context.SaveChanges();
            }

            public void DeleteUser(Users user)
            {
                context.Users.Attach(user);
                context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            public void CreateUser(Users user)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }

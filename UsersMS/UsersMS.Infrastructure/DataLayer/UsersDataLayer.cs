using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Infrastructure.Domain.DbCtx;
using UsersMS.Infrastructure.Domain.Entities;

namespace UsersMS.Infrastructure.DataLayer
{
    public class UsersDataLayer : IUsersDataLayer
    {
        public async Task<User> AddUser(User newuser)
        {
            using (UserMsDbContext db = new UserMsDbContext())
            {
                db.Users.Add(newuser);
                await db.SaveChangesAsync();

                return newuser;
            }
        }

        public async Task<bool> Edit(User userById)
        {
            using (UserMsDbContext db = new UserMsDbContext())
            {
                int rowsUpdated = 0;

                db.Users.Update(userById);

                rowsUpdated = await db.SaveChangesAsync();

                return rowsUpdated > 0;
            }
        }

        public async Task<User> GetUserById(long id)
        {
            using (UserMsDbContext db = new UserMsDbContext())
            {
                User userById = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

                return userById != null ? userById : new User();
            }
        }
    }
}

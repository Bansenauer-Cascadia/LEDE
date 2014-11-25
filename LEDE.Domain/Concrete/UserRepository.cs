using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Concrete
{
    public class UserRepository : IUserRepository 
    {
        private DbContext db;

        public UserRepository(DbContext context)
        {
            this.db = context; 
        }

        public UserRepository()
        {
            this.db = new DbContext(); 
        }

        public IEnumerable<IndexModel> GetUsersWithRole()
        {
            var usersquery = db.Users.Include(u => u.Roles).ToList();
            List<IndexModel> users = new List<IndexModel>(); 

            foreach (User user in usersquery)
            {
                IndexModel viewModel = new IndexModel()
                {
                    Id = user.Id, 
                    FirstName = user.FirstName,
                    LastName = user.LastName, 
                    Role = db.Roles.Find(user.Roles.FirstOrDefault().RoleId).Name
                };
           
                users.Add(viewModel); 
            }

            return users.OrderBy(u=> u.LastName);

        }

        public User Find(int Id)
        {
            return db.Users.Find(Id); 
        }

        public void Edit(User updatedUser)
        {
            User oldUser = db.Users.Find(updatedUser.Id);
            if (oldUser != null)
            {
                //There has to be a better way to update
                oldUser.LastName = updatedUser.LastName;
                oldUser.FirstName = updatedUser.FirstName;
                oldUser.UserName = updatedUser.UserName;
                oldUser.UniversityID = updatedUser.UniversityID; 

                db.SaveChanges();

            }
               
        }
    }
}
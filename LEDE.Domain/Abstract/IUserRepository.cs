using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<IndexModel> GetUsersWithRole();

        User Find(int Id);

        void Edit(User user); 

    }
}

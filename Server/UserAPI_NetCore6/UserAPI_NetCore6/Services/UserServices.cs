using EF.Core.Repository.Interface.Repository;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using UserAPI_NetCore6.DataConfig;
using UserAPI_NetCore6.Models;

namespace UserAPI_NetCore6.Services
{
    public interface IUserServices: ICommonRepository<User>
    {
        
    }

    public class UserServices : CommonRepository<User> ,IUserServices
    {
        //private readonly MyDbContext _myDbContext;
        public UserServices(MyDbContext myDbContext): base(myDbContext)
        {
            //_myDbContext = myDbContext;
        }

        
    }
}

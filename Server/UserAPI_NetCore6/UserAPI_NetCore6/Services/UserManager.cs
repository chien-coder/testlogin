using EF.Core.Repository.Interface.Manager;
using EF.Core.Repository.Interface.Repository;
using EF.Core.Repository.Manager;
using EF.Core.Repository.Repository;
using UserAPI_NetCore6.DataConfig;
using UserAPI_NetCore6.Models;

namespace UserAPI_NetCore6.Services
{
    public interface IUserManager: ICommonManager<User>
    {
        Task<User?> GetUserByUsernameOfAdminAsync(string un);
        Task<User?> GetUserByIdAsync(string id);
        Task<bool> AddNewUserAsync(User user);
        Task UpdateUserByIdAsync(User user);
        //Task<List<User>> GetAllUserAsync();
        Task DeleteUserByIdAsync(User user);
        Task<ICollection<User>> GetAllStaffAsync();
    }

    public class UserManager:CommonManager<User>, IUserManager
    {
        public UserManager(MyDbContext myDbContext) : base(new UserServices(myDbContext))
        {
        }

        public async Task<bool> AddNewUserAsync(User user)   => await AddAsync(user);


        public async Task DeleteUserByIdAsync(User user)
        {
            await DeleteAsync(user);
        }

        public async Task<ICollection<User>> GetAllStaffAsync()
        {
            return await GetAsync(u => u.roles == Roles.staff);
        }

        //public async Task<List<User>> GetAllUserAsync()
        //    => await _myDbContext.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(string id)
        {
            Guid guid = Guid.Parse(id);
            return await GetFirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<User?> GetUserByUsernameOfAdminAsync(string un)
            => await GetFirstOrDefaultAsync(u => u.username == un && u.roles == Roles.Admin);

        public async Task UpdateUserByIdAsync(User user)
        {
            await UpdateAsync(user);
        }
    }
}

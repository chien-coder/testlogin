using Microsoft.EntityFrameworkCore;
using UserAPI_NetCore6.Models;

namespace UserAPI_NetCore6.DataConfig
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        #region Dbset
        public DbSet<User> Users { get; set; }
        #endregion

    }
}

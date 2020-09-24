namespace Logger.Infrastructure.DbContext
{
    using Microsoft.EntityFrameworkCore;
    using Models.Entities;

    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Log> Log { get; set; }
    }
}
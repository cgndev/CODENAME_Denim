using Denim.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Denim.DataAccess
{
    public class DenimDbContext : DbContext
    {
        public DenimDbContext() : base("DenimDb")
        {

        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Department> Departments { get; set; }
        

        public DbSet<Friend> Friends { get; set; }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public DbSet<FriendPhoneNumber> FriendPhoneNumbers { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

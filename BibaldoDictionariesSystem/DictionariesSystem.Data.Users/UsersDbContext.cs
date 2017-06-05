namespace DictionariesSystem.Data.Users
{
    using DictionariesSystem.Models.Users;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class UsersDbContext : DbContext
    {
        private const string ConnectionStringName = "Users";

        public UsersDbContext()
            : base(ConnectionStringName)
        {
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Badge> Badges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.OnUserModelCreating(modelBuilder);
            this.OnBadgeModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void OnUserModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
        }

        private void OnBadgeModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}

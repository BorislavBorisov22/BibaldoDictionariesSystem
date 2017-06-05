namespace DictionariesSystem.Data.Users
{
    using DictionariesSystem.Models.Users;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;

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

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("IX_Username")
                        {
                            IsUnique = true
                        }));

            modelBuilder.Entity<User>()
                .Property(x => x.Passhash)
                .IsRequired()
                .HasMaxLength(50);


            modelBuilder.Entity<User>()
                .Property(x => x.ContributionsCount)
                .IsOptional();                
        }

        private void OnBadgeModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Badge>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Badge>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("IX_Name")
                        {
                            IsUnique = true
                        }));

            modelBuilder.Entity<Badge>()
                .Property(x => x.RequiredContributions)
                .IsRequired();                
        }
    }
}

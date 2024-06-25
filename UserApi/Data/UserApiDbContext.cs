using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data
{
    public class UserApiDbContext : DbContext
    {
        public UserApiDbContext(DbContextOptions<UserApiDbContext> options) : base(options)
        {
            // Ensure the database is created
            Database.EnsureCreated();
        }

        // Define your entity sets here
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserChat> UserChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            // Users
            modelBuilder.Entity<User>().ToTable("Users");

            // Chats
            modelBuilder.Entity<Chat>().ToTable("Chats");

            // UserChat
            modelBuilder.Entity<UserChat>().ToTable("UserChats");
            modelBuilder.Entity<UserChat>().HasKey(uc => new { uc.UserId, uc.ChatId });
            modelBuilder.Entity<UserChat>().HasOne<User>(uc => uc.User).WithMany(u => u.UserChats).HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserChat>().HasOne<Chat>(uc => uc.Chat).WithMany(c => c.UserChats).HasForeignKey(uc => uc.ChatId);
        }
    }
}
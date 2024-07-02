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
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            // Users
            modelBuilder.Entity<User>().ToTable("Users");

            // Chats
            modelBuilder.Entity<Chat>().ToTable("Chats");
            modelBuilder.Entity<Chat>().HasOne(c => c.Group).WithMany(g => g.Chats).HasForeignKey(c => c.GroupId);

            // Group
            modelBuilder.Entity<Group>().ToTable("Groups");

            // UserGroup
            modelBuilder.Entity<UserGroup>().ToTable("UserGroups");
            modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });
            modelBuilder.Entity<UserGroup>().HasOne(ug => ug.User).WithMany(u => u.UserGroups).HasForeignKey(ug => ug.UserId);
            modelBuilder.Entity<UserGroup>().HasOne(ug => ug.Group).WithMany(g => g.UserGroups).HasForeignKey(ug => ug.GroupId);

            // Message
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            modelBuilder.Entity<Message>().HasOne(m => m.User).WithMany(u => u.Messages).HasForeignKey(m => m.SenderId);
            modelBuilder.Entity<Message>().HasOne(m => m.Chat).WithMany(c => c.Messages).HasForeignKey(m => m.ChatId);


            // Contact
            modelBuilder.Entity<Contact>().ToTable("Contacts");
            modelBuilder.Entity<Contact>().HasKey(c => c.Id);
            modelBuilder.Entity<Contact>().HasOne(c => c.UserContact).WithMany(u => u.Contacts).HasForeignKey(c => c.UserContactId);
        }
    }
}
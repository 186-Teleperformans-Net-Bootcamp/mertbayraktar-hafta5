using MertBayraktar.Social.Network.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MertBayraktar.Social.Network.Api.Data
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendshipStatus> FriendshipStatuses { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<UserMessageHistory> UserMessageHistories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> Memberships { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<GroupMessageHistory> GroupMessageHistories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FriendshipApprovement> FriendshipsApprovements { get; set; }

    }
}

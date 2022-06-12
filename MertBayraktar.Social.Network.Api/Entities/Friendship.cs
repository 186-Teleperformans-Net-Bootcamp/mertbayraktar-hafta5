using System.ComponentModel.DataAnnotations.Schema;

namespace MertBayraktar.Social.Network.Api.Entities
{
    public class Friendship
    {
        public int Id { get; set; }

        [ForeignKey("Sender")]
        public User User { get; set; }

        [ForeignKey("Reciever")]
        public User Friend { get; set; }

        public FriendshipStatus Status { get; set; }
    }
}

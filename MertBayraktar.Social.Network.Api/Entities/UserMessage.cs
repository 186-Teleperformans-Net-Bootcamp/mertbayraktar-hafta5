using System.ComponentModel.DataAnnotations.Schema;

namespace MertBayraktar.Social.Network.Api.Entities
{
    public class UserMessage
    {
        public int Id { get; set; }
        [ForeignKey("Sender")]
        public User User { get; set; }
        [ForeignKey("Reciever")]
        public User Friend { get; set; }
        public DateTime SendDate { get; set; }
        public ContentType Type { get; set; }
        public string Content { get; set; }
    }
}

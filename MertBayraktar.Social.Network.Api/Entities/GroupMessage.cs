using System.ComponentModel.DataAnnotations.Schema;

namespace MertBayraktar.Social.Network.Api.Entities
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public ContentType Type { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        [ForeignKey("Sender")]
        public User GroupMember { get; set; }

        [ForeignKey("Reciever")]
        public Group Group { get; set; }
    }
}

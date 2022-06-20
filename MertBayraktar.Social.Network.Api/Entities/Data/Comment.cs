using System.ComponentModel.DataAnnotations.Schema;

namespace MertBayraktar.Social.Network.Api.Entities.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public ContentType Type { get; set; }

        [ForeignKey("Sender")]
        public User User { get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}

namespace MertBayraktar.Social.Network.Api.Entities
{
    public class GroupMembership
    {
        public int Id { get; set; }

        public User GroupMember { get; set; }
        public int GroupMemberId { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }
    }
}

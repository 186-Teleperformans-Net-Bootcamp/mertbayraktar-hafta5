namespace MertBayraktar.Social.Network.Api.Entities
{
    public class FriendshipApprovement
    {
        public int Id { get; set; }
        public int FriendshipId { get; set; }

        public int StatusId { get; set; }
    }
}

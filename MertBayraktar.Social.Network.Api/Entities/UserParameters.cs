namespace MertBayraktar.Social.Network.Api.Entities
{
    public class UserParameters : QueryStringParameters
    {
        public int MinYearOfBirth { get; set; }
        public int MaxYearOfBirth { get; set; } = (int)DateTime.Now.Year;
    }
}

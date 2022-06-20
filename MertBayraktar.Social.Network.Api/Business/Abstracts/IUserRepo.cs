using MertBayraktar.Social.Network.Api.Entities;
using MertBayraktar.Social.Network.Api.Entities.Data;

namespace MertBayraktar.Social.Network.Api.Business.Abstracts
{
    public interface IUserRepo : IRepo<User>
    {
        Task<PagedList<User>> GetAllUsers(UserParameters parameters);
    }
}

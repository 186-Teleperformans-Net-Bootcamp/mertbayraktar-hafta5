using MertBayraktar.Social.Network.Api.Business.Abstracts;
using MertBayraktar.Social.Network.Api.Data;
using MertBayraktar.Social.Network.Api.Entities;
using MertBayraktar.Social.Network.Api.Entities.Data;

namespace MertBayraktar.Social.Network.Api.Business.Concretes
{
    public class UserRepository : GenericRepository<User>, IUserRepo
    {
        public UserRepository(Context _context) : base(_context) { }

        public Task<PagedList<User>> GetAllUsers(UserParameters parameters)
        {
            var users = FindByCondition(x => x.Birthdate.Value.Year <= parameters.MinYearOfBirth &&
            x.Birthdate.Value.Year <= parameters.MaxYearOfBirth);


            return Task.FromResult
             (PagedList<User>.ToPagedList(users.OrderBy(x => x.UserName), parameters.PageNumber, parameters.PageSize));
        }

    }
}

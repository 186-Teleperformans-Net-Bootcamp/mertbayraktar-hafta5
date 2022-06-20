using System.Linq.Expressions;

namespace MertBayraktar.Social.Network.Api.Business.Abstracts
{
    public interface IRepo<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}

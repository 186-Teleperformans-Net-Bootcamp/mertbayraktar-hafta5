using System.Linq.Expressions;
using MertBayraktar.Social.Network.Api.Business.Abstracts;
using MertBayraktar.Social.Network.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MertBayraktar.Social.Network.Api.Business.Concretes
{
    public class GenericRepository<T> : IRepo<T> where T : class
    {

        protected Context _context { get; set; }

        public GenericRepository(Context context) => _context = context;


        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).AsNoTracking();
        }
    }
}

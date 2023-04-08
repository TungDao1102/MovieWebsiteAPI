using System.Linq.Expressions;

namespace APIWebMovie.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<TMapper> GetById<TMapper>(int id) where TMapper : class;
        Task<IEnumerable<TMapper>> GetAll<TMapper>() where TMapper : class;
        Task<bool> Add<TMapper>(TMapper mapper) where TMapper : class;
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
        Task<List<TMapper>> FindToList<TMapper>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, int? quantity = null) where TMapper : class;
        Task<TMapper> Find<TMapper>(Expression<Func<T, bool>> predicate) where TMapper : class;
        Task<T> FindToEntity(Expression<Func<T, bool>> predicate);
    }
}

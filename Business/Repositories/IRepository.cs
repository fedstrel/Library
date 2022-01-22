using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IRepository<T, TKey>
    {
        T Read(TKey key);
        void Update(T value);
        Task UpdateAsync(T value);
        void Create(T value);
        Task CreateAsync(T value);
        void CreateOrUpdate(T value);
        void Delete(T value);
        Task DeleteAsync(T value);

        void Create(IEnumerable<T> values);
        void Update(IEnumerable<T> values);
        void CreateOrUpdate(IEnumerable<T> values);
        void Delete(IEnumerable<T> values);

        List<T> Query(Expression<Func<T, bool>> where = null);

        TResult Query<TResult>(Func<IQueryable<T>, TResult> body);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository
{
    public abstract class AbstractRepository<T, TKey> : IRepository<T, TKey> where T : class
    {
        private readonly object _syncRoot = new object();
        protected DbSet<T> table;
        protected Context _context;
        #region implementation
        protected void CreateOrUpdateImplementation(T value)
        {
            var entity = ReadImplementation(KeySelector(value));
            if (entity == null) 
                CreateImplementation(value);
            else 
                UpdateImplementation(value);
        }

        protected void CreateOrUpdateImplementation(IEnumerable<T> values)
        {
            foreach (T value in values)
                CreateOrUpdateImplementation(value);
        }

        protected void CreateImplementation(T value)
        {
            _context.Add(value);      
        }

        protected void CreateImplementation(IEnumerable<T> values)
        {
            foreach(T value in values)
                _context.Add(value);
        }

        protected void UpdateImplementation(T value)
        {
            _context.Update(value);
        }

        protected void UpdateImplementation(IEnumerable<T> values)
        {
            _context.UpdateRange(values);  
        }

        protected void DeleteImplementation(T value)
        {
            var entity = ReadImplementation(KeySelector(value));
            if (entity == null) 
                return;
            _context.Remove(entity);
        }

        protected void DeleteImplementation(IEnumerable<T> values)
        {
            foreach (T value in values)
                DeleteImplementation(value);
        }

        protected void OperationEnvironment(Action body)
        {
            lock (_syncRoot)
            {

                body.Invoke();
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ex.Entries.Single().Reload();
                    _context.SaveChanges();
                }
            }
        }

        protected TRet OperationEnvironment<TRet>(Func<TRet> body)
        {
            lock (_syncRoot)
            {
                return body.Invoke();
            }
        }
        #endregion

        #region abstract
        protected abstract TKey KeySelector(T value);
        protected abstract T ReadImplementation(TKey key);

        protected abstract IQueryable<T> QueryImplementation();
        #endregion

        #region interface
        public async Task CreateAsync(T value)
        {
            await _context.AddAsync(value);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T value)
        {
            table.Update(value);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T value)
        {
            table.Remove(value);
            await _context.SaveChangesAsync();
        }
        public void Create(T value)
        {
            OperationEnvironment(() => CreateImplementation(value));
        }

        public void Create(IEnumerable<T> values)
        {
            OperationEnvironment(() => CreateImplementation(values));
        }

        public void CreateOrUpdate(T value)
        {
            OperationEnvironment(() => CreateOrUpdateImplementation(value));
        }

        public void CreateOrUpdate(IEnumerable<T> values)
        {
            OperationEnvironment(() => CreateOrUpdateImplementation(values));
        }

        public void Delete(T value)
        {
            OperationEnvironment(() => DeleteImplementation(value));
        }

        public void Delete(IEnumerable<T> values)
        {
            OperationEnvironment(() => DeleteImplementation(values));
        }

        public List<T> Query(Expression<Func<T, bool>> where = null)
        {
            return OperationEnvironment(() =>
            {
                var query = QueryImplementation();
                if (where != null) query = query.Where(where);
                return query.ToList();
            });
        }

        public TResult Query<TResult>(Func<IQueryable<T>, TResult> body)
        {
            return OperationEnvironment(() =>
            {
                var query = QueryImplementation();
                return body(query);
            });
        }

        public T Read(TKey key)
        {
            return OperationEnvironment(() => ReadImplementation(key));
        }

        public void Update(T value)
        {
            OperationEnvironment(() => UpdateImplementation(value));
        }

        public void Update(IEnumerable<T> values)
        {
            OperationEnvironment(() => UpdateImplementation(values));
        }
        #endregion
    }
}

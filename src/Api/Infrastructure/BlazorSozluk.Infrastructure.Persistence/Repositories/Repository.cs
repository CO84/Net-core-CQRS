using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        //private readonly BlazorSozlukContext _context;
        private readonly DbContext _context;

        protected DbSet<TEntity> entity => _context.Set<TEntity>();
        public Repository(DbContext context)
        {
            _context = context;
        }

        #region insert
        public void Add(TEntity entity)
        {
           this.entity.Add(entity);
           _context.SaveChanges();
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        #endregion

        public virtual int AddOrUpdate(TEntity entity)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
                _context.Update(entity);

            return  _context.SaveChanges();
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
                _context.Update(entity);
            
            return await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return query.AsNoTracking();
        }
        #region bulk
        public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if(entities is not null && !entities.Any()) await Task.CompletedTask;
            
               await entity.AddRangeAsync(entities);
            
            await _context.SaveChangesAsync();
        }

        public Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if(ids is not null && !ids.Any()) return Task.CompletedTask;

            _context.RemoveRange(entity.Where(x => ids.Contains(x.Id)));
            return _context.SaveChangesAsync();
        }

        public Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
        #endregion
        public virtual int Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);
            return _context.SaveChanges();
        }

        public virtual int Delete(Guid id)
        {
            var entity = this.entity.Find(id);
            return Delete(entity);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);
            return  await _context.SaveChangesAsync();
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
           var entity = this.entity.Find(id);
            return DeleteAsync(entity);
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
           _context.RemoveRange(entity.Where(predicate));
            return _context.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            _context.RemoveRange(entity.Where(predicate));
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();
            if (predicate is not null) query = query.Where(predicate);

                query = ApplyIncludes(query, includes);

            if(noTracking) query = query.AsNoTracking();

            return query;
        }

        public Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await entity.FindAsync(id);
            if (found is null) return null;

            if (noTracking) _context.Entry(found).State = EntityState.Detached;

            foreach (Expression<Func<TEntity, object>>include in includes)
            {
                _context.Entry(found).Reference(include).Load();
            }

            return found;
        }

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;
            if (predicate is not null) query = query.Where(predicate);

            foreach (Expression<Func<TEntity, object>>include in includes)
            {
                query = query.Include(include);
            }
            if(orderBy is not null)
            {
                query = orderBy(query);
            }

            if (noTracking) query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
           IQueryable<TEntity> query = entity;

            if(predicate is not null)
            {
                query = query.Where(predicate);
            }
            query = ApplyIncludes(query, includes);

            if (noTracking) query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        public virtual int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query,params Expression<Func<TEntity, object>>[] includes)
        {
            if(includes is not null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }
            return query;
        }
    }
}

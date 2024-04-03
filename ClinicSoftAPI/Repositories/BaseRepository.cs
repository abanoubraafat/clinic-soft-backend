using ClinicSoftAPI.IRepositories;
using ClinicSoftAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClinicSoftAPI.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected BookingClinicsContext _context;

        public BaseRepository(BookingClinicsContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return entity;
        }
        //public bool login(string email, string passward)
        //{
        //    var q = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == passward);
        //    if (q != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public IEnumerable<TEntity> GetAll(string Include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            if (Include != null)
                query = query.Include(Include);
            return query.ToList();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public bool IsExists(int id)
        {
            return _context.Set<TEntity>().Find(id) != null;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria,
            Expression<Func<TEntity, object>> orderBy = null, string[] includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(criteria);
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (orderBy != null)
                query = query.OrderBy(orderBy);
            return query.ToList();
        }
        public TEntity Find(Expression<Func<TEntity, bool>> criteria, string[] includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);
        }
        ////
        ///
        public TEntity attach(TEntity obj)
        {
            _context.Set<TEntity>().Attach(obj);
            return obj;
        }

        public int count()
        {
            return _context.Set<TEntity>().Count();
        }

        public int count(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Count(expression);
        }
    }
}

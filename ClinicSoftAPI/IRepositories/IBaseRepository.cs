using System.Linq.Expressions;

namespace ClinicSoftAPI.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(string Include = null);
        TEntity GetById(int id);
        Task<TEntity> Add(TEntity entity);
        TEntity Delete(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        bool IsExists(int id);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria,
            Expression<Func<TEntity, object>> orderBy = null, string[] includes = null);
        TEntity Find(Expression<Func<TEntity, bool>> criteria,
            string[] includes = null);
        ///
        TEntity attach(TEntity obj);
        int count();
        int count(Expression<Func<TEntity, bool>> expression);
        //bool login(string username, string password );
    }
}

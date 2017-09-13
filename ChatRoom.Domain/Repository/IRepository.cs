using System.Collections.Generic;
using System.Linq;
using ChatRoom.Domain.Entities;

namespace ChatRoom.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Query();
        TEntity Get(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
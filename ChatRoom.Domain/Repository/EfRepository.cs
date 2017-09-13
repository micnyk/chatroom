using System.Collections.Generic;
using System.Linq;
using ChatRoom.Domain.Entities;

namespace ChatRoom.Domain.Repository
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public TEntity Get(object id)
        {
            return _context
                .Set<TEntity>()
                .FirstOrDefault(x => id.Equals(x.Id));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context
                .Set<TEntity>()
                .AsEnumerable();
        }

        public IQueryable<TEntity> Query()
        {
            return _context
                .Set<TEntity>()
                .AsQueryable();
        }

        public void Insert(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
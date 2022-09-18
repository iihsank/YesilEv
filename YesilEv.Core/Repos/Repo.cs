using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core.Interfaces;

namespace YesilEv.Core.Repos
{
    public abstract class Repo<TContext, TEntity> :
        ISelectableRepo<TEntity>,
        IUpdatableRepo<TEntity>,
        IInsertableRepo<TEntity>,
        IDeletetableRepo<TEntity>
        where TEntity:class
        where TContext:DbContext,new()
    {
        private readonly TContext _context;
        public Repo()
        {
            _context = new TContext();
        }
        public Repo(TContext context )
        {
            _context = context;
        }

        public TEntity Add(TEntity item)
        {
            return _context.Set<TEntity>().Add(item);
        }

        public List<TEntity> AddRange(List<TEntity> items)
        {
            return _context.Set<TEntity>().AddRange(items).ToList();
        }

        public TEntity Delete(TEntity item)
        {
            return _context.Set<TEntity>().Remove(item);
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetByID(object ID)
        {
            return _context.Set<TEntity>().Find(ID);
        }

        public void MySaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Set<TEntity>().Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> //Temel sinif'lar new'lenmasin, base class'i devralan siniflar new'lensin.
    where T : class, new() //Kisitlayicilar: Tip referans tip, new'lenebilir(instance olusturulabilir tip)
    {
        protected readonly RepositoryContext _context;
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);  
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges
                ? _context.Set<T>()  //Degisiklikler izlenmek isteniyorsa
                : _context.Set<T>().AsNoTracking(); //Degisiklikler izlenmeyecekse
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
                ? _context.Set<T>().Where(expression).SingleOrDefault()
                : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
                
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

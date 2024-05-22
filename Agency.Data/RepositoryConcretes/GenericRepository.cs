using Agency.Core.Models;
using Agency.Core.RepositoryAbstracts;
using Agency.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.RepositoryConcretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public int Commit()
        {
            return _appDbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
          _appDbContext.Set<T>().Remove(entity);
        }

        public T Get(Func<T, bool>? func = null)
        {
            return func == null ?
                 _appDbContext.Set<T>().FirstOrDefault() :
                 _appDbContext.Set<T>().FirstOrDefault(func);
        }

        public List<T> GetAll(Func<T, bool>? func = null)
        {
            return func == null ?
                  _appDbContext.Set<T>().ToList() :
                  _appDbContext.Set<T>().Where (func).ToList ();
        }
    }
}

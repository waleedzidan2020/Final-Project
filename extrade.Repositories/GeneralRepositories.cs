using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Extrade.Repositories
{
    public class GeneralRepositories<T> where T : class
    {
        public readonly ExtradeContext DBContext;
        private DbSet<T> set;
        public GeneralRepositories(ExtradeContext _DBContext)
        {
            DBContext = _DBContext;
            set = DBContext.Set<T>();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, string? orderby = null,
            bool IsAsceding = false, int pageindex = 1, int pagesize = 20,params string[]include)
        {
            var query = set.AsQueryable();
            if (include !=null && include.Length>0)
                foreach(string i in include)
                    query = query.Include(i);
                if (orderby != null)
                query = query.OrderBy(orderby, IsAsceding);
            if(expression!=null)
                query = query.Where(expression);
            int rows=query.Count();
            if (pageindex <= 0)
                pageindex = 1;
            if (rows < pagesize)
                pageindex = 1;
            int rowscount = (pageindex - 1) * pagesize;
            query = query.Skip(rowscount).Take(pagesize);
            
            return query;
        }
        public T? GetbyID( Expression<Func<T, bool>>? expression = null)
        {
            var query = set.AsQueryable();
            if (expression !=null)
                query = query.Where(expression);
            return query.FirstOrDefault();
        }


        public IQueryable<T> GetList()
        {
          var query= set.AsQueryable();
            return query;
        }


        public T? GetByID(Expression<Func<T, bool>>? expression = null, int id = 0) {

            var query = set.AsQueryable();
            if (query != null)
            {
                if (expression != null)
                {

                    query = query.Where(expression);

                }


                return query.FirstOrDefault();
            }
            return null;


        }
        public T? GetByIDWithRole(Expression<Func<T, bool>>? expression = null, int id = 0)
        {

            var query = set.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query.FirstOrDefault();


        }
        public EntityEntry<T> Add(T entry) => set.Add(entry);
        public EntityEntry<T> Update(T entry) => set.Update(entry);
        public EntityEntry<T> Remove(T entry) => set.Remove(entry);






    }
}

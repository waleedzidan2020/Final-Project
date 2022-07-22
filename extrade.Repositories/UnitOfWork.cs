using extrade.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;

namespace Extrade.Repositories
{
    public class UnitOfWork
    {
        private readonly ExtradeContext dbContext;
        public UnitOfWork(ExtradeContext context)
        {
            dbContext = context;
        }
        public void Submit()
        {
            dbContext.SaveChanges();
        }

    }
}

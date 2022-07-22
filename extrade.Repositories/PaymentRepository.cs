using extrade.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.Repositories
{
    public class PaymentRepository : GeneralRepositories<AllPayment>
    {
        public PaymentRepository(ExtradeContext _DBContext) : base(_DBContext)
        {
        }
    }
}

using extrade.models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.Repositories
{
    public class DriverRepository : GeneralRepositories<Driver>
    {
        public DriverRepository(ExtradeContext _DBContext) : base(_DBContext)
        {
            
        }

        public List<Driver> Get()
        {
            return base.GetList().ToList();
        }
        public Driver GetDriverByID(int ID)
        {
            var filter = PredicateBuilder.New<Driver>();
            filter.Or(p => p.ID == ID);
            return base.GetbyID(filter);
           
        }
        public new Driver Add(Driver driver) => base.Add(driver).Entity;

        public new Driver Update(Driver driver)
        {
            var filter = PredicateBuilder.New<Driver>();
            filter=filter.Or(d=>d.ID==driver.ID);
            var result = GetByID(filter);
            result.Street = driver.Street;
            result.DriverLicense = driver.DriverLicense;
            result.City = driver.City;
            result.NameEn = driver.NameEn;
            result.Country=driver.Country;
            result.NameAr = driver.NameAr;
            result.PhoneDriver = driver.PhoneDriver;
            result.Order = driver.Order;

            return base.Update(result).Entity;
        }
        public Driver ChangeDriverStatus(int ID)
        {
            var filter = PredicateBuilder.New<Driver>();
            filter = filter.Or(p => p.ID == ID);
            var query = GetbyID(filter);
            if (query.DriverStatus == DriverStatus.block)
            {
                query.DriverStatus = DriverStatus.online;
            }
            else if (query.DriverStatus == DriverStatus.online)
                query.DriverStatus = DriverStatus.offline;

            else query.DriverStatus = DriverStatus.online;
            return base.Update(query).Entity;
        }
        public Driver Delete(int ID)
        {
            var filter = PredicateBuilder.New<Driver>();
            filter = filter.Or(p => p.ID == ID);
            var last = GetByID(filter);
            if (last.IsDeleted == false)
            {
                last.IsDeleted = true;
            }
            else last.IsDeleted = false;
            return base.Update(last).Entity;
        }
    }
}

using extrade.models;
using Extrade.ViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.Repositories
{
    public class RatingRepository : GeneralRepositories<Rating>
    {
        public RatingRepository(ExtradeContext _DBContext) : base(_DBContext)
        {
        }
        public List<RatingVIewModel> GetBestRating(int val = 4)
        {
            var result = base.GetList().Select(p => p.ProductID).Distinct().ToList();
            var finalresult = new List<RatingVIewModel>() ;
            for (int i = 0; i < result.Count(); i++)
            {
                var values = GetList().Where(p => p.ProductID == result[i]).Average(p => p.Value);
                finalresult.Add(new RatingVIewModel{ProductID = result[i],Value= values});
            }

            return finalresult.OrderBy(p => p.Value).Take(val).ToList();
        }

        public new Rating Add(Rating rating) => base.Add(rating).Entity;
        public new Rating Update(Rating rating) => base.Update(rating).Entity;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using Extrade.ViewModels;
using LinqKit;
using X.PagedList;

namespace Extrade.Repositories
{
    public class ProductRepository : GeneralRepositories<Product>
    {
        public ProductRepository(ExtradeContext context) : base(context)
        { }
        public PaginingViewModel<List<ProductViewModel>> GetProduct(
                        int ID = 0,
                        string? NameEn = null,
                        string? NameAr = null,
                        float Price = 0,
                        string? CategoryName = null,
                        DateTime? ModifiedDate = null,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 20
                        )
        {
            var Filtering = PredicateBuilder.New<Product>();
            var oldFiltering = Filtering;
            if (ID > 0)
                Filtering = Filtering.Or(p => p.ID == ID);
            if (!string.IsNullOrEmpty(NameEn))
                Filtering = Filtering.Or(p => p.NameEn.Contains(NameEn));
            if (!string.IsNullOrEmpty(NameAr))
                Filtering = Filtering.Or(p => p.NameAr.Contains(NameAr));
            if (Price > 0)
                Filtering = Filtering.Or(p => p.Price == Price);
            if (CategoryName != null)
                Filtering = Filtering.Or(p => p.Category.NameEn.Contains(CategoryName));
            if (ModifiedDate != null)
                Filtering = Filtering.Or(p => p.ModifiedDate <= ModifiedDate.Value);
            if (oldFiltering == Filtering)
                Filtering = null;
            var query = Get(Filtering, OrderBy, IsAscending, PageIndex, PageSize, null);

            var Result =
                query.Select(p => new ProductViewModel()
                {
                    ID = p.ID,
                    NameEn = p.NameEn,
                    NameAr = p.NameAr,
                    Description = p.Description,
                    Category = p.Category.NameEn,
                    VendorName = p.Vendor.User.NameEn,
                    Rating = p.Ratings.Select(p => p.Value).Average()
                });

            PaginingViewModel<List<ProductViewModel>>
                FinalResult = new PaginingViewModel<List<ProductViewModel>>()
                {
                    PageIndex = PageIndex,
                    PageSize = PageSize,
                    Count = GetList().Count(),
                    Data = Result.ToList(),
                };

            return FinalResult;

        }
        public List<ProductViewModel> GetProductForVendor(
                        string? ID,
                        string? NameEn = "",
                        string? NameAr = "",
                        float Price = 0,
                        string? CategoryName = "",
                        DateTime? ModifiedDate = null,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 30
                        )
        {
            var Filtering = PredicateBuilder.New<Product>();
            var oldFiltering = Filtering;
            if(!string.IsNullOrEmpty(ID))
                Filtering = Filtering.Or(p => p.VendorID.Contains(ID));

            if (!string.IsNullOrEmpty(NameEn))
                Filtering = Filtering.Or(p => p.NameEn.Contains(NameEn));
            if (!string.IsNullOrEmpty(NameAr))
                Filtering = Filtering.Or(p => p.NameAr.Contains(NameAr));
            if (Price > 0)
                Filtering = Filtering.Or(p => p.Price == Price);
            if (CategoryName != null)
                Filtering = Filtering.Or(p => p.Category.NameEn.Contains(CategoryName));
            if (ModifiedDate != null)
                Filtering = Filtering.Or(p => p.ModifiedDate <= ModifiedDate.Value);
            if (oldFiltering == Filtering)
                Filtering = null;
            var query = Get(Filtering, OrderBy, IsAscending, PageIndex, PageSize, "Category", "Vendor");

            var Result =
                query.Select(p => new ProductViewModel()
                {
                    ID = p.ID,
                    CategoryID = 1,
                    NameEn = p.NameEn,
                    NameAr = p.NameAr,
                    Price = p.Price,
                    Quantity=p.Quantity,
                    Description = p.Description,
                    Category = p.Category.NameEn,
                    VendorName = p.Vendor.User.NameEn,
                    Status=p.Status,
                    IsDeleted = p.IsDeleted,
                    VendorID=p.Vendor.UserID
                    


                });

            return  Result.ToList();
        }
        public PaginingViewModel<List<ProductViewModel>> GetProductForUsers(
                        int categoryID=0,
                        string vendorID="",
                        string? NameEn = null,
                        string? NameAr = null,
                        float Price = 0,
                        string? CategoryName = null,
                        DateTime? ModifiedDate = null,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 30
                        )
        {
            var Filtering = PredicateBuilder.New<Product>();
            var oldFiltering = Filtering;

            if (!string.IsNullOrEmpty(vendorID))
                Filtering = Filtering.Or(p => p.VendorID == vendorID);
            if (!string.IsNullOrEmpty(NameEn))
                Filtering = Filtering.Or(p => p.NameEn.Contains(NameEn));
            if (!string.IsNullOrEmpty(NameAr))
                Filtering = Filtering.Or(p => p.NameAr.Contains(NameAr));
            if (Price > 0)
                Filtering = Filtering.Or(p => p.Price == Price);
            if (categoryID > 0)
                Filtering = Filtering.Or(p => p.CategoryID == categoryID);
            if (!string.IsNullOrEmpty(CategoryName))
                Filtering = Filtering.Or(p => p.Category.NameEn.Contains(CategoryName)|| p.Category.NameAr.Contains(CategoryName));
            if (ModifiedDate != null)
                Filtering = Filtering.Or(p => p.ModifiedDate <= ModifiedDate.Value);

            if (oldFiltering == Filtering)
                Filtering = null;

            var query = Get(Filtering, OrderBy, IsAscending, PageIndex, PageSize, "Category", "Vendor", "Ratings", "ProductImages");

            var Result =
                query
                .Where(p => p.Status == extrade.models.ProductStatus.accepted
                  && p.IsDeleted == false && p.Quantity > 0)
                .Select(p => new ProductViewModel()
                {
                    ID = p.ID,
                    NameEn = p.NameEn,
                    NameAr = p.NameAr,
                    Description = p.Description,
                    Category = (p.Category != null)?p.Category.NameEn:"not provided",
                    VendorName = (p.Vendor != null)?p.Vendor.User.NameEn : "not provided",
                    CategoryID = p.CategoryID,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    VendorID=p.VendorID,
                    //Images = (p.ProductImages != null)?p.ProductImages.Select(p=>p.image).ToList():new List<string> { "notProvide.jpg"},
                    //Rating = (p.Ratings != null)? p.Ratings.Select(p => p.Value).Average():0
                });

            PaginingViewModel<List<ProductViewModel>>
                FinalResult = new PaginingViewModel<List<ProductViewModel>>()
                {
                    PageIndex = PageIndex,
                    PageSize = PageSize,
                    Count = GetList().Count(),
                    Data = Result.ToList(),
                };

            return FinalResult;

        }
        public List<ProductViewModel> GetProductForCollection(List<int> Productid)
        {
            var prod = new List<ProductViewModel>();
            foreach(var i in Productid) 
            { 
                var product = GetProductByID(i);
                prod.Add(product);
            }
            
            return prod;
        }
        public IPagedList<ProductViewModel> Search(int PageIndex = 1, int PageSize = 6)
        =>
            GetList().Select(p => new ProductViewModel
            {
                ID = p.ID,
                NameEn = p.NameEn,
                NameAr = p.NameAr,
                Description= p.Description,
                Price = p.Price,
                Category = p.Category.NameEn,

            }).ToPagedList(PageIndex, PageSize);
        public ProductViewModel GetProductByID(int ID)
        {
            var filter = PredicateBuilder.New<Product>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);

            var query = base.GetByID(filter);
            return query.ToViewModel();
        }
        public Product GetProductModelByID(int ID)
        {
            var filter = PredicateBuilder.New<Product>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);

            var query = base.GetByID(filter);
            return query;
        }



        public int GetCategoryId(int ID)
        {
            var filter = PredicateBuilder.New<Product>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);

            var query = base.GetByID(filter);
            return query.CategoryID;
        }
        public ProductEditViewModel GetProduct(int ID)
        {
            var filter = PredicateBuilder.New<Product>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);

            var query = base.GetByID(filter);
            return query.ToEditModel();
        }


        public ProductViewModel Add(ProductEditViewModel obj) =>
             base.Add(obj.ToModel()).Entity.ToViewModel();
        
        public ProductViewModel Update(ProductEditViewModel obj)
        {
            var filter = PredicateBuilder.New<Product>();
            filter = filter.Or(c => c.ID == obj.ID);
            var query = base.GetByID(filter);
            query.NameEn= obj.NameEn;
            query.NameAr= obj.NameAr;
            query.Description= obj.Description;
            query.Price= obj.Price;
            query.Quantity = obj.Quantity;

            return base.Update(query).Entity.ToViewModel();
        }
        public ProductViewModel Delete(int ID)
        {
            var filter = PredicateBuilder.New<Product>();
            filter = filter.Or(p => p.ID == ID);
            var query = GetbyID(filter);
            if (query.IsDeleted == false)
            {
                query.IsDeleted = true;
            }
            else query.IsDeleted = false;

            return base.Update(query).Entity.ToViewModel();
        }
        public ProductViewModel ProductStatus(int ID)
        {
            var filter = PredicateBuilder.New<Product>();
            filter = filter.Or(p => p.ID == ID);
            var query = GetbyID(filter);
            if (query.Status == extrade.models.ProductStatus.pending)
            {
                query.Status = extrade.models.ProductStatus.accepted;
            }
            else if (query.Status == extrade.models.ProductStatus.rejected)
                query.Status = extrade.models.ProductStatus.accepted;

            else if (query.Status == extrade.models.ProductStatus.accepted)
                query.Status = extrade.models.ProductStatus.rejected;
            return base.Update(query).Entity.ToViewModel();
        }
        //public ProductViewModel RejectProduct(int ID)
        //{
        //    var filter = PredicateBuilder.New<Product>();
        //    filter = filter.Or(p => p.ID == ID);
        //    var query = GetbyID(filter);
        //    if (query.Status == extrade.models.ProductStatus.pending)
        //    {
        //        query.Status = extrade.models.ProductStatus.rejected;
        //    }
        //    else if (query.Status == extrade.models.ProductStatus.accepted)
        //        query.Status = extrade.models.ProductStatus.rejected;

        //    else query.Status = extrade.models.ProductStatus.accepted;
        //    return base.Update(query).Entity.ToViewModel();
        //}

    }   
}

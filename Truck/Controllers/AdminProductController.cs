using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Truck.Models;
using Truck.Repository;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Truck.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class AdminProductController : ControllerBase
    {
        private readonly TruckContext _context;
        private readonly IRepos _repos;
        private readonly IStorage _storage;

        public AdminProductController(TruckContext context, IRepos repos, IStorage storage)
        {
            _context = context;
            _repos = repos;
            _storage = storage;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<ProductCategoryMasterModel>>> CreateOrUpdateProductCategory(ProductCategoryMasterModel model)
        {
            try
            {
                var ProdCategory = await _context.Product_Category_Masters.Where(x => x.ProductCategory_ID == model.ProductCategory_ID).FirstOrDefaultAsync();

                Product_Category_Master category = (ProdCategory == null) ? new Product_Category_Master() : ProdCategory;
                model.Product_Type = model.Product_Type.TrimStart().TrimEnd();
                category.Product_Type = (string.IsNullOrEmpty(model.Product_Type)) ? category.Product_Type : model.Product_Type;
                //category.ProductCategory_ID = (model.ProductCategory_ID == 0) ? category.ProductCategory_ID : model.ProductCategory_ID;
                if (ProdCategory == null)
                {
                    _context.Product_Category_Masters.Add(category);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<ProductCategoryMasterModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductCategoryMasterModel> { code = 0, message = ex.Message, data = null };
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse<List<Select>>>> EProductCategoryMasterDDL()
        {
            var list = await _context.Product_Category_Masters.Select(x => new Select
            {
                name = x.Product_Type,
                value = x.ProductCategory_ID
            }).ToListAsync();
            return new ApiResponse<List<Select>> { code = 1, data = list };
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApiResponse<ProductModel>>> CreateOrUpdateProduct(ProductModel model)
        {
            try
            {
                var Products = await _context.Products.Where(x => x.productID == model.productID).FirstOrDefaultAsync();

                Product newproduct = (Products == null) ? new Product() : Products;
                model.Product_Name = model.Product_Name.TrimStart().TrimEnd();
                newproduct.Product_Name = (string.IsNullOrEmpty(model.Product_Name)) ? newproduct.Product_Name : model.Product_Name;
                newproduct.Product_Description = (string.IsNullOrEmpty(model.Product_Description)) ? newproduct.Product_Description : model.Product_Description;
                newproduct.Product_CompanyAddress = (string.IsNullOrEmpty(model.Product_CompanyAddress)) ? newproduct.Product_CompanyAddress : model.Product_CompanyAddress;
                newproduct.Product_CompanyContactNo = (string.IsNullOrEmpty(model.Product_CompanyContactNo)) ? newproduct.Product_CompanyContactNo : model.Product_CompanyContactNo;
                newproduct.MRP = (model.MRP == 0) ? newproduct.MRP : model.MRP;
                newproduct.PercentDiscount = (model.PercentDiscount == 0) ? newproduct.PercentDiscount : model.PercentDiscount;
                if (!string.IsNullOrEmpty(model.Photo_Path))
                {
                    if (newproduct.Photo_Path != "/images/profile-picture/default.png")
                    {
                        await _storage.DeleteIfExists(newproduct.Photo_Path);
                    }
                    newproduct.Photo_Path = await _storage.Save(model.Photo_Path, "/Products");
                }

                if (Products == null)
                {
                    _context.Products.Add(newproduct);
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<ProductModel> { code = 1, message = "Successfull" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductModel> { code = 0, message = ex.Message, data = null };
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> EProductById(int id)
        {
            return await _context.Products.Where(x => x.productID == id).Select(x => new ProductModel
            {
                productID = x.productID,
                Product_Name = x.Product_Name,
                Product_Description = x.Product_Description,
                Photo_Path = x.Photo_Path,
                MRP = x.MRP,
                PercentDiscount=x.PercentDiscount,
                Product_CompanyAddress = x.Product_CompanyAddress,
                Product_CompanyContactNo = x.Product_CompanyContactNo,

            }).ToListAsync();
        }

        /*  [HttpGet("[action]")]
          public async Task<ActionResult<IEnumerable<EcomOrdersListModel>>> EcomOrderList(int orderid)
          {
              return await _context.Ecom_Orders.Where(x => x.Order_ID == orderid).Select(x => new EcomOrdersListModel
              {
                  Order_ID = x.Order_ID,
                  Order_Status = x.OrderStatusNavigation.OrderStatus,
                  Order_SubTotal = x.Order_SubTotal,
                  Product_Discount = x.Product_Discount,
                  Order_Tax = x.Order_Tax,
                  Order_Shipping = x.Order_Shipping,
                  Order_Total = x.Order_Total,
                  Order_Promo = x.Order_Promo,
                  Order_Discount = x.Order_Discount,
                  Order_GrandTotal = x.Order_GrandTotal,
                  Order_Date = x.Order_Date,
                  FK_Razor_Order_Ids = x.FK_Razor_Order_Ids,
                  Payment_Status = x.Payment_Status,
                  Payment_Details = x.Payment_Details,
                  ShippingAddress = _context.Ecom_Shipping.Where(a => a.FK_AppUser_Id == x.FK_AppUser_Id && a.Shipment_ID == x.Fk_Shipping_id).Select(s => new EcomAddressDetails
                  {
                      FullName = s.FullName,
                      ShipingStatus = s.Shipment_Status,
                      ShippingAddress = s.Shipping_Address,
                      Email = s.Email_Address,
                      PhoneNo = s.PhoneNos,
                      City = s.City,
                      PostCode = s.PostCode,
                      Shipment_ID = s.Shipment_ID
                  }).FirstOrDefault(),
                  ecomitemDetails = _context.Ecom_OrderItems.Where(y => y.FK_Order_Id == x.Order_ID).Select(s => new EcomOrdersItems
                  {
                      FK_Product_Id = s.FK_Product_Id,
                      Order_Price = s.Order_Price,
                      Product_Discount = s.Product_Discount,
                      Order_Quantity = s.Order_Quantity,
                      Order_Tax = s.Order_Tax,
                      Order_Date = s.Order_Date,
                      PhotoPath = _context.Ecom_ProductPhotoPath.Where(w => w.FK_Product_Id == s.FK_Product_Id).Select(s => s.ProductPhotoPath).FirstOrDefault(),
                      ProductName = _context.Ecom_Products.Where(w => w.productID == s.FK_Product_Id).Select(s => s.name).FirstOrDefault()
                  }).ToList()
              }).ToListAsync();
          }

          [HttpGet("[action]")]
          public ActionResult<PagedResponse<EcomOrderListModel>> GetEcomOrderList(int pageIndex, int pageSize, int status, DateTime? startdate, DateTime? enddate, string sortstring, int brandid)
          {
              var datacount = 0;
              var list = _context.GetEcomOrderList(pageIndex, pageSize, status, startdate, enddate, sortstring, brandid);
              var count = _context.GetEcomOrderListCount(0, pageSize, status, startdate, enddate, sortstring, brandid);
              if (count == null)
              {
                  datacount = list.Count;
              }
              else
              {
                  datacount = count.count;
              }

              return new PagedResponse<EcomOrderListModel>(list, datacount, pageIndex, pageSize);
          }

          [HttpGet("[action]")]
          public async Task<ActionResult<ApiResponse<PagedResponse<TransactionModel>>>> GetAllTransactions(int brandid, string search, int pageNumber, int pageSize)
          {
              try
              {
                  var query = from s in _context.Ecom_Orders select s;
                  query = query.Where(x => x.FK_Brandid == brandid).Include(x => x.Fk_Shipping_).Include(x => x.FK_AppUser_).Include(x => x.OrderStatusNavigation).Include(x => x.FK_Brand);
                  if (!string.IsNullOrEmpty(search))
                  {
                      query = query.Where(x => x.OrderStatusNavigation.OrderStatus.Contains(search) || x.Order_ID.ToString().Contains(search) || x.FK_AppUser_.fullName.Contains(search) || x.FK_Brand.name.Contains(search));
                  }

                  query = query.OrderByDescending(x => x.Order_Date);

                  var newquery = query.Select(x => new TransactionModel
                  {
                      AppuserID = x.FK_AppUser_Id,
                      Appuser = x.FK_AppUser_,
                      brandID = x.FK_Brandid,
                      grandTotalAmount = x.Order_GrandTotal,
                      isCashOnDelivery = x.isCashOnDelivery,
                      orderDate = x.Order_Date,
                      orderID = x.Order_ID,
                      orderStatus = x.OrderStatusNavigation.OrderStatus,
                      paymentStatus = x.Payment_Status,
                      razorPayID = x.FK_RazorId,
                      razorPayOrderID = x.FK_Razor_Order_Ids,
                      shippingAddress = x.Fk_Shipping_
                  });

                  return new ApiResponse<PagedResponse<TransactionModel>> { code = 1, message = "success", data = await PagedResponse<TransactionModel>.CreateAsync(newquery.AsNoTracking(), pageNumber, pageSize) };
              }
              catch (Exception)
              {
                  return new ApiResponse<PagedResponse<TransactionModel>> { code = 0, data = null };
              }
          }

          */
    }
}

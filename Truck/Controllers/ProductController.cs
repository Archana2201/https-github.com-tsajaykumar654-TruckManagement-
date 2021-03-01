using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Microsoft.AspNetCore.Authorization;

namespace Truck.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly TruckContext _context;
        private readonly IRepos _repos;
        private readonly IStorage _storage;

        public ProductController(TruckContext context, IRepos repos, IStorage storage)
        {
            _context = context;
            _repos = repos;
            _storage = storage;
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


        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> EProductList()
        {
            return await _context.Products.Select(x => new ProductModel
            {
                productID = x.productID,
                Product_Name = x.Product_Name,
                Product_Description = x.Product_Description,
                Photo_Path = x.Photo_Path,
                MRP = x.MRP,
                PercentDiscount = x.PercentDiscount,
                Product_CompanyAddress = x.Product_CompanyAddress,
                Product_CompanyContactNo = x.Product_CompanyContactNo,

            }).ToListAsync();
        }


        /* [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> CancelOrder(CancelOrderModel form)
         {
             try
             {
                 var cancel = await _context.Ecom_Orders.Where(x => x.Order_ID == form.Order_ID).FirstOrDefaultAsync();

                 if (cancel != null)
                 {
                     cancel.OrderStatus = 6;
                     cancel.CancelReason = form.Reason;
                     _context.Update(cancel);
                     await _context.SaveChangesAsync();
                 }
                 else
                 {
                     _context.Ecom_Orders.Add(cancel);
                     await _context.SaveChangesAsync();
                 }
                 return new ApiResponse<int> { code = 1, data = cancel.Order_ID, message = "Success" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<int> { code = 0, data = 0 };
             }



         }

         [HttpGet("[action]/id")]
         public async Task<ActionResult<ApiResponse<SelectInvoice>>> EcomProductInvoice(int id)
         {
             //&& y.FK_Order_Id == id
             var list = await _context.Ecom_Invoice.Where(y => y.FK_AppUser_Id == _repos.UserID).Select(x => new SelectInvoice
             {
                 path = x.Invoice_Path
             }).FirstOrDefaultAsync();
             return new ApiResponse<SelectInvoice> { code = 1, data = list };
         }



         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> AddEcomShoppingCart(EcomAddtoCart form)
         {
             Ecom_ShoppingCart ecomaddcart = new Ecom_ShoppingCart();
             ecomaddcart.FK_AppUser_Id = _repos.UserID;
             ecomaddcart.FK_Product_Id = form.productID;
             //ecomaddcart.Price = form.Price;
             ecomaddcart.Price = _context.Ecom_Products.Where(x => x.productID == form.productID).Select(s => s.SP).FirstOrDefault();
             ecomaddcart.MRP = _context.Ecom_Products.Where(x => x.productID == form.productID).Select(s => s.MRP).FirstOrDefault();
             //ecomaddcart.MRP = form.MRP;
             ecomaddcart.Quantity = form.Quantity;
             ecomaddcart.Fk_Brand_Id = _repos.BrandID;
             ecomaddcart.status = 1;

             try
             {
                 var existingprod = await _context.Ecom_ShoppingCart.Where(x => x.FK_Product_Id == form.productID && x.Fk_Brand_Id == _repos.BrandID && x.status == 1 && x.FK_AppUser_Id == _repos.UserID).FirstOrDefaultAsync();

                 // var existingprod = await context.Ecom_ShoppingCart.Where(x => x.FK_Product_Id == form.productID).FirstOrDefaultAsync(); //&& x.Fk_Brand_Id == repos.BrandID);
                 if (existingprod != null)
                 {
                     existingprod.Quantity = existingprod.Quantity + form.Quantity;
                     _context.Update(existingprod);
                     await _context.SaveChangesAsync();
                 }
                 else
                 {
                     _context.Ecom_ShoppingCart.Add(ecomaddcart);
                     await _context.SaveChangesAsync();
                 }
                 return new ApiResponse<int> { code = 1, data = ecomaddcart.ShoppingCart_ID, message = "Success" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<int> { code = 0, data = 0 };
             }

         }


         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> UpdateEcomCart(EcomAddtoCart model)
         {
             try
             {
                 //&& x.Fk_Brand_Id == model._repos.BrandID
                 var comp = await _context.Ecom_ShoppingCart.Where(x => x.ShoppingCart_ID == model.ShoppingCart_ID && x.FK_AppUser_Id == _repos.UserID && x.FK_Product_Id == model.productID && x.Fk_Brand_Id == _repos.BrandID && x.status == 1).FirstOrDefaultAsync();
                 if (comp != null)
                 {
                     if (model.Quantity < 1)
                     {
                         _context.RemoveRange(comp);
                     }
                     else if (model.Quantity >= 1)
                     {
                         comp.Quantity = model.Quantity;
                         _context.Update(comp);
                     }
                 }

                 await _context.SaveChangesAsync();

                 return new ApiResponse<int> { code = 1, message = "Cart Updated Successfully" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<int>
                 {
                     code = 1,
                     message = ex.Message
                 };
             }

         }


         [HttpGet("[action]")]
         public async Task<ActionResult<IEnumerable<EcomCart>>> GetEcomCartDetails()
         {

             return await _context.Ecom_ShoppingCart.Where(x => x.FK_AppUser_Id == _repos.UserID && x.Fk_Brand_Id == _repos.BrandID && x.status == 1).Select(x => new EcomCart
             {
                 productID = x.FK_Product_Id,
                 ShoppingCart_ID = x.ShoppingCart_ID,
                 ProductName = x.FK_Product_.name,
                 Price = x.Price,
                 MRP = x.MRP,
                 Quantity = x.Quantity,
                 Order_Date = x.Order_Date,
                 PhotoPath = _context.Ecom_ProductPhotoPath.Where(y => y.FK_Product_Id == x.FK_Product_Id)
                 .Select(s => s.ProductPhotoPath).FirstOrDefault(),
                 PercentDiscount = _context.Ecom_Products.Where(x => x.productID == x.productID).Select(s => s.PercentDiscount).FirstOrDefault(),
                 isfreedelievry = x.FK_Product_.isfreedelievry,
                 GST = _context.GST.Where(w => w.GST_ID == x.FK_Product_.FK_GST).Select(s => s.GST_Value).FirstOrDefault(),
                 Shipping_Charge = _context.Ecom_Products.Where(w => w.productID == x.FK_Product_Id).Select(s => s.Shipping_Charge).FirstOrDefault(),

             }).ToListAsync();
         }

         [HttpDelete("[action]/{id}")]
         public async Task<ActionResult<ApiResponse<int>>> DeleteEcomShoppingCart(int id)
         {
             var cartid = await _context.Ecom_ShoppingCart.FindAsync(id);
             if (cartid != null)
             {
                 cartid.status = 0;
                 _context.Ecom_ShoppingCart.Update(cartid);
                 await _context.SaveChangesAsync();
             }
             return new ApiResponse<int> { code = 1 };
         }

         [HttpDelete("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> DeleteAllCart()
         {
             //var cartid = await _context.Ecom_ShoppingCart.Where(x=>x.FK_AppUser_Id ==_repos.UserID);
             var cartid = _context.Ecom_ShoppingCart.Where(x => x.FK_AppUser_Id == _repos.UserID);
             if (cartid != null)
             {
                 //_context.Ecom_ShoppingCart.Remove(cartid);
                 _context.Ecom_ShoppingCart.RemoveRange(_context.Ecom_ShoppingCart.Where(x => x.FK_AppUser_Id == _repos.UserID));

                 await _context.SaveChangesAsync();
             }
             return new ApiResponse<int> { code = 1 };
         }


         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> AddEcomWishList(EcomWishlist form)
         {
             Ecom_Wishlist ecomwishlist = new Ecom_Wishlist();
             ecomwishlist.FK_AppUser_Id = form.userID;
             ecomwishlist.FK_Product_Id = form.productID;
             ecomwishlist.Fk_Brand_Id = form.brandId;
             try
             {
                 var userid = await _context.Ecom_Wishlist.FindAsync(form.userID);
                 if (userid != null)
                 {
                     _context.Ecom_Wishlist.Add(ecomwishlist);
                     await _context.SaveChangesAsync();
                 }
                 return new ApiResponse<int> { code = 1, data = ecomwishlist.Wishlist_ID, message = "" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<int> { code = 0, data = 0 };
             }

         }

         [HttpDelete("[action]/{id}")]
         public async Task<ActionResult<ApiResponse<int>>> DeleteEcomWishList(int id)
         {
             var cartid = await _context.Ecom_Wishlist.FindAsync(id);
             if (cartid != null)
             {
                 _context.Ecom_Wishlist.Remove(cartid);
                 await _context.SaveChangesAsync();
             }
             return new ApiResponse<int> { code = 1 };
         }


         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<EcomOrders>>> EcomOrders(EcomOrderItemsViewModel model)
         {
             Ecom_Orders orders = new Ecom_Orders();
             try
             {
                 decimal? totalAmount = 0;
                 decimal? grandTotal = 0;
                 decimal? gst = 0;
                 decimal? shippingPrice = 0;
                 decimal? discount = 0;

                 int? freeDeliveryShipppingCharge = _context.Ecom_Brand_Appsettings
                 .Where(x => x.BrandID == _repos.BrandID).Select(s => s.Shipping_Charge).FirstOrDefault();

                 foreach (var models in model.ecomitemDetails)
                 {
                     var ecom_Products = _context.Ecom_Products.Where(x => x.productID == models.FK_Product_Id).FirstOrDefault();
                     totalAmount += ecom_Products.SP * models.Order_Quantity;
                     grandTotal += ecom_Products.MRP * models.Order_Quantity;
                     gst += (ecom_Products.SP - ((ecom_Products.SP * 100) / (100 + ecom_Products.FK_GST))) * models.Order_Quantity;
                     shippingPrice += ecom_Products.isfreedelievry == 0 ? ecom_Products.Shipping_Charge : 0;
                 }
                 if (freeDeliveryShipppingCharge != 0)
                 {
                     shippingPrice = freeDeliveryShipppingCharge <= totalAmount ? 0 : shippingPrice;
                 }
                 orders.FK_AppUser_Id = _repos.UserID;
                 orders.Order_Status = model.ecomorder.Order_Status;
                 orders.Order_SubTotal = model.ecomorder.Order_SubTotal;
                 orders.Order_Tax = gst;
                 orders.Order_Shipping = shippingPrice;
                 orders.Order_Total = totalAmount;
                 orders.Order_Promo = model.ecomorder.Order_Promo;
                 orders.Order_Discount = (grandTotal - totalAmount);
                 orders.Order_GrandTotal = grandTotal;
                 orders.Fk_Shipping_id = model.ecomorder.Fk_Shipping_id;
                 orders.Order_Date = DateTime.Now;
                 orders.FK_Brandid = _repos.BrandID;
                 orders.Order_Status = "ORDERED";
                 orders.OrderStatus = 7;
                 orders.isCashOnDelivery = false;

                 RazorpayClient client = new RazorpayClient("rzp_test_Uf4CUBEQ370rpF", "JMZS0T1THnHYXCJ8dqD3bYbC");

                 decimal? amount = model.ecomorder.Order_GrandTotal;

                 var options = new Dictionary<string, object>
                 {
                 { "amount", amount * 100 },
                 { "currency", "INR" },
                 { "receipt", "Receipt" },
                 // auto capture payments rather than manual capture
                 // razor pay recommended option
                 { "payment_capture", true }
                 };

                 var order = client.Order.Create(options);
                 var orderId = order["id"].ToString();
                 var orderJson = order.Attributes.ToString();

                 orders.FK_Razor_Order_Ids = orderId;
                 orders.Payment_Details = model.ecomorder.Payment_Details;
                 orders.Payment_Status = model.ecomorder.Payment_Status;

                 await _context.Ecom_Orders.AddAsync(orders);
                 await _context.SaveChangesAsync();
                 var ParentOrderId = await _context.Ecom_Orders.FindAsync(orders.Order_ID);


                 foreach (var models in model.ecomitemDetails)
                 {
                     var ecom_Products = _context.Ecom_Products.Where(x => x.productID == models.FK_Product_Id).FirstOrDefault();
                     Ecom_OrderItems ordersitems = new Ecom_OrderItems();
                     ordersitems.FK_Product_Id = models.FK_Product_Id;
                     ordersitems.FK_Order_Id = ParentOrderId.Order_ID;
                     ordersitems.Order_Price = ecom_Products.SP;
                     ordersitems.Product_Discount = (ecom_Products.MRP - ecom_Products.SP);
                     ordersitems.Order_Quantity = models.Order_Quantity;
                     ordersitems.Order_Tax = (ecom_Products.SP - ((ecom_Products.SP * 100) / (100 + ecom_Products.FK_GST)));
                     ordersitems.Order_Date = models.Order_Date = DateTime.Now;
                     await _context.Ecom_OrderItems.AddAsync(ordersitems);
                 }
                 await _context.SaveChangesAsync();

                 EcomOrders query = new EcomOrders();
                 query.Order_ID = orders.Order_ID;
                 query.Fk_Razor_OrderIds = orderId;
                 query.FK_AppUser_Id = _repos.UserID;
                 query.Order_Shipping = orders.Order_Shipping;
                 query.Order_Status = orders.Order_Status;
                 query.Order_SubTotal = orders.Order_SubTotal;
                 query.Order_Total = orders.Order_Total;
                 query.Order_Tax = orders.Order_Tax;
                 query.Payment_Details = orders.Payment_Details;
                 query.Payment_Status = orders.Payment_Status;
                 query.Fk_Shipping_id = orders.Fk_Shipping_id;
                 query.Order_Promo = orders.Order_Promo;
                 query.Order_Discount = orders.Order_Discount;
                 orders.Order_GrandTotal = orders.Order_GrandTotal;
                 orders.FK_RazorId = orders.FK_RazorId;
                 return new ApiResponse<EcomOrders> { code = 1, data = query, message = "Success" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<EcomOrders> { code = 1, message = ex.Message };
             }
         }


         [HttpGet("[action]")]
         public async Task<ActionResult<IEnumerable<EcomOrdersList>>> EcomOrderList()
         {
             return await _context.Ecom_Orders.Where(x => x.FK_AppUser_Id == _repos.UserID).Select(x => new EcomOrdersList
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
                 ShippingAddress = _context.Ecom_Shipping.Where(a => a.FK_AppUser_Id == _repos.UserID && a.Shipment_ID == x.Fk_Shipping_id).Select(s => new EcomAddressDetails
                 {
                     FullName = s.FullName,
                     ShipingStatus = s.Shipment_Status,
                     ShippingAddress = s.Shipping_Address,
                     Email = s.Email_Address,
                     PhoneNo = s.PhoneNos,
                     City = s.City,
                     PostCode = s.PostCode,
                     Shipment_ID = s.Shipment_ID,
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

         [HttpGet("[action]/id")]
         public async Task<ActionResult<EcomOrdersList>> EcomOrderyId(int id)
         {
             return await _context.Ecom_Orders.Where(x => x.FK_AppUser_Id == _repos.UserID && x.Order_ID == id).Select(x => new EcomOrdersList
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
                 ShippingAddress = _context.Ecom_Shipping.Where(a => a.FK_AppUser_Id == _repos.UserID && a.Shipment_ID == x.Fk_Shipping_id).Select(s => new EcomAddressDetails
                 {
                     FullName = s.FullName,
                     ShipingStatus = s.Shipment_Status,
                     ShippingAddress = s.Shipping_Address,
                     Email = s.Email_Address,
                     PhoneNo = s.PhoneNos,
                     City = s.City,
                     PostCode = s.PostCode,
                     Shipment_ID = s.Shipment_ID,
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
                     ProductName = _context.Ecom_Products.Where(w => w.productID == s.FK_Product_Id).Select(s => s.name).FirstOrDefault(),
                     PercentDiscount = _context.Ecom_Products.Where(w => w.productID == s.FK_Product_Id).Select(s => s.PercentDiscount).FirstOrDefault(),
                     isfreedelievry = _context.Ecom_Products.Where(w => w.productID == s.FK_Product_Id).Select(s => s.isfreedelievry).FirstOrDefault(),
                     Shipping_Charge = _context.Ecom_Products.Where(w => w.productID == s.FK_Product_Id).Select(s => s.Shipping_Charge).FirstOrDefault()

                 }).ToList()


             }).FirstOrDefaultAsync();
         }


         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> AddorUpdateEcomShippingAddress([FromForm] EcomShipping form)
         {
             //Ecom_Shipping ecomShipping = new Ecom_Shipping();
             Ecom_Shipping ecomShipping = (form.Shipment_ID > 0) ? await _context.Ecom_Shipping.Where(x => x.Shipment_ID == form.Shipment_ID).FirstOrDefaultAsync() : new Ecom_Shipping();

             ecomShipping.FK_Order_Id = form.FK_Order_Id;
             ecomShipping.FK_AppUser_Id = _repos.UserID;
             ecomShipping.Shipment_Status = "";
             ecomShipping.Created_Date = DateTime.Now;
             ecomShipping.FullName = form.FullName;
             ecomShipping.Shipping_Address = form.Shipping_Address;
             ecomShipping.Email_Address = form.Email_Address;
             ecomShipping.PhoneNos = form.PhoneNos;
             ecomShipping.City = form.City;
             ecomShipping.PostCode = form.PostCode;
             try
             {
                 if (form.Shipment_ID <= 0)
                     _context.Ecom_Shipping.Add(ecomShipping);

                 await _context.SaveChangesAsync();
                 return new ApiResponse<int> { code = 1, data = ecomShipping.Shipment_ID, message = "Success" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<int> { code = 0, data = 0 };
             }

         }

         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> AddEcomFavorate([FromForm] EcomFavorte form)
         {

             var query = await _context.Ecom_FavoriteListUserwise.Where(w => w.FK_AppUser_Id == _repos.UserID &&
             w.Fk_Brand_Id == _repos.BrandID
             && w.FK_Product_Id == form.FK_Product_Id).FirstOrDefaultAsync();
             if (query != null)
             {
                 _context.Ecom_FavoriteListUserwise.Remove(query);
                 _context.SaveChangesAsync();
                 return new ApiResponse<int> { code = 1, data = 1, message = "Removed From Favorite" };
             }
             else
             {
                 Ecom_FavoriteListUserwise ecomfavor = new Ecom_FavoriteListUserwise();
                 ecomfavor.FK_AppUser_Id = _repos.UserID;
                 ecomfavor.FK_Product_Id = form.FK_Product_Id;
                 ecomfavor.Fk_Brand_Id = _repos.BrandID;
                 try
                 {
                     _context.Ecom_FavoriteListUserwise.Add(ecomfavor);
                     await _context.SaveChangesAsync();
                     return new ApiResponse<int> { code = 1, data = ecomfavor.Favorate_ID, message = "Added To Favorite" };
                 }
                 catch (Exception ex)
                 {
                     return new ApiResponse<int> { code = 0, data = 0 };
                 }
             }

         }

         [HttpDelete("[action]/{id}")]
         public async Task<ActionResult<ApiResponse<int>>> DeleteShippingAddress(int id)
         {
             var add = await _context.Ecom_Shipping.FindAsync(id);
             if (add != null)
             {
                 _context.Ecom_Shipping.Remove(add);
                 await _context.SaveChangesAsync();
             }
             return new ApiResponse<int> { code = 1 };
         }

         [HttpGet("[action]")]
         public async Task<ActionResult<IEnumerable<EcomShipping>>> GetEcomShippingAddress()
         {
             return await _context.Ecom_Shipping.Where(x => x.FK_AppUser_Id == _repos.UserID).Select(x => new EcomShipping
             {
                 Shipment_ID = x.Shipment_ID,
                 FK_Order_Id = x.FK_Order_Id,
                 Shipment_Status = x.Shipment_Status,
                 FullName = x.FullName,
                 Shipping_Address = x.Shipping_Address,
                 Email_Address = x.Email_Address,
                 PhoneNos = x.PhoneNos,
                 Created_Date = x.Created_Date,
                 PostCode = x.PostCode,
                 City = x.City

             }).ToListAsync();
         }


         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> AddEcomPayment(EcomPayment form)
         {
             Ecom_Payment ecomPayment = new Ecom_Payment();
             ecomPayment.FK_Invoice_Id = form.FK_Invoice_Id;
             ecomPayment.FK_AppUser_Id = form.FK_AppUser_Id;
             ecomPayment.Payment_Method = form.Payment_Method;
             ecomPayment.Payment_Date = DateTime.Now;

             try
             {
                 _context.Ecom_Payment.Add(ecomPayment);
                 await _context.SaveChangesAsync();
                 return new ApiResponse<int> { code = 1, data = ecomPayment.Payment_ID, message = "" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<int> { code = 0, data = 0 };
             }

         }


         [HttpPost("[action]")]
         public async Task<ActionResult<ApiResponse<int>>> AddEcomInvoice(EcomInvoice form)
         {
             Ecom_Invoice ecomInvoice = new Ecom_Invoice();
             ecomInvoice.FK_AppUser_Id = form.FK_AppUser_Id;
             ecomInvoice.FK_Shipment_Id = form.FK_Shipment_Id;
             ecomInvoice.FK_Order_Id = form.FK_Order_Id;
             ecomInvoice.Invoice_Date = DateTime.Now;

             try
             {
                 _context.Ecom_Invoice.Add(ecomInvoice);
                 await _context.SaveChangesAsync();
                 return new ApiResponse<int> { code = 1, data = ecomInvoice.Invoice_Id, message = "Success" };
             }
             catch (Exception ex)
             {
                 return new ApiResponse<int> { code = 0, data = 0, message = "Error" };
             }

         }
         */
    }
}

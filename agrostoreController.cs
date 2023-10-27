using agrostoreAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace agrostoreAPI.Controllers
{
    public class agrostoreController : Controller
    {
        // GET: agrostore
        DB_Resources_Class Resources = new DB_Resources_Class();
        
        public async Task<ActionResult> CreateFertilizer([FromBody] DB_Resources_Class.FertilizerRequest datarequest)
        {
            return await Task.FromResult(Json(Resources.CreateFertilizer(datarequest), JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> CreateSeed([FromBody] DB_Resources_Class.SeedRequest datarequest)
        {
            return await Task.FromResult(Json(Resources.CreateSeed(datarequest), JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> CreateOrder([FromBody] DB_Resources_Class.OrderRequest datarequest)
        {
            return await Task.FromResult(Json(Resources.CreateOrder(datarequest), JsonRequestBehavior.AllowGet));
        }
        
        public async Task<ActionResult> CreateOrderDetails([FromBody] DB_Resources_Class.OrderDetailsRequest datarequest)
        {
            return await Task.FromResult(Json(Resources.CreateOrderDetails(datarequest), JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> GetOrderDetails([FromUri] string orderCode)
        {

            DB_Resources_Class.OrderDetailsData[] orderdetails = new DB_Resources_Class.OrderDetailsData[1000000];
            orderdetails = Resources.GetOrderDetails(orderCode);
            return await Task.FromResult(Json(orderdetails, JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> ListAllOrders()
        {

            DB_Resources_Class.OrderData[] orders = new DB_Resources_Class.OrderData[1000000];
            orders = Resources.ListAllOrders();
            return await Task.FromResult(Json(orders, JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> ListAllFertilizers()
        {

            DB_Resources_Class.FertilizerData[] fertilizers = new DB_Resources_Class.FertilizerData[1000000];
            fertilizers = Resources.ListAllFertilizers();
            return await Task.FromResult(Json(fertilizers, JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> ListAllSeeds()
        {
           
                DB_Resources_Class.SeedData[] seeds = new DB_Resources_Class.SeedData[1000000];
                seeds = Resources.ListAllSeeds();
                return await Task.FromResult(Json(seeds, JsonRequestBehavior.AllowGet));
           
        }
        public async Task<ActionResult> GetOrder([FromUri] string PhoneNumber)
        {

            DB_Resources_Class.OrderData[] orders = new DB_Resources_Class.OrderData[1000000];
            orders = Resources.GetOrder(PhoneNumber);
            return await Task.FromResult(Json(orders, JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> Update_Order_Status([FromBody] DB_Resources_Class.updateOrderRequest datarequest)
        {
            return await Task.FromResult(Json(Resources.Update_Order_Status(datarequest), JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> Delete_OrderDetail([FromBody] DB_Resources_Class.DeleteOrderDetailRequest datarequest)
        {
            return await Task.FromResult(Json(Resources.Delete_OrderDetail(datarequest), JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> DiscardOrder([FromBody] DB_Resources_Class.DeleteOrderDetailRequest datarequest)
        {
            return await Task.FromResult(Json(Resources.DiscardOrder(datarequest), JsonRequestBehavior.AllowGet));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace agrostoreAPI.Classes
{
    public class DB_Resources_Class
    {
        public static string con = ConfigurationManager.AppSettings["ConnString"];
        public SqlConnection conn = new SqlConnection(con);
        public struct DbResponse
        {
            public string Code { get; set; }
            public string Message { get; set; }
        }
        public struct OrderResponse
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public string OrderCode { get; set; }
        }
        public struct FertilizerRequest
        {
            public string Name { get; set; }
            public float QuantityLimit { get; set; }
            public float UnitPrice { get; set; }
        }
        public struct SeedRequest
        {
            public string Name { get; set; }
            public float QuantityLimit { get; set; }
            public float UnitPrice { get; set; }
            public string FertilizerCode { get; set; }
        }
        public struct OrderRequest
        {
            public string FarmerName { get; set; }
            public string FarmerPhone { get; set; }
        }
        public struct OrderDetailsRequest
        {
            public string OrderCode { get; set; }
            public string ItemType { get; set; }
            public string CodeItem { get; set; }
            public float LandSize { get; set; }
        }

        public struct FertilizerData
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string QuantityLimit { get; set; }
            public string UnitPrice { get; set; }
            public bool Available { get; set; }
        }
        public struct SeedData
        {
            public string Code { get; set; }
            public string Name { get; set; }
           
            public string QuantityLimit { get; set; }
            public string UnitPrice { get; set; }
            public string Fertilizer { get; set; }
            public bool Available { get; set; }
        }
        public struct OrderData
        {
            public string OrderCode { get; set; }
            public string FarmerName { get; set; }
            public string FarmerPhone { get; set; }
            public string OrderDate { get; set; }
            public string OrderStatus { get; set; }
        }
        public struct OrderDetailsData
        {
            public int IdRecord { get; set; }

            public string ItemName { get; set; }
            public string CodeItem { get; set; }
            public string ItemType { get; set; }
            public string LandSize { get; set; }
            public string Quantity { get; set; }
            public string UnitPrice { get; set; }
            public string SubTotal { get; set; }

        }
        public struct updateOrderRequest
        {
            public string status { get; set; }
            public string OrderCode { get; set; }
        }
        public struct DeleteOrderDetailRequest
        {
            public int Idrecord { get; set; }
            public string OrderCode { get; set; }
        }
        public DbResponse CreateFertilizer(FertilizerRequest data)
        {
            DbResponse result = new DbResponse();
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();

            SqlCommand com = new SqlCommand("AGROSTORE.dbo.CREATE_FERTILIZER", conn);
            com.Parameters.AddWithValue("@FertilizerName", data.Name.Replace(",", " ").Replace("'", "`"));
            com.Parameters.AddWithValue("@QuantityLimit", data.QuantityLimit);
            com.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {

                da.Fill(dt);
                result.Code = dt.Rows[0].ItemArray.GetValue(0).ToString();
                result.Message = dt.Rows[0].ItemArray.GetValue(1).ToString();
            }
            catch (Exception)
            {
                result.Code = "400";
                result.Message = "Bad request";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

           

            return result;
        }
        public DbResponse CreateSeed(SeedRequest data)
        {
            DbResponse result = new DbResponse();
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();

            SqlCommand com = new SqlCommand("AGROSTORE.dbo.CREATE_SEED", conn);
            com.Parameters.AddWithValue("@SeedName", data.Name.Replace(", ", " ").Replace("'", "`"));
            com.Parameters.AddWithValue("@FertilizerCode", data.FertilizerCode);
            com.Parameters.AddWithValue("@QuantityLimit", data.QuantityLimit);
            com.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {

                da.Fill(dt);
                result.Code = dt.Rows[0].ItemArray.GetValue(0).ToString();
                result.Message = dt.Rows[0].ItemArray.GetValue(1).ToString();
            }
            catch (Exception)
            {
                result.Code = "400";
                result.Message = "Bad request";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
        public OrderResponse CreateOrder(OrderRequest data)
        {
            OrderResponse result = new OrderResponse();
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();

            SqlCommand com = new SqlCommand("AGROSTORE.dbo.CREATE_ORDER", conn);
            com.Parameters.AddWithValue("@FarmerName", data.FarmerName.Replace(", ", " ").Replace("'", "`"));
            com.Parameters.AddWithValue("@FarmerPhone", data.FarmerPhone);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {

                da.Fill(dt);
                result.Code = dt.Rows[0].ItemArray.GetValue(0).ToString();
                result.Message = dt.Rows[0].ItemArray.GetValue(1).ToString();
                result.OrderCode = dt.Rows[0].ItemArray.GetValue(2).ToString();
            }
            catch (Exception)
            {
                result.Code = "400";
                result.Message = "Bad request";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
        public DbResponse CreateOrderDetails(OrderDetailsRequest data)
        {
            DbResponse result = new DbResponse();
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();

            SqlCommand com = new SqlCommand("AGROSTORE.dbo.CREATE_ORDER_DETAILS", conn);
            com.Parameters.AddWithValue("@OrderCode", data.OrderCode);
            com.Parameters.AddWithValue("@ItemType", data.ItemType);
            com.Parameters.AddWithValue("@CodeItem", data.CodeItem);
            com.Parameters.AddWithValue("@LandSize", data.LandSize);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {

                da.Fill(dt);
                result.Code = dt.Rows[0].ItemArray.GetValue(0).ToString();
                result.Message = dt.Rows[0].ItemArray.GetValue(1).ToString();
            }
            catch (Exception)
            {
                result.Code = "400";
                result.Message = "Bad request";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
        public DbResponse Update_Order_Status(updateOrderRequest data)
        {
            DbResponse result = new DbResponse();
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();

            SqlCommand com = new SqlCommand("AGROSTORE.dbo.Update_Order_Status", conn);
            com.Parameters.AddWithValue("@Status", data.status);
            com.Parameters.AddWithValue("@OrderCode", data.OrderCode);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {

                da.Fill(dt);
                result.Code = dt.Rows[0].ItemArray.GetValue(0).ToString();
                result.Message = dt.Rows[0].ItemArray.GetValue(1).ToString();
            }
            catch (Exception)
            {
                result.Code = "400";
                result.Message = "Bad request";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
        public DbResponse Delete_OrderDetail(DeleteOrderDetailRequest data)
        {
            DbResponse result = new DbResponse();
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();

            SqlCommand com = new SqlCommand("AGROSTORE.dbo.Delete_OrderDetail", conn);
            com.Parameters.AddWithValue("@idRecord", data.Idrecord);
            com.Parameters.AddWithValue("@OrderCode", data.OrderCode);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {

                da.Fill(dt);
                result.Code = dt.Rows[0].ItemArray.GetValue(0).ToString();
                result.Message = dt.Rows[0].ItemArray.GetValue(1).ToString();
            }
            catch (Exception)
            {
                result.Code = "400";
                result.Message = "Bad request";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
        public DbResponse DiscardOrder(DeleteOrderDetailRequest data)
        {
            DbResponse result = new DbResponse();
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();

            SqlCommand com = new SqlCommand("AGROSTORE.dbo.DISCARD_ORDER", conn);
            com.Parameters.AddWithValue("@OrderCode", data.OrderCode);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {

                da.Fill(dt);
                result.Code = dt.Rows[0].ItemArray.GetValue(0).ToString();
                result.Message = dt.Rows[0].ItemArray.GetValue(1).ToString();
            }
            catch (Exception)
            {
                result.Code = "400";
                result.Message = "Bad request";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
        public FertilizerData[] ListAllFertilizers()
        {
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();
            string cmdList = "SELECT [FertilizerCode],[FertilizerName] ,convert(nvarchar(50),[QuantityLimit])+' '+[MeasurementUnit],[UnitPrice],[Available] FROM [AGROSTORE].[dbo].[Fertilizer_Register]  order by FertilizerName";
            SqlCommand sqlList = new SqlCommand(cmdList, conn);
            SqlDataAdapter da = new SqlDataAdapter(sqlList);
            try
            {
                //sqlList.ExecuteNonQuery();
                da.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            FertilizerData[] DataProduct = new FertilizerData[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataProduct[i].Code = dt.Rows[i].ItemArray.GetValue(0).ToString();
                DataProduct[i].Name = dt.Rows[i].ItemArray.GetValue(1).ToString();
                DataProduct[i].QuantityLimit = dt.Rows[i].ItemArray.GetValue(2).ToString();
                DataProduct[i].UnitPrice = dt.Rows[i].ItemArray.GetValue(3).ToString();
                DataProduct[i].Available = bool.Parse(dt.Rows[i].ItemArray.GetValue(4).ToString());

            }
            return DataProduct;
        }
        public SeedData[] ListAllSeeds()
        {
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();
            string cmdList = "SELECT * FROM [AGROSTORE].[dbo].[View_Seed_Details]  order by SeedName";
            SqlCommand sqlList = new SqlCommand(cmdList, conn);
            SqlDataAdapter da = new SqlDataAdapter(sqlList);
            try
            {
                //sqlList.ExecuteNonQuery();
                da.Fill(dt);
            }
            //catch (Exception)
            //{
            //    //throw;
            //}
            catch (HttpException ex)
            {
                ex.ToString();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            SeedData[] DataProduct = new SeedData[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataProduct[i].Code = dt.Rows[i].ItemArray.GetValue(0).ToString();
                DataProduct[i].Name = dt.Rows[i].ItemArray.GetValue(1).ToString();
               
                DataProduct[i].QuantityLimit = dt.Rows[i].ItemArray.GetValue(2).ToString();
                DataProduct[i].UnitPrice = dt.Rows[i].ItemArray.GetValue(3).ToString();
                DataProduct[i].Fertilizer = dt.Rows[i].ItemArray.GetValue(4).ToString();
                DataProduct[i].Available = bool.Parse(dt.Rows[i].ItemArray.GetValue(5).ToString());

            }
           
            return DataProduct;
        }
        public OrderData[] ListAllOrders()
        {
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();
            string cmdList = "SELECT [OrderCode],[FarmerName],[FarmerPhone],format([RecordDate],'dd/MM/yyyy'),[OrderStatus]  FROM [dbo].[Order_Register]  order by IdOrder desc";
            SqlCommand sqlList = new SqlCommand(cmdList, conn);
            SqlDataAdapter da = new SqlDataAdapter(sqlList);
            try
            {
                //sqlList.ExecuteNonQuery();
                da.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            OrderData[] DataProduct = new OrderData[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataProduct[i].OrderCode = dt.Rows[i].ItemArray.GetValue(0).ToString();
                DataProduct[i].FarmerName = dt.Rows[i].ItemArray.GetValue(1).ToString();
                DataProduct[i].FarmerPhone = dt.Rows[i].ItemArray.GetValue(2).ToString();
                DataProduct[i].OrderDate = dt.Rows[i].ItemArray.GetValue(3).ToString();
                DataProduct[i].OrderStatus = dt.Rows[i].ItemArray.GetValue(4).ToString();
                

            }
            return DataProduct;
        }
        public OrderDetailsData[] GetOrderDetails(string orderCode)
        {
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();
            string cmdList = "SELECT IdRecord, ItemName, CodeItem, ItemType, CONVERT(nvarchar(50), LandSize) + ' ' + LandMeasurementUnit AS [Land Size], Quantity, UnitPrice, SubTotal FROM      [AGROSTORE].dbo.Order_Details where OrderCode='" + orderCode + "' order by ItemName";
            SqlCommand sqlList = new SqlCommand(cmdList, conn);
            SqlDataAdapter da = new SqlDataAdapter(sqlList);
            try
            {
                //sqlList.ExecuteNonQuery();
                da.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            OrderDetailsData[] DataProduct = new OrderDetailsData[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataProduct[i].IdRecord = int.Parse(dt.Rows[i].ItemArray.GetValue(0).ToString());
                DataProduct[i].ItemName = dt.Rows[i].ItemArray.GetValue(1).ToString();
                DataProduct[i].CodeItem = dt.Rows[i].ItemArray.GetValue(2).ToString();
                DataProduct[i].ItemType = dt.Rows[i].ItemArray.GetValue(3).ToString();
                DataProduct[i].LandSize = dt.Rows[i].ItemArray.GetValue(4).ToString();
                DataProduct[i].Quantity = dt.Rows[i].ItemArray.GetValue(5).ToString();
                DataProduct[i].UnitPrice = dt.Rows[i].ItemArray.GetValue(6).ToString();
                DataProduct[i].SubTotal = dt.Rows[i].ItemArray.GetValue(7).ToString();


            }
            return DataProduct;
        }
        public OrderData[] GetOrder(string PhoneNumber)
        {
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();
            string cmdList = "SELECT [OrderCode],[FarmerName],[FarmerPhone],format([RecordDate],'dd/MM/yyyy'),[OrderStatus]  FROM [dbo].[Order_Register] where [FarmerPhone]='"+PhoneNumber+ "' and [OrderStatus]='PENDING'  order by IdOrder desc";
            SqlCommand sqlList = new SqlCommand(cmdList, conn);
            SqlDataAdapter da = new SqlDataAdapter(sqlList);
            try
            {
                //sqlList.ExecuteNonQuery();
                da.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            OrderData[] DataProduct = new OrderData[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataProduct[i].OrderCode = dt.Rows[i].ItemArray.GetValue(0).ToString();
                DataProduct[i].FarmerName = dt.Rows[i].ItemArray.GetValue(1).ToString();
                DataProduct[i].FarmerPhone = dt.Rows[i].ItemArray.GetValue(2).ToString();
                DataProduct[i].OrderDate = dt.Rows[i].ItemArray.GetValue(3).ToString();
                DataProduct[i].OrderStatus = dt.Rows[i].ItemArray.GetValue(4).ToString();


            }
            return DataProduct;
        }
    }
}
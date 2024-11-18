using Forjob.IRepository;
using Forjob.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Forjob.Repository
{
    public class Product : IProduct
    {
        private IConfiguration configuration;

        public Product(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public List<UserModel> UserDropDown()
        {
            List<UserModel> userList = new List<UserModel>();
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                SqlCommand command1 = connection1.CreateCommand();
                command1.CommandType = System.Data.CommandType.StoredProcedure;
                command1.CommandText = "SP_SELECT_ALL_User";
                SqlDataReader reader1 = command1.ExecuteReader();
                DataTable dataTable1 = new DataTable();
                dataTable1.Load(reader1);
                foreach (DataRow use in dataTable1.Rows)
                {
                    UserModel userModel = new UserModel();
                    userModel.UserID = Convert.ToInt32(use["UserID"]);
                    userModel.UserName = use["UserName"].ToString();
                    userList.Add(userModel);
                }
                //ViewBag.User = userList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return userList;
        }

        public DataTable ProductList()
        {
                DataTable table = new DataTable();
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SELECT_ALL_PRODUCT";
                SqlDataReader reader = command.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return table;
        }

        public void ProductSave(ProductModel productModel)
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (productModel.ProductID == null)
                {
                    command.CommandText = "SP_INSERT_PRODUCT";
                }
                else
                {
                    command.CommandText = "SP_UPDATE_PRODUCT";
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productModel.ProductID;
                }
                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productModel.ProductName;
                command.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = productModel.ProductCode;
                command.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = productModel.ProductPrice;
                command.Parameters.Add("@Description", SqlDbType.VarChar).Value = productModel.Description;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = productModel.UserID;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            
        }

        public ProductModel AddEdit(int? productID)
        {
            ProductModel productModel = new ProductModel();
            try
            {

                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SELECT_PRODUCT_BY_ID";
                command.Parameters.AddWithValue("@ProductID", productID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                foreach (DataRow dataRow in table.Rows)
                {
                    productModel.ProductID = Convert.ToInt32(@dataRow["ProductID"]);
                    productModel.ProductName = @dataRow["ProductName"].ToString();
                    productModel.ProductCode = @dataRow["ProductCode"].ToString();
                    productModel.ProductPrice = Convert.ToDouble(@dataRow["ProductPrice"]);
                    productModel.Description = @dataRow["Description"].ToString();
                    productModel.UserID = Convert.ToInt32(@dataRow["UserID"]);
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex);

            }
            return productModel;
        }

        
    }
}

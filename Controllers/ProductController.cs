using Forjob.IRepository;
using Forjob.Models;
using Forjob.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Forjob.Controllers
{
    public class ProductController : Controller
    {
        private IConfiguration configuration;
        private readonly IProduct product;

        public ProductController(IConfiguration configuration, IProduct product)
        {
            this.configuration = configuration;
            this.product = product;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region User Dropdown

        public void UserDropDown()
        {
            //string connectionString = configuration.GetConnectionString("ConnectionString");
            //SqlConnection connection1 = new SqlConnection(connectionString);
            //connection1.Open();
            //SqlCommand command1 = connection1.CreateCommand();
            //command1.CommandType = System.Data.CommandType.StoredProcedure;
            //command1.CommandText = "SP_SELECT_ALL_User";
            //SqlDataReader reader1 = command1.ExecuteReader();
            //DataTable dataTable1 = new DataTable();
            //dataTable1.Load(reader1);
            //List<UserModel> userList = new List<UserModel>();
            //foreach (DataRow use in dataTable1.Rows)
            //{
            //    UserModel userModel = new UserModel();
            //    userModel.UserID = Convert.ToInt32(use["UserID"]);
            //    userModel.UserName = use["UserName"].ToString();
            //    userList.Add(userModel);
            //}
            ViewBag.User = product.UserDropDown();
        }
        #endregion


        public IActionResult ProductList()
        {
            //string connectionString = this.configuration.GetConnectionString("ConnectionString");
            //SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();
            //SqlCommand command = connection.CreateCommand();
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "SP_SELECT_ALL_PRODUCT";
            //SqlDataReader reader = command.ExecuteReader();
            //DataTable table = new DataTable();
            //table.Load(reader);


            return View(product.ProductList());

        }

        public IActionResult AddEdit(int? productID)
        {

            UserDropDown();
            if (productID == null)
            {
                return View();
            }
            else
            {
                return View("AddEdit", product.AddEdit(productID));
            }
        }

       public IActionResult ProductSave(ProductModel productModel)
        {
            product.ProductSave(productModel);

                TempData["SuccessMessage"] = productModel.ProductID == null ?
                    "Product added successfully!" :
                    "Product updated successfully!";
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteProduct(int productID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_DELETE_PRODUCT";
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("ProductList");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // SQL Server error code for foreign key constraint violation
                {
                    TempData["ErrorMessage"] = "Unable to delete the product because it is referenced in another record.";
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while deleting the product. Please try again.";
                }
                Console.WriteLine(ex.ToString());
                return RedirectToAction("ProductList");
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = "Product deleted successfully!";
                Console.WriteLine(ex.ToString());
                return RedirectToAction("ProductList");
            }
        }
    }
}

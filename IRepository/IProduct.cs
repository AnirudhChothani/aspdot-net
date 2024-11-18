using Forjob.Models;
using System.Data;

namespace Forjob.IRepository
{
    public interface IProduct
    {
        List<UserModel> UserDropDown();
        DataTable ProductList();

        void  ProductSave(ProductModel productModel);

        ProductModel AddEdit(int? productID);
       

    }


}

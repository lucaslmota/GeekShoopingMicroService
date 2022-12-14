using GeekShoopin.Web.Models;

namespace GeekShoopin.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAll();
        Task<ProductModel> FindById(int id);
        Task<ProductModel> Create(ProductModel product);
        Task<ProductModel> Update(ProductModel product);
        Task<bool> Delete(int id);
    }
}

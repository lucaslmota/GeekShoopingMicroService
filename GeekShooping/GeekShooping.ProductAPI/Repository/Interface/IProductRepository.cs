using GeekShooping.ProductAPI.Data.ValueObjects;

namespace GeekShooping.ProductAPI.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindById(int id);
        Task<ProductVO> Create(ProductVO product);
        Task<ProductVO> Update(ProductVO product);
        Task<bool> Delete(int id);
    }
}

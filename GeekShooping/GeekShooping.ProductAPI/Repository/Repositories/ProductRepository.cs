using AutoMapper;
using GeekShooping.ProductAPI.Config;
using GeekShooping.ProductAPI.Data.ValueObjects;
using GeekShooping.ProductAPI.Model.Context;
using GeekShooping.ProductAPI.Model.Entities;
using GeekShooping.ProductAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.ProductAPI.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLServerContext _sQLServerContext;
        private readonly IMapper _mapper;
        public ProductRepository(SQLServerContext sQLServerContext, IMapper mapper)
        {
            _sQLServerContext = sQLServerContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _sQLServerContext.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }
        public async Task<ProductVO> FindById(int id)
        {
            var product = await _sQLServerContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Create(ProductVO product)
        {
            Product createProduct = _mapper.Map<Product>(product);
            _sQLServerContext.Products.Add(createProduct);
            await _sQLServerContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(createProduct);
        }
        public async Task<ProductVO> Update(ProductVO productId)
        {
            Product upadteProduct = _mapper.Map<Product>(productId);
            _sQLServerContext.Products.Update(upadteProduct);
            await _sQLServerContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(upadteProduct);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Product product =
                await _sQLServerContext.Products.Where(p => p.Id == id)
                    .FirstOrDefaultAsync() ?? new Product();
                if (product.Id <= 0) return false;
                _sQLServerContext.Products.Remove(product);
                await _sQLServerContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

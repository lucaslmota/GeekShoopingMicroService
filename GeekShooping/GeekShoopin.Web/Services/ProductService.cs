using GeekShoopin.Web.Models;
using GeekShoopin.Web.Services.IServices;
using GeekShoopin.Web.Utils;

namespace GeekShoopin.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public const string BasePath = "api/v1/Product";
        
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient)); ;
        }
        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }
        public async Task<ProductModel> FindById(int id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();

        }
        public async Task<ProductModel> Create(ProductModel product)
        {
            var response = await _httpClient.PostAsJson(BasePath, product);
            if(response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductModel>();
            }
            else
            {
               throw new Exception("Something went wrong when calling API");
            }
        }
        public async Task<ProductModel> Update(ProductModel product)
        {
            var response = await _httpClient.PutAsJson(BasePath, product);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductModel>();
            }
            else
            {
                throw new Exception("Something went wrong when calling API");
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
            if(response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }else
            {
                throw new Exception("Something went wrong when calling API");
            }
        }
    }
}

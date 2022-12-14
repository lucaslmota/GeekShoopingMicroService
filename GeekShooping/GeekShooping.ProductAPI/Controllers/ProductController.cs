﻿using GeekShooping.ProductAPI.Data.ValueObjects;
using GeekShooping.ProductAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> GetAllProducts()
        {
            var product = await _productRepository.FindAll();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> GetById(int id)
        {
            var product = await _productRepository.FindById(id);
            if (product.Id <= 0) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> CriateProduct(ProductVO product)
        {
            if (product != null)
            {
                var createProduct = await _productRepository.Create(product);
                return Ok(createProduct);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> UpdateProduct(ProductVO product)
        {
            if(product != null)
            {
                var updateProduct = await _productRepository.Update(product);
                return Ok(updateProduct);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleteProduct = await _productRepository.Delete(id);
            if(!deleteProduct) return BadRequest();
            return Ok(deleteProduct);
        }
    }
}

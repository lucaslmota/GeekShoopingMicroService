using GeekShoopin.Web.Models;
using GeekShoopin.Web.Services.IServices;
using GeekShoopin.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoopin.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAll();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if(ModelState.IsValid)
            {
                var response = await _productService.Create(productModel);
                if(response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _productService.FindById(id);
            if(model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if(ModelState.IsValid) 
            {
                var response = await _productService.Update(productModel);
                if(response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productModel);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _productService.FindById(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var response = await _productService.Delete(model.Id);
            if (response) return RedirectToAction(
                    nameof(ProductIndex));
            return View(model);
        }
    }
}

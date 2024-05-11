using DataAccess;
using DataHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo.Services;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using static System.Net.WebRequestMethods;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Azure;
using DataAccess.Migrations;

namespace PlantEShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IUploadImageService _uploadImageService;
        private readonly ICategoryService _categoryService;


        public ProductController(IProductService service, IUploadImageService uploadImageService, ICategoryService categoryService)
        {
            _service = service;
            _uploadImageService = uploadImageService;
            _categoryService = categoryService;

        }
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync();
            return View(allProducts);
        }

        public async Task<IActionResult> ManagementIndex()
        {
            var allProducts = await _service.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            return View(allProducts);
        }

        //Get: Product/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProductImage,ProductName,ProductDescription,ProductPrice,ProductStock,CategoryId")] Product product, IFormFile ImageUpload)
        {
            if (ModelState.IsValid)
            {

                if (ImageUpload != null && ImageUpload.Length > 0)
                {
                    // Upload image using the UploadImageService
                    string containerName = "codenbugsproductsimg";
                    string imgUrl = await _uploadImageService.UploadImageAsync(ImageUpload, containerName);
                    // Set the product image URL
                    product.ProductImage = imgUrl;


                }

            }
            else
            {
                return View(product);
            }



            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManagementDetails(int id)
        {
            var ProductDetails = await _service.GetByIdAsync(id);

            var prodWithCats = await _categoryService.GetByIdAsync(ProductDetails.CategoryId);
            ViewBag.Category = prodWithCats.CategoryName;

            if (ProductDetails == null) return View("NotFound");
            return View(ProductDetails);
        }

        //Get: Product/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName");

            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ProductImage,ProductName,ProductDescription,ProductPrice,ProductStock,CategoryId")] Product product, IFormFile ImageUpload)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName");
                return View(product);
            }

            if (ImageUpload != null && ImageUpload.Length > 0)
            {
                // Upload image using the UploadImageService
                string containerName = "codenbugsproductsimg";
                string imgUrl = await _uploadImageService.UploadImageAsync(ImageUpload, containerName);
                // Set the product image URL
                product.ProductImage = imgUrl;
            }


            await _service.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));
        }


        //Get: Product/Delete/1

        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);

            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            bool imageDeleted = await _uploadImageService.DeleteImageAsync(productDetails.ProductImage, "codenbugsproductsimg");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var ProductDetails = await _service.GetByIdAsync(id);

            var prodWithCats = await _categoryService.GetByIdAsync(ProductDetails.CategoryId);
            ViewBag.Category = prodWithCats.CategoryName;

            if (ProductDetails == null) return View("NotFound");
            return View(ProductDetails);
        }

        public async Task<IActionResult> ProductsByCategory(int id)
        {
            var productsInCategory = await _service.GetByCategoryAsync(id);
            return View(productsInCategory);


        }

    }
}

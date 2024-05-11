using DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using DataAccess.Migrations;

namespace Repo.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;
        public ProductService(AppDbContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        public async Task AddAsync(Product product)
        {
         
           await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(n => n.ProductId == id);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await _context.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(n => n.ProductId == id);


            return result;
        }

        public async Task<Product> UpdateAsync(int id, Product newProduct)
        {
            var existingProduct = await GetByIdAsync(id);

            
            existingProduct.ProductName = newProduct.ProductName;
            existingProduct.ProductImage = newProduct.ProductImage;
            existingProduct.ProductDescription = newProduct.ProductDescription;
            existingProduct.ProductPrice = newProduct.ProductPrice;
            existingProduct.ProductStock = newProduct.ProductStock;
            existingProduct.CategoryId = newProduct.CategoryId;


            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int id)
        {
            var result = await _context.Products
                .Where(n => n.CategoryId == id)
                .ToListAsync();

            return result;
            
        }
        


    }
}

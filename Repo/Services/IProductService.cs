using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHub.Models;


namespace Repo.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task<Product> UpdateAsync(int id, Product newProduct);
       
        Task DeleteAsync(int id);

        Task<IEnumerable<Product>> GetByCategoryAsync(int id);
    }
}

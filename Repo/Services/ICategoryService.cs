using DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int id);

        Task AddAsync(Category category);

        Task<Category> UpdateAsync(int id, Category newCategory);

        Task DeleteAsync(int id);
    }
}

using DataHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repo.Services;

namespace PlantEShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var allCategories = await _categoryService.GetAllAsync();
            return View(allCategories);
        }

        //Get: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryName")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("NotFound");
               
            };
           
            await _categoryService.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManagementIndex()
        {
            var allCategories = await _categoryService.GetAllAsync();
            return View(allCategories);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            
            if (categories == null) return View("NotFound");
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryName")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("NotFound");

            };

            await _categoryService.UpdateAsync(id, category);
            return RedirectToAction(nameof(ManagementIndex));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null) return View("NotFound");
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return View("NotFound");

            

            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(ManagementIndex));
        }
    }
}

using DataHub.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}

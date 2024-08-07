﻿using DataHub.Models;
using DataHub.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repo.Services;

namespace PlantEShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [AllowAnonymous]
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

        public async Task<IActionResult> ProductByCategory(int id)
        {
            var result = await _productService.GetByCategoryAsync(id);

            return View("Index", result);
        }
    }
}

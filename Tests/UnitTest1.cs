using Xunit;
using Moq;
using DataHub.Models;
using Repo.Services;
using PlantEShop.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlantEShop.Tests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<IProductService> _mockProductService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockProductService = new Mock<IProductService>();
            _controller = new CategoryController(_mockCategoryService.Object, _mockProductService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfCategories()
        {
            // Arrange
            var categories = new List<Category> { new Category { CategoryName = "TestCategory" } };
            _mockCategoryService.Setup(service => service.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var category = new Category { CategoryName = "NewCategory" };

            // Act
            var result = await _controller.Create(category);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_Post_ReturnsNotFoundView_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("CategoryName", "Required");

            // Act
            var result = await _controller.Create(new Category());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NotFound", viewResult.ViewName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithCategory()
        {
            // Arrange
            var category = new Category { CategoryName = "TestCategory" };
            _mockCategoryService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(category);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Category>(viewResult.Model);
            Assert.Equal("TestCategory", model.CategoryName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFoundView_WhenCategoryIsNull()
        {
            // Arrange
            _mockCategoryService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((Category)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NotFound", viewResult.ViewName);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithCategory()
        {
            // Arrange
            var category = new Category { CategoryName = "TestCategory" };
            _mockCategoryService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(category);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Category>(viewResult.Model);
            Assert.Equal("TestCategory", model.CategoryName);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundView_WhenCategoryIsNull()
        {
            // Arrange
            _mockCategoryService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((Category)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NotFound", viewResult.ViewName);
        }
    }
}

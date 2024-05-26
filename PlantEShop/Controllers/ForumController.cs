using DataAccess;
using DataHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Repo.Services;
using System.Security.Claims;

namespace PlantEShop.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IForumService _service;
        private readonly IUploadImageService _uploadImageService;

        public ForumController(IForumService service, IUploadImageService uploadImageService)
        {
            _service = service;
            _uploadImageService = uploadImageService;

        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allPosts = await _service.GetAllAsync();
            return View(allPosts);
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,PostImage")] Post post, IFormFile ImageUpload)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            post.UserId = userId;

            if (ModelState.IsValid)
            {

                if (ImageUpload != null && ImageUpload.Length > 0)
                {
                    // Upload image using the UploadImageService
                    string containerName = "codenbugsproductsimg";
                    string imgUrl = await _uploadImageService.UploadImageAsync(ImageUpload, containerName);
                    // Set the product image URL
                    post.PostImage = imgUrl;


                }

            }
            else
            {
                return View(post);
            }


            await _service.AddAsync(post);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            
            var PostDetails = await _service.GetByIdAsync(id);
          

            if (PostDetails == null) return View("NotFound");
            return View(PostDetails);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddComment([Bind("Content")] Comment comment, int postId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            comment.UserId = userId;
            comment.CreatedAt = DateTime.Now;
            comment.PostId = postId;

            await _service.AddCommentAsync(comment, postId);

            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var PostDetails = await _service.GetByIdAsync(postId);

            return View("Details", PostDetails);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var postDetails = await _service.GetByIdAsync(id);
            if (postDetails == null) return View("NotFound");

            bool imageDeleted = await _uploadImageService.DeleteImageAsync(postDetails.PostImage, "codenbugsproductsimg");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

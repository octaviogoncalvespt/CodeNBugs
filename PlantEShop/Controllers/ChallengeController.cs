using DataHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repo.Services;
using System.Security.Claims;

namespace PlantEShop.Controllers
{
    [Authorize]
    public class ChallengeController : Controller
    {
        private readonly IChallengeService _challengeService;
        private readonly IUploadImageService _uploadImageService;

        public ChallengeController(IChallengeService challengeService, IUploadImageService uploadImageService)
        {
            _challengeService = challengeService;
            _uploadImageService = uploadImageService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var challenge = await _challengeService.GetActiveChallengeAsync();
            if (challenge == null)
            {
                return View("NotFound");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var attemptsLeft = userId == null ? 3 : await _challengeService.GetRemainingAttemptsAsync(userId, challenge.Id);

            return View(challenge);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitGuess([Bind("Guess")] UserGuess userGuess, int challengeId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userGuess.UserId = userId;
            userGuess.ChallengeId = challengeId;
            userGuess.AttemptedAt = DateTime.Now;
            userGuess.IsCorrect = false;


            await _challengeService.AddGuessAsync(userGuess, challengeId);

            var challenge = await _challengeService.GetActiveChallengeAsync();

            userGuess.IsCorrect = string.Equals(userGuess.Guess, challenge.CorrectAnswer, StringComparison.OrdinalIgnoreCase);

            

            userGuess.AttemptsLeft = await _challengeService.GetRemainingAttemptsAsync(userId, challenge.ChallengeId);
            
           model.IsCorrect = guess.IsCorrect;

            if (model.IsCorrect)
            {
                model.DiscountCode = challenge.DiscountCode;
            }

            return View("Index", model);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Challenge challenge, IFormFile ImageUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImageUpload != null && ImageUpload.Length > 0)
                {
                    // Upload image using the UploadImageService
                    string containerName = "codenbugsproductsimg";
                    string imgUrl = await _uploadImageService.UploadImageAsync(ImageUpload, containerName);
                    // Set the product image URL
                    challenge.ChallengeImage = imgUrl;


                }
                else
                {
                    return View(challenge);
                }
            }

            await _challengeService.AddChallengeAsync(challenge);
            return RedirectToAction(nameof(Index));
        }
    }
}


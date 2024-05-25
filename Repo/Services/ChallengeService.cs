using DataAccess;
using DataHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Services
{
    public class ChallengeService
    {
        private readonly AppDbContext _context;

        public ChallengeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Challenge> GetActiveChallengeAsync()
        {
            var today = DateTime.Today;
            return await _context.Challenges.FirstOrDefaultAsync(c => c.ActiveDate.Date == today);
        }

        public async Task AddChallengeAsync(Challenge challenge)
        {
            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();
        }

        public async Task AddGuessAsync(UserGuess guess, int challengeId)
        {
            _context.UserGuesses.Add(guess);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetRemainingAttemptsAsync(string userId, int challengeId)
        {
            var attempts = await _context.UserGuesses.CountAsync(g => g.UserId == userId && g.ChallengeId == challengeId);
            return 3 - attempts;
        }
    }
}
}

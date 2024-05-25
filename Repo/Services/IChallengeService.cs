using DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Services
{
    public interface IChallengeService
    {
        Task<Challenge> GetActiveChallengeAsync();
        Task AddChallengeAsync(Challenge challenge);
        Task AddGuessAsync(UserGuess guess, int challengeId);
        Task<int> GetRemainingAttemptsAsync(string userId, int challengeId);
    }
}

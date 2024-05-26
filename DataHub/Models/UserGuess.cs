using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHub.Models
{
    public class UserGuess
    {
        public int Id { get; set; }
        public string Guess { get; set; }
        public DateTime AttemptedAt { get; set; }
        public bool IsCorrect { get; set; }

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public int ChallengeId { get; set; }
        [ForeignKey(nameof(ChallengeId))]

        public Challenge Challenge { get; set; }
    }
}

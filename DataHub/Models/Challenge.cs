using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHub.Models
{
    public class Challenge
    {
        public int ChallengeId { get; set; }
        public string? ChallengeImage { get; set; }
        public string CorrectAnswer { get; set; }
        public string DiscountCode { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime ActiveDate { get; set; }

        public string? ImgURL(string account, string container)
        {
            string result = "https://" + account + ".blob.core.windows.net/" + container + "/" + this.ChallengeImage;
            return result;
        }
    }
}

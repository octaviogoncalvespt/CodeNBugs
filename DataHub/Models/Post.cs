using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataHub.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name ="Image")]
        public string? PostImage { get; set; }


        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public string? ImgURL(string account, string container)
        {
            string result = "https://" + account + ".blob.core.windows.net/" + container + "/" + this.PostImage;
            return result;
        }
    }
}

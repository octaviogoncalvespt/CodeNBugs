using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHub.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}

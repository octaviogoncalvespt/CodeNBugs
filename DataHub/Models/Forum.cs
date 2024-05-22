using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHub.Models
{
    public class Forum
    {
        public int Id { get; set; }

        public string Post { get; set; }



        public List<Post> Posts { get; set; }
    }
}

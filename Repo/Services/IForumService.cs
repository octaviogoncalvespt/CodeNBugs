using DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Services
{
    public interface IForumService
    {
        Task<IEnumerable<Post>> GetAllAsync();

        Task<Post> GetByIdAsync(int id);

        Task AddAsync(Post post);

        Task<Post> UpdateAsync(int id, Post newPost);

        Task DeleteAsync(int id);

        Task<Post> AddCommentAsync(Comment comment, int postId);
    }
}

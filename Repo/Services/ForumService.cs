using DataAccess;
using DataHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Services
{
    public class ForumService : IForumService
    {
        private readonly AppDbContext _context;

        public ForumService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Post post)
        {

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> AddCommentAsync(Comment comment, int postId)
        {
            var result = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == postId);
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Posts.FirstOrDefaultAsync(n => n.Id == id);
            _context.Posts.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var result = await _context.Posts.Include(p => p.User).ToListAsync();
            return result;
        }

        public async Task<Post> GetByIdAsync(int id)
        {

            var result = await _context.Posts.Include(p => p.Comments).ThenInclude(c => c.User).Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);


            return result;
        }

        public async Task<Post> UpdateAsync(int id, Post newPost)
        {
            var existingPost = await GetByIdAsync(id);


            existingPost.Title = newPost.Title;
            existingPost.PostImage = newPost.PostImage;
            existingPost.Description = newPost.Description;



            await _context.SaveChangesAsync();

            return existingPost;
        }
    }
}

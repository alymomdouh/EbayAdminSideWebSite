namespace EbayAdminRepository.Comments
{
    using EbayAdminDbContext;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentRepository : ICommentRepository
    {
        private readonly myDbContext _context;
        public CommentRepository(myDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddCommentAsync(Comment Comment)
        {
            await _context.Comments.AddAsync(Comment);

            await _context.SaveChangesAsync();


            return Comment.UserId;
        }

        public async Task<int> DeleteCommentAsync(Comment Comment)
        {
            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();
            return Comment.UserId;
        }

        public async Task<Comment> GetCommentDetailsAsync(int value)
        {
            return await _context.Comments.Where(c => c.UserId == value).FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> GetCommentAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<int> UpdateCommentAsync(Comment Comment)
        {
            _context.Update(Comment);
            await _context.SaveChangesAsync();
            return Comment.UserId;
        }

      
    }
}

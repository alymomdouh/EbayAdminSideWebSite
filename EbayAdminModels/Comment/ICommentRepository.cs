namespace Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentRepository
    {
        Task<int> AddCommentAsync(Comment Comment);
        Task<List<Comment>> GetCommentAsync(int id);
        Task<Comment> GetCommentDetailsAsync(int value);
        Task<int> UpdateCommentAsync(Comment Comment);
        Task<int> DeleteCommentAsync(Comment Comment);
    }
}

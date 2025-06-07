using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IBlogPostService
    {
        Task<PagedResult<BlogPost>> GetBlogPostsAsync(BlogPostFilterRequest filter);
        Task<BlogPost> GetBlogPostByIdAsync(int postId);
        Task<BlogPost> CreateBlogPostAsync(CreateBlogPostRequest request);
        Task<BlogPost> UpdateBlogPostAsync(int postId, UpdateBlogPostRequest request);
        Task<bool> DeleteBlogPostAsync(int postId);
        Task<bool> PublishBlogPostAsync(int postId);
        Task<bool> UnpublishBlogPostAsync(int postId);
        Task<bool> IncrementViewCountAsync(int postId);
        Task<IEnumerable<BlogPost>> GetFeaturedPostsAsync(int schoolId, int count = 5);
        Task<IEnumerable<BlogPost>> GetRecentPostsAsync(int schoolId, int count = 10);
    }
}

using Management.Common;
using Management.Common.Configuration;
using Management.Model.BusinessObjects.BusinessObjects.BlogDTO;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.Entities.BlogsEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Management.Services.Blogs
{
    public class PostServices : IPostServices
    {
        private readonly ConnectionStringConfig _connectionStringConfig;
        private readonly UserManager<ApplicationUser> _userManager;

        DbContextOptionsBuilder<BlogDbContext> optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
        public PostServices(ConnectionStringConfig connectionStringConfig, UserManager<ApplicationUser> userManager)
        {
            _connectionStringConfig = connectionStringConfig;
            optionsBuilder.UseSqlServer(_connectionStringConfig.DefaultConnection);
            _userManager = userManager; 
        }
        public async Task<ServiceResponse<PostDTO>> CreatePost(PostDTO postDTO, ClaimsPrincipal user)
        {
            try
            {
                if (postDTO == null)
                {
                    return ServiceResponse<PostDTO>.Error("Post creation failed, information is not provided.");
                }
                if(String.IsNullOrEmpty(postDTO.content) && string.IsNullOrWhiteSpace(postDTO.content))
                {
                    return ServiceResponse<PostDTO>.Error("Content can not be null.");
                }

                using (var context = new BlogDbContext(optionsBuilder.Options))
                {
                    var post = new Post
                    {
                        Title = postDTO.title,
                        Content = postDTO.content,
                        IsActive = true,
                        IsVisible = true,
                        PublishOn = postDTO.create_date,
                        LastModifiedDate = Utilities.GetDate(),
                        UserId = new Guid(_userManager.GetUserId(user))
                    };
                    await context.AddAsync(post);

                    foreach (var categoryId in postDTO.category)
                    {
                        PostCategory postCategory = new PostCategory()
                        {
                            Post = post,
                            CategoryId = categoryId,
                            PostId = post.Id
                        };
                        await context.AddAsync(postCategory);
                    }

                    foreach (var tagId in postDTO.tag)
                    {
                        PostTag postTag = new PostTag()
                        {
                            Post = post,  
                            PostId= post.Id,
                            TagId = tagId
                        };
                        await context.AddAsync(postTag);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return ServiceResponse<PostDTO>.Error("Post Create failed.");
            }
            return ServiceResponse<PostDTO>.Success("Post Create Successfully.");
        }
    }
    public interface IPostServices
    {
        Task<ServiceResponse<PostDTO>> CreatePost(PostDTO postDTO, ClaimsPrincipal user);
    }
}
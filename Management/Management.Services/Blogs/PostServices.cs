using Management.Common;
using Management.Common.Configuration;
using Management.Model.BlogModel;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.Entities.BlogsEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Management.Services.Blogs
{
    public class PostServices : IPostServices
    {
        private readonly ConnectionStringConfig _connectionStringConfig;
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        public PostServices(ConnectionStringConfig connectionStringConfig)
        {
            _connectionStringConfig = connectionStringConfig;
            optionsBuilder.UseSqlServer(_connectionStringConfig.DefaultConnection);
        }
        public async Task<ServiceResponse<PostDTO>> CreatePost(PostDTO postDTO, string userId)
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

                using(var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    foreach(var i in postDTO.tag)
                    {
                        var tag = new PostTag
                        {
                            Tag = new Tag
                            {
                                Title = i.Title,
                            }
                        };
                    }

                    var post = new Post
                    {
                        Title = postDTO.title,
                        Content = postDTO.content,
                        //PostCategories = new List<PostCategory> { new Post { } }
                        PostTags = new List<PostTag> { new PostTag { Tag = new Tag { Title = postDTO.tag} } },
                        IsActive = true,
                        IsVisible = true,
                        PublishOn = postDTO.create_date,
                        LastModifiedDate = Utilities.GetDate(),
                        UserId = new Guid(userId)

                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
           

            throw new NotImplementedException();
        }
    }
    public interface IPostServices
    {
        Task<ServiceResponse<PostDTO>> CreatePost(PostDTO postDTO, string userId);
    }
}

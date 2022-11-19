using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.Entities.BlogsEntities
{
    public class BlogEntity
    {
    }
    public class BlogBaseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PublishOn { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class Post : BlogBaseModel
    {
        public string? Title { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Like>? Likes { get; set; }
        public List<PostTag>? PostTags { get; set; }
        public List<PostCategory>? PostCategories { get; set; }
    }
    public class Category : BlogBaseModel
    {
        public string? Title { get; set; }
        public List<PostCategory>? PostCategories { get; set; }
    }
    public class Tag : BlogBaseModel
    {
        public string? Title { get; set; }
        public List<PostTag>? PostTags { get; set; }
    }
    public class Comment : BlogBaseModel
    {
        public Guid UserId { get; set; }
        public string? CommentContent { get; set; }
        public int Totalcomment { get; set; }
        public int PostId { get; set; }
        public Comment? Parent { get; set; }
        public Post? Post { get; set; }
    }
    public class Like : BlogBaseModel
    {
        public Guid UserId { get; set; }
        public int TotalLike { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
    public class PostTag
    {
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
    public class PostCategory
    {
        public int PostId { get; set; }
        public Post? Post { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

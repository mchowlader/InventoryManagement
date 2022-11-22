
namespace Management.Model.BusinessObjects.BusinessObjects.BlogDTO
{
    public class PostDTO
    {
        public string? title { get; set; }
        public string? content { get; set; }
        public DateTime create_date { get; set; }
        public List<int>? tag { get; set; }
        public List<int>? category { get; set; }
    }
}
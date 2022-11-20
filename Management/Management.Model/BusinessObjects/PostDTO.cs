using Management.Model.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.BlogModel
{
    public class PostDTO
    {
        public string? title { get; set; }
        public string? content { get; set; }
        public DateTime create_date { get; set; }
        public List<Tag>? tag { get; set; }
        public List<Category>? category { get; set; }
    }
}
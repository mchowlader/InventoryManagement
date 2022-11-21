using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.BusinessObjects
{
    public class BlogBusinessModel
    {

    }

    public class Tag
    {
        public string? Title { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Title { get; set; }
    }
}

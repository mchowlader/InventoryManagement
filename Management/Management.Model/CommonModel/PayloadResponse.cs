using Management.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.CommonModel
{
    public class PayloadResponse<TEntity> where TEntity: class
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PayloadResponse() 
        {
            _httpContextAccessor = new HttpContextAccessor();
            this.RequestURL = _httpContextAccessor.HttpContext != null ? $"{_httpContextAccessor.HttpContext.Request.Scheme} : //{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}{_httpContextAccessor.HttpContext.Request.Path}" : "";
            this.ResponseTime = Utilities.GetRequestResponseTime(); 
        }
        public bool Success { get; set; }
        public string? RequestTime { get; set; }
        public string? ResponseTime { get; set; }
        public string? RequestURL { get; set; }
        public List<string> Message { get; set; }
        public TEntity Payload { get; set; }
        public string? PayloadType { get; set; }
    }
}
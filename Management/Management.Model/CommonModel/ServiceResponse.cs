using Management.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.CommonModel
{
    public class ServiceResponse<TEntity> where TEntity : class
    {
        public TEntity? data { get; set; }
        public List<string>? message { get; set; }
        public bool success { get; set; }
        public static ServiceResponse<TEntity> Error(string? message = null)
        {
            if(message.IsNullOrEmpty())
            {
                if (message.Contains("Return the transaction."))
                {
                    message = "Something went wrong. Please try again";
                }
            }
            return new ServiceResponse<TEntity>
            {
                data = null,
                message = new List<string> { message ?? "There was a problem handling the request."} ,
                success = false
            };
        }

        public static ServiceResponse<TEntity> Success(string? message = null, TEntity data = null)
        {
            return new ServiceResponse<TEntity>
            {
                data = data,
                message = new List<string> { message ?? "Request successful." },
                success= true
            };
        }
    }
}
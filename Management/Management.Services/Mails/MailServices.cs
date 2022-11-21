using Management.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Services.Mails
{
    public class MailServices : IMailServices
    {
        public Task SendEmailConrifmMailAsync(MailConfiguration mailRequest)
        {
            throw new NotImplementedException();
        }
    }
    public interface IMailServices
    {
        Task SendEmailConrifmMailAsync(MailConfiguration mailRequest);
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmails.Services
{
    public interface IMailingService
    {
        Task SendEmailAsync(
            string mailTo, 
            string subject, 
            string body,
            IList<IFormFile> attachments = null); /*عشان اعرف الاند يوزر محتاج ي اتاتش فايل مع الايميل ولا لا ؟ فاعملت اوبشنال براميتر*/

    }
}

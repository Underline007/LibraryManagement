using LibraryManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(SendMailRequest mailRequest);
    }
}

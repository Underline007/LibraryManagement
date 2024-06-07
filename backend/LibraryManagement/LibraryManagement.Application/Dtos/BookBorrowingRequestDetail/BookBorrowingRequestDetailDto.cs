using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.BookBorrowingRequestDetail
{
   
        public class BookBorrowingRequestDetailDto
        {
            public Guid BookId { get; set; }
            public string BookTitle { get; set; } = string.Empty;
        }
}

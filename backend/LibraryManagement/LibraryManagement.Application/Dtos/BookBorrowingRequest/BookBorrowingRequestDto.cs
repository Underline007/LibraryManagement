using LibraryManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.BookBorrowingRequest
{
    public class BookBorrowingRequestDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime BookBorrowingReturnDate { get; set; }
        public BorrowingRequestStatus Status { get; set; }
        public Guid RequestorId { get; set; }
        public Guid? ApproverId { get; set; }
    }
}

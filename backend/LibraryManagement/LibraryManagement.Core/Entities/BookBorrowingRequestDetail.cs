using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    public class BookBorrowingRequestDetails
    {
        public Guid BookBorrowingRequestId { get; set; }
        public virtual BookBorrowingRequest? BookBorrowingRequest { get; set; }

        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}

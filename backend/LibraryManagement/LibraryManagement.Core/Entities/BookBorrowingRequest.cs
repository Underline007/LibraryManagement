using LibraryManagement.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    public class BookBorrowingRequest
    {
        public Guid Id { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public DateTime BookBorrowingReturnDate { get; set; }

        public BorrowingRequestStatus Status { get; set; } = BorrowingRequestStatus.Waitting;

        public Guid? RequestorId { get; set; }
        public virtual User? Requestor { get; set; }

        public Guid? ApproverId { get; set; }
        public virtual User? Approver { get; set; }

        public virtual ICollection<BookBorrowingRequestDetails>? BookBorrowingRequestDetails { get; set; }
    }
}

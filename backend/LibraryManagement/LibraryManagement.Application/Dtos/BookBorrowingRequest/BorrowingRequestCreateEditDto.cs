﻿using LibraryManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.BookBorrowingRequest
{
    public class BorrowingRequestCreateEditDto
    {

        [Required(ErrorMessage = "RequestorId is required")]
        public Guid RequestorId { get; set; }

        [Required(ErrorMessage = "ApproverId is required")]
        public Guid ApproverId { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public DateTime BookBorrowingReturnDate { get; set; } 

        public BorrowingRequestStatus Status { get; set; } = BorrowingRequestStatus.Waitting;

        [Required(ErrorMessage = "Book list is required")]
        [MaxLength(5, ErrorMessage = "Maximum 5 books allowed")]
        public List<Guid>? BookIds { get; set; }
    }
}
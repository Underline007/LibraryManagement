﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Author { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<BookBorrowingRequestDetail>? BookBorrowingRequestDetails { get; set; }
    }
}
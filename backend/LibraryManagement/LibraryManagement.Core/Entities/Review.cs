using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = string.Empty;

        public DateTime DateReview { get; set; }

        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }

        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}

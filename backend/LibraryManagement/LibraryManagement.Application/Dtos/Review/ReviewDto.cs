using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.Review
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public DateTime DateReview { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
    }
}

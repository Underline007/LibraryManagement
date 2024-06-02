using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.Rating
{
    public class RatingCreateEditDto
    {

        [Range(1, 5, ErrorMessage = "Score must be between 1 and 5")]
        public int Score { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }
    }
}

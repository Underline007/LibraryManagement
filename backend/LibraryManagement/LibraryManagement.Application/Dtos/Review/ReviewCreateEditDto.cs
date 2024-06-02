using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.Review
{
    public class ReviewCreateEditDto
    {

        [Required(ErrorMessage = "Content is required")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "BookId is required")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }


    }
}

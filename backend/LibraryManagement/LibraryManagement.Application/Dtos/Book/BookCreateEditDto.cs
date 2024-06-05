using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.Book
{
    public class CreateEditBookDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters.")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }
        public IFormFile? Image { get; set; }

    }
}

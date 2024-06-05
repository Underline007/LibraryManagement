using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.User
{
    public class UserCreateEditDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        public IFormFile? Image { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
    }
}

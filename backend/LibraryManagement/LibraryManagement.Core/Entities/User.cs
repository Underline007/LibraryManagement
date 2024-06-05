using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Avatar { get; set; } = string.Empty;

        public UserRole Role {  get; set; } = UserRole.User;
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
        public virtual ICollection<BookBorrowingRequest>? BookBorrowingRequests { get; set; }
    }
}

using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Dtos.Rating
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
    }
}

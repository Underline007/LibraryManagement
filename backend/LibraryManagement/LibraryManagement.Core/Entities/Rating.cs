using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }

        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}

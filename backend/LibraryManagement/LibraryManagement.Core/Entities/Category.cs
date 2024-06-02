using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Book>? Books { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Identity;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
    public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Books)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId).
            OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Ratings)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);

        modelBuilder.Entity<BookBorrowingRequest>()
            .HasMany(bbr => bbr.BookBorrowingRequestDetails)
            .WithOne(brd => brd.BookBorrowingRequest)
            .HasForeignKey(brd => brd.BookBorrowingRequestId);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.BookBorrowingRequestDetails)
            .WithOne(brd => brd.Book)
            .HasForeignKey(brd => brd.BookId);

        modelBuilder.Entity<BookBorrowingRequest>()
            .HasOne(bbr => bbr.Requestor)
            .WithMany(u => u.BookBorrowingRequests)
            .HasForeignKey(bbr => bbr.RequestorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BookBorrowingRequest>()
            .HasOne(bbr => bbr.Approver)
            .WithMany()
            .HasForeignKey(bbr => bbr.ApproverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BookBorrowingRequestDetails>()
            .HasKey(brd => new { brd.BookBorrowingRequestId, brd.BookId });
    }
}

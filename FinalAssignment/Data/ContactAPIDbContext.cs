using FinalAssignment.Model;
using Microsoft.EntityFrameworkCore;

namespace FinalAssignment.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}

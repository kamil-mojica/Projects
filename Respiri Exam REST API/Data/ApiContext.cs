using Microsoft.EntityFrameworkCore;
using Respiri_Exam_REST_API.Models;

namespace Respiri_Exam_REST_API.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
    }
}

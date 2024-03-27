using GetOnIt.Models;
using Microsoft.EntityFrameworkCore;

namespace GetOnIt.Data
{
    public class GetOnItContext : DbContext
    {
        public GetOnItContext(DbContextOptions<GetOnItContext> options) : base(options) 
        { 
        }

        //Need To Create Data Model and Relationships THEN create DBSets.
        public DbSet<Tasks> Tasks { get; set; }
    }
}

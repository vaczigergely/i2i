namespace l2l.Data.Model
{
    public class L2lDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
    }
}
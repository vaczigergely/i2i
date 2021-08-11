using System;
using l2l.Data.Model;

namespace l2l.Data.Repository
{
    public class CourseRepository
    {
        private readonly L2lDbContext db;

        public CourseRepository()
        {
            var factory = new L2lDbContextFactory();
            db = factory.CreateDbContext(new string[] {});
        }

        public CourseRepository(L2lDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Course course)
        {
            db.Courses.Add(course);
        }

        public Course GetById(int Id)
        {
            return db.Courses.Find(Id);
        }

        public void Update(Course course)
        {
            db.Courses.Update(course);
        }

        public void Remove(Course course)
        {
            db.Courses.Remove(course);
        }
    }
}
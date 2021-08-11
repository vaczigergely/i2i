using System;
using Xunit;
using l2l.Data.Model;
using l2l.Data.Repository;
using FluentAssertions;

namespace l2l.Data.Tests
{
    /// <summary>
    /// CRUD and lists tests
    /// </summary>
    public class CourseRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture fixture;

        public CourseRepositoryTests(DatabaseFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public void CourseRepositoryTests_AddedCoursesShouldAppearInRepository()
        {
            // Arrange

            //SUT: System Under Test
            var sut = new CourseRepository(fixture.GetNewL2lDbContext());
            var course = new Course { Id = 1, Name = "Test Course" };

            // Act
            sut.Add(course);
            var result = sut.GetById(course.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(course);
        }

        [Fact]
        public void CourseRepositoryTests_ExistingCoursesShouldAppearInRepository()
        {
            // Arrange
            var sut = new CourseRepository(fixture.GetNewL2lDbContext());
            var course = new Course { Id = 1, Name = "Test Course" };
            sut.Add(course);

            // Act
            var result = sut.GetById(course.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(course);
        }

        [Fact]
        public void CourseRepositoryTests_ExistingCoursesShouldBeChanged()
        {
            // Arrange
            var sut = new CourseRepository(fixture.GetNewL2lDbContext());
            var course = new Course { Id = 1, Name = "Test Course" };
            sut.Add(course);
            var toUpdate = sut.GetById(course.Id);

            // Act
            toUpdate.Name = "Modified TestCourse";
            sut.Update(toUpdate);

            var afterUpdate = sut.GetById(course.Id);

            // Assert
            afterUpdate.Should().BeEquivalentTo(toUpdate);

        }

        [Fact]
        public void CourseRepositoryTests_ExistingCoursesShouldBeDeleted()
        {
            // Arrange
            var sut = new CourseRepository(fixture.GetNewL2lDbContext());
            var course = new Course { Id = 1, Name = "Test Course" };
            sut.Add(course);
            

            // Act
            var toDelete = sut.GetById(course.Id);
            sut.Remove(toDelete);
            var afterDelete = sut.GetById(course.Id);

            // Assert
            afterDelete.Should().BeNull();

        }
    }
}

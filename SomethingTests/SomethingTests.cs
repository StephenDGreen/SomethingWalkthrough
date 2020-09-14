using Something.Domain;
using SomethingTests.Infrastructure.Factories;
using System.Linq;
using Xunit;
using Domain = Something.Domain.Models;

namespace SomethingTests
{
    public class SomethingTests
    {
        [Fact]
        public void Something_HasAName()
        {
            var something = new Domain.Something();
            string expected = null;

            string actual = something.Name;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void SomethingFactory_Create_CreatesSomething()
        {
            SomethingFactory somethingFactory = new SomethingFactory();

            Domain.Something actual = somethingFactory.Create();

            Assert.IsType<Domain.Something>(actual);
        }
        [Fact]
        public void SomethingFactory_Create_CreatesSomethingWithName()
        {
            SomethingFactory somethingFactory = new SomethingFactory();
            string expected = "Fred Bloggs";

            Domain.Something actual = somethingFactory.Create(expected);

            Assert.IsType<Domain.Something>(actual);
            Assert.Equal(expected, actual.Name);
        }
        [Fact]
        public void DbContextFactory_CreateAppDbContext_SavesSomethingToDatabaseAndRetrievesIt()
        {
            var something = new Domain.Something() { Name = "Fred Bloggs" };
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingToDatabaseAndRetrievesIt)))
            {
                ctx.Somethings.Add(something);
                ctx.SaveChanges();
            };
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingToDatabaseAndRetrievesIt)))
            {
                var savedSomething = ctx.Somethings.Single();
                Assert.Equal(something.Name, savedSomething.Name);
            };
        }
    }
}

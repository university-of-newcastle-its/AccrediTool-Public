using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Repositories;

namespace UoN.AccrediTool.Tests.Core
{
   public class UoCourseRepositoryTest
    {
        [Fact]
        public void DoesNotFail()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot()).Options;
            var context = new DataContext(options);
            
            LoadFromConfigAsync(context).Wait();

            UoCourseRepository service = new UoCourseRepository(context);
            _ = service.GetCourseById(1).Result;

            context.Dispose();
        }

        [Fact]
        public void HasCorrectCourseName()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot()).Options;
            var context = new DataContext(options);

            LoadFromConfigAsync(context).Wait();

            UoCourseRepository service = new UoCourseRepository(context);
            UoCourseModel result = service.GetCourseById(1).Result;
            Assert.Equal("Test Course", result.Name);

            context.Dispose();
        }

        private async Task LoadFromConfigAsync(DataContext context)
        {
            await context.Course.AddAsync(new UoCourseModel { CourseId = 1, Name = "Test Course" }).ConfigureAwait(false);
        }
    }
}

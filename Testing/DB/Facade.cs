using System;
using System.Collections.Generic;
using Xunit;
using Moq;

using Head;
using db;

using Testing.Builders;

namespace Testing.BD
{
    public class FacadeGetTaskTests
    {
        private db.Task taskEmpty = null;

        [Fact]
        public void ShouldBeReturnTaskTest()
        {
            int id = 3;
            var mustBeResult = new db.Task();
            var mock = new Mock<db.IRepositoryTask>();
            mock.Setup(facade => facade.GetTask(id)).Returns(mustBeResult);
            var facade = new db.ConFacade(null, mock.Object, null);

            var result = facade.GetTask(id);

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldBeTrowExceptionTaskNotFoundTest()
        {
            int id = 3;
            var mock = new Mock<db.IRepositoryTask>();
            mock.Setup(facade => facade.GetTask(id)).Returns(taskEmpty);
            var facade = new db.ConFacade(null, mock.Object, null);

            Assert.Throws<Exception>(() => facade.GetTask(id));
        }
    }
}

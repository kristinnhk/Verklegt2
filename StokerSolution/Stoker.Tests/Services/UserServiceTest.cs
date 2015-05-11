using Microsoft.VisualStudio.TestTools.UnitTesting;

using Stoker.Services;
using Stoker.Tests;
using Stoker.Models;

namespace Stoker.Tests.Services
{

    [TestClass]
    public class GroupServiceTest
    {
        private GroupService service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();

            var f1 = new GroupModel
            {
                groupID = 1,
                title = "dabs",
                numberOfGroupMembers = 0
            };
            mockDb.groups.Add(f1);

            var f2 = new GroupModel
            {
                groupID = 2,
                title = "Steinn",
                numberOfGroupMembers = 3
            };
            mockDb.groups.Add(f2);

            var f3 = new GroupModel
            {
                groupID = 3,
                title = "Ellidi",
                numberOfGroupMembers = 2
            };
            mockDb.groups.Add(f3);

            service = new GroupService(mockDb);
        }


        [TestMethod]
        public void TestGetGroupByID()
        {
            //Arrange:
            const string title1 = "dabs";
            const string title2 = "Steinn";
            const string title3 = "Ellidi";
            //Act:
            var result1 = service.GetGroupByID(1);
            var result2 = service.GetGroupByID(2);
            var result3 = service.GetGroupByID(3);

            //Assert:
            Assert.AreEqual(result1.title, title1);
            Assert.AreEqual(result2.title, title2);
            Assert.AreEqual(result3.title, title3);


        }
    }
}

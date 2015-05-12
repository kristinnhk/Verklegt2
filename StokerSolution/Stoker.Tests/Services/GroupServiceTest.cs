﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

using Stoker.Services;
using Stoker.Tests;
using Stoker.Models;
using Stoker.Models.UnionModels;


namespace Stoker.Tests.Services
{

    [TestClass]
    public class GroupServiceTest
    {
        private GroupService service;
        private UserService service2;

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

            ApplicationUser user = new ApplicationUser();

            user.Id = "Steinn";
            user.gender = "male";
            user.firstName = "Ellidi";
            user.PasswordHash = "LALALAL";
            mockDb.Users.Add(user);
            

            service = new GroupService(mockDb);
            service2 = new UserService(mockDb);
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
            var result4 = service.GetGroupByID(4);

            //Assert:
            Assert.AreEqual(result1.title, title1);
            Assert.AreEqual(result2.title, title2);
            Assert.AreEqual(result3.title, title3);
            Assert.AreEqual(result4, null);


        }

        [TestMethod]
        public void TestGetGroupByTitle()
        {
            //Arrange:
            const string title1 = "s";
            const string title2 = "e";
            const string title3 = "d";
            //Act:
            var result1 = service.GetGroupByTitle(title1);
            var result2 = service.GetGroupByTitle(title2);
            var result3 = service.GetGroupByTitle(title3);

            List<GroupModel> finalResult1 = new List<GroupModel>();
            List<GroupModel> finalResult2 = new List<GroupModel>();
            List<GroupModel> finalResult3 = new List<GroupModel>();

            foreach (GroupModel group in result1)
            {
                finalResult1.Add(group);
            }
            foreach (GroupModel group in result2)
            {
                finalResult2.Add(group);
            }
            foreach (GroupModel group in result3)
            {
                finalResult3.Add(group);
            }

            //Assert:
            Assert.AreEqual(finalResult1.Count, 1);
            Assert.AreEqual(finalResult2.Count, 1);
            Assert.AreEqual(finalResult3.Count, 2);
            Assert.AreEqual(finalResult1[0].title, "dabs");
            Assert.AreEqual(finalResult3[1].title, "Ellidi");
            Assert.AreEqual(finalResult3[0], finalResult1[0]);
        }
    }
}

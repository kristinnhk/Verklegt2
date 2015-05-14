using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

using Stoker.Services;
using Stoker.Tests;
using Stoker.Models;

namespace Stoker.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();

            ApplicationUser user = new ApplicationUser();

            user.Id = "Steinn";
            user.gender = "male";
            user.firstName = "Ellidi";
            user.lastName = "Petursson";
            user.PasswordHash = "LALALAL";
            mockDb.Users.Add(user);

            ApplicationUser user2 = new ApplicationUser();

            user2.Id = "dabs";
            user2.gender = "male";
            user2.firstName = "Daniel";
            user2.lastName = "Brandur";
            user2.PasswordHash = "LA";
            user2.friendRequestSent = new List<ApplicationUser>();
            user2.friendRequestReceived = new List<ApplicationUser>();
            mockDb.Users.Add(user2);

            ApplicationUser user3 = new ApplicationUser();

            user3.Id = "Apple";
            user3.gender = "male";
            user3.firstName = "Steve";
            user3.lastName = "Jobs";
            user3.PasswordHash = "LA";
            user3.friendRequestSent = new List<ApplicationUser>();
            user3.friendRequestReceived = new List<ApplicationUser>();
            mockDb.Users.Add(user3);



            service = new UserService(mockDb);
        }

        [TestMethod]
        public void FriendRequestTest()
        {
            service.SendFriendRequest("Steinn", "dabs");
            service.SendFriendRequest("Apple", "dabs");

            List<ApplicationUser> user2Friends = service.GetFriendRequests("dabs").ToList();
            List<ApplicationUser> userFriends = service.GetFriendRequests("Steinn").ToList();

            Assert.AreEqual("Steinn", user2Friends[0].Id);
            Assert.AreEqual(0, userFriends.Count);
            Assert.AreEqual(2, user2Friends.Count);
        }

        [TestMethod]
        public void GetUserByIDTest()
        {
            ApplicationUser user1 = service.GetUserByID("Steinn");
            ApplicationUser user2 = service.GetUserByID("dabs");
            ApplicationUser user3 = service.GetUserByID("NoID");

            Assert.AreEqual("Ellidi", user1.firstName);
            Assert.AreEqual("Daniel", user2.firstName);
            Assert.AreEqual(null, user3);
        }

        [TestMethod]
        public void SetUserLastAndFirstNameTest()
        {
            ApplicationUser user = service.GetUserByID("Apple");

            service.SetFirstName(user.Id, "Bill");
            service.SetLastName(user.Id, "Gates");

            Assert.AreEqual("Bill", user.firstName);
            Assert.AreEqual("Gates", user.lastName);
        }
    }
}

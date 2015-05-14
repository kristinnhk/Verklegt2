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
    public class ThreadServiceTest
    {
        private ThreadService service;

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

            user2.Id = "Ellidi";
            user2.gender = "male";
            user2.firstName = "Ellidi";
            user2.lastName = "Petursson";
            user2.PasswordHash = "LALALAL";
            user2.friendRequestReceived = new List<ApplicationUser>();
            user2.friendRequestReceived.Add(user);
            user2.friendRequestSent = new List<ApplicationUser>();
            user2.friendRequestSent.Add(user);
            mockDb.Users.Add(user2);

            ThreadModel model1 = new ThreadModel();

            model1.threadID = 1;
            model1.title = "Test";
            model1.mainContent = "Test post please ignore";
            model1.likes = 0;
            model1.originalPoster = user;
            model1.dateCreated = DateTime.Now;
            mockDb.threads.Add(model1);

            ThreadModel model2 = new ThreadModel();

            model2.threadID = 2;
            model2.title = "Test2";
            model2.mainContent = "Test post2 please ignore";
            model2.likes = 1;
            model2.originalPoster = user;
            model2.dateCreated = DateTime.Now;
            mockDb.threads.Add(model2);

            user.threads = new List<ThreadModel>();
            user.threads.Add(model1);
            user.threads.Add(model2);
            service = new ThreadService(mockDb);
        }

        [TestMethod]
        public void GetUserThreadsTest()
        {
            List<ThreadModel> threads = service.GetUserThreads("Steinn").ToList();
            Assert.AreEqual(2, threads.Count);
        }

        [TestMethod]
        public void GetUserFriendsThreadsTest()
        {
            List<ThreadModel> threads = service.GetUserFriendsThreads("Ellidi").ToList();
            List<ThreadModel> threads2 = service.GetUserFriendsThreads("Steinn").ToList();
            Assert.AreEqual(2, threads.Count);
            Assert.AreEqual(0, threads2.Count);
        }
    }
}

using MTCG.repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Test
{
    public class TestUsersReps
    {

        [SetUp]
        public void Setup() 
        {
            TestHelper.insertUsers();
        }

        [Test]
        public void TestGetUser()
        {
            Assert.AreNotEqual(new UserReps().getUser("user1"), null);
            Assert.Null(new UserReps().getUser("unknownUser"));
        }

        [Test]
        public void TestAddUser() 
        {
            Assert.IsTrue(new UserReps().addUser("user3","1234"));
            Assert.IsFalse(new UserReps().addUser("user2", "1234"));
        }
        
        [Test]
        public void TestGetToken()
        {
            Assert.AreEqual(new UserReps().getToken("user1", "1234"), "user1-mtcgToken");
            Assert.AreNotEqual(new UserReps().getToken("user2", "1234"), "user1-mtcgToken");
            Assert.AreNotEqual(new UserReps().getToken("user2", "123"), "user2-mtcgToken");
            Assert.AreNotEqual(new UserReps().getToken("user", "1234"), "user2-mtcgToken");
        }

        [Test]
        public void TestPassword()
        {
            User user = new UserReps().getUser("user1");
            Assert.AreEqual(user.password, "1234");
            Assert.AreNotEqual(user.password, "123");
        }
        [Test]
        public void TestEditUser()
        {
            User user = new UserReps().getUser("user1");
            Assert.IsEmpty(user.image);

            Assert.True(new UserReps().updateUser(user.username,"user1","Test bio","Test image"));

            user = new UserReps().getUser("user1");
            Assert.AreEqual("Test image", user.image);
        }

        [Test]
        public void TestshouldDeleteUser()
        {
            Assert.IsTrue(new UserReps().deleteUser("user2"), "Deleted");
            Assert.IsFalse(new UserReps().deleteUser("user3"), "Fehler");
        }
    }
}

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
            Database.GetInstance();
            UserReps userReps = new UserReps();
            userReps.addUser("test1", "1234");
            userReps.addUser("test2", "1234");
            //User user1 = new User("test1", "test1", "1234", "test1-mtcgToken", null, null,20);
            //User user2 = new User("test2", "test2", "1234", "test2-mtcgToken", null, null, 20);
        }
        
        [Test]
        public void TestAddUserReturnTrue() 
        {
            Assert.IsTrue(new UserReps().addUser("test3","1234"),"users added");
        }
        
        [Test]
        public void TestAddUserReturnfalse()
        {
            UserReps userReps = new UserReps();
            userReps.addUser("test2", "1234");
            Assert.IsFalse(userReps.addUser("test2", "1234"),"Fehler");
        }

        [Test]
        public void TestTakeTokenReturnToken()
        {
            Assert.AreEqual(new UserReps().getToken("test1", "1234"), "test1-mtcgToken");
            Assert.AreNotEqual(new UserReps().getToken("test2", "1234"), "test1-mtcgToken");
            Assert.AreNotEqual(new UserReps().getToken("test2", "123"), "test1-mtcgToken");
            Assert.AreNotEqual(new UserReps().getToken("test", "1234"), "test1-mtcgToken");
        }

        [Test]
        public void TestdeleteUserReturnTrue()
        {
            Assert.IsTrue(new UserReps().deleteUser("test3"), "Deleted");
            Assert.IsFalse(new UserReps().deleteUser("test3"), "Fehler");
        }

     
    }
}

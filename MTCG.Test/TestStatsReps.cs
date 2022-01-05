using MTCG.data.entity;
using MTCG.Play.play;
using MTCG.repository;
using MTCG.repository.entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Test
{
    public class TestStatsReps
    {
        [SetUp]
        public void Setup()
        {
            TestHelper.insertUsers();
            TestHelper.insertCards();
            TestHelper.configeDeck();
        }

        [Test]
        public void TestGetStats()
        {
            StatsUser statsUser1 = new StatReps().getStatsUserByUsername("user1");
            StatsUser statsUser2 = new StatReps().getStatsUserByUsername("user2");
            StatsUser statsUser3 = new StatReps().getStatsUserByUsername("user3");

            Assert.NotNull(statsUser1);
            Assert.NotNull(statsUser2);
            Assert.Null(statsUser3);
            Assert.AreEqual(2, statsUser1.lose);
            Assert.AreEqual(2, statsUser2.win);
        }
    }
}

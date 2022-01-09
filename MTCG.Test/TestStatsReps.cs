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
        public void TestGetStatsByUsername()
        {
            Stat stat = new StatReps().getStatsByUsername("user1");

            Assert.IsNotNull(stat);
            Assert.AreEqual("user1", stat.username);
        }

        [Test]
        public void TestGetStatsUserByUsername()
        {
            StatsUser statsUser = new StatReps().getStatsUserByUsername("user1");

            Assert.IsNotNull(statsUser);
            Assert.AreEqual(20,statsUser.coin);
        }


        [Test]
        public void TestGetStatsAllUsers()
        {
            List<StatsUser> statList = new StatReps().getStatsAllUsers();

            Assert.IsNotNull(statList);
            Assert.AreEqual(2, statList.Count);
        }

        [Test]
        public void TestUpdateStatWinnerByUsernameAfterPlay()
        {
            Stat stat = new StatReps().getStatsByUsername("user1");
            Assert.AreEqual(100, stat.elo);
            Assert.AreEqual(0, stat.win);

            new StatReps().updateStatWinnerByUsernameAfterPlay("user1");
            
            Stat NewStat = new StatReps().getStatsByUsername("user1");
            Assert.AreEqual(103, NewStat.elo);
            Assert.AreEqual(1, NewStat.win);
        }

        [Test]
        public void TestUpdateStatLoserByUsernameAfterPlay()
        {
            Stat stat = new StatReps().getStatsByUsername("user1");
            Assert.AreEqual(100, stat.elo);
            Assert.AreEqual(0, stat.lose);

            new StatReps().updateStatLoserByUsernameAfterPlay("user1");

            Stat NewStat = new StatReps().getStatsByUsername("user1");
            Assert.AreEqual(95, NewStat.elo);
            Assert.AreEqual(1, NewStat.lose);
        }

        [Test]
        public void TestUpdateStatDrawByUsernameAfterPlay()
        {
            Stat stat = new StatReps().getStatsByUsername("user1");
            Assert.AreEqual(100, stat.elo);
            Assert.AreEqual(0, stat.draw);

            new StatReps().updateStatDrawByUsernameAfterPlay("user1");

            Stat NewStat = new StatReps().getStatsByUsername("user1");
            Assert.AreEqual(100, NewStat.elo);
            Assert.AreEqual(1, NewStat.draw);
        }
    }
}

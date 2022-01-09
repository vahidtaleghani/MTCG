using MTCG.endpoints.battle;
using MTCG.Play;
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
    public class TestBattle
    {
        [SetUp]
        public void Setup()
        {
            TestHelper.insertUsers();
            TestHelper.insertCards();
            TestHelper.configeDeck();
        }

        [Test]
        public void TestCanProcessOneMonsterOneSpellCards()
        {
            Card card1 = new CardReps().getCardById("1");
            Card card2 = new CardReps().getCardById("2");

            Assert.IsTrue(new OneMonsterOneSpellCards().canProcrss(card1, card2));
            Assert.IsFalse(new TwoMonsterCards().canProcrss(card1, card2));
            Assert.IsFalse(new TwoSpellCards().canProcrss(card1, card2));

        }

        [Test]
        public void TestOneMonsterOneSpellCards()
        {
            
            Card card1 = new CardReps().getCardById("1");
            Card card2 = new CardReps().getCardById("2");

            new OneMonsterOneSpellCards().handleBattle(card1, card2);

            Assert.AreEqual(95,new StatReps().getStatsByUsername("user1").elo);
            Assert.AreEqual(103, new StatReps().getStatsByUsername("user2").elo);
        }

        [Test]
        public void TestCheckMonster()
        {

            Card card1 = new CardReps().getCardById("3");
            Card card2 = new CardReps().getCardById("4");
            Assert.AreEqual(-1, new MonsterType().checkMonsterType(new MonsterType().getMonsterType(card1.card_type),
                new MonsterType().getMonsterType(card2.card_type)));
        }

        [Test]
        public void TestCanProcessTwoMonsterCards()
        {

            Card card1 = new CardReps().getCardById("3");
            Card card2 = new CardReps().getCardById("4");

            Assert.IsFalse(new OneMonsterOneSpellCards().canProcrss(card1, card2));
            Assert.IsTrue(new TwoMonsterCards().canProcrss(card1, card2));
            Assert.IsFalse(new TwoSpellCards().canProcrss(card1, card2));

        }

        [Test]
        public void TestTwoMonsterCards()
        {

            Card card1 = new CardReps().getCardById("3");
            Card card2 = new CardReps().getCardById("4");

            new TwoMonsterCards().handleBattle(card1, card2);

            Assert.AreEqual(95, new StatReps().getStatsByUsername("user1").elo);
            Assert.AreEqual(103, new StatReps().getStatsByUsername("user2").elo);
        }

        [Test]
        public void TestCanProcessTwoSpellCards()
        {
            Card card1 = new CardReps().getCardById("5");
            Card card2 = new CardReps().getCardById("6");

            Assert.IsFalse(new OneMonsterOneSpellCards().canProcrss(card1, card2));
            Assert.IsFalse(new TwoMonsterCards().canProcrss(card1, card2));
            Assert.IsTrue(new TwoSpellCards().canProcrss(card1, card2));

        }

        [Test]
        public void TestTwoSpellCards()
        {
            Card card1 = new CardReps().getCardById("5");
            Card card2 = new CardReps().getCardById("6");

            new TwoSpellCards().handleBattle(card1, card2);

            Assert.AreEqual(103, new StatReps().getStatsByUsername("user1").elo);
            Assert.AreEqual(95, new StatReps().getStatsByUsername("user2").elo);
        }
    }
}

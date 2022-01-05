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
    public class TestCardsReps
    {
        [SetUp]
        public void Setup()
        {
            TestHelper.insertCards();
        }

        [Test]
        public void TestGetCardById()
        {
            Assert.AreNotEqual(new CardReps().getCardById("1"), null);
            Assert.Null(new CardReps().getCardById("9"));
        }

        [Test]
        public void TestAddCard()
        {
            int package_id = new CardReps().getLastpackageId();
            Assert.IsTrue(new CardReps().addCard("9", "FireSpell", "Fire", "Spell", 20, package_id+1), null);
            Assert.IsFalse(new CardReps().addCard("9", "FireSpell", "Fire", "Spell", 20, package_id+1), null);
        }

        [Test]
        public void TestConfigeDeck()
        {
            Card card = new CardReps().getCardById("1");
            Assert.IsEmpty(card.username);

            Assert.IsTrue(new CardReps().updateUsernameOfCards("user1",1));

            Card card1 = new CardReps().getCardById("1");
            Assert.AreEqual(card1.username, "user1");
        }

        [Test]
        public void TestGetDeckOfUsername()
        {
            List<Card> cardList = new CardReps().getDeckByUsername("user1");
            foreach (Card card in cardList)
            {
                Assert.AreEqual(card.packageId, 1);
            }  
        }

        [Test]
        public void TestFirstFreePackageId()
        {
            Assert.AreEqual(2, new CardReps().getFirstFreePackageId());
        }

        [Test]
        public void TestGetLastPackageId()
        {
            Assert.AreEqual(2,new CardReps().getLastpackageId());
        }
        /*
        [Test]
        public void TestShouldDeleteCard()
        {
            Assert.IsTrue(new CardReps().deleteCardsByUsername("user1"));
        }*/
    }
}

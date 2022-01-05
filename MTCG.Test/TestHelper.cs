using MTCG.repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Test
{
    public class TestHelper
    {
        [SetUp]
        public void Setup()
        {
 
        }

        public static void insertUsers()
        {
            Database.GetInstance().truncateUsersTable();
            new UserReps().addUser("user1", "1234");
            new UserReps().addUser("user2", "1234");
        }

        public static void insertCards()
        {
            Database.GetInstance().truncateCardsTable();
            new CardReps().addCard("1", "WaterSpell", "Water", "Spell", 20, 1);
            new CardReps().addCard("2", "WaterGoblin", "Water", "Goblin", 25, 2);
            new CardReps().addCard("3", "Goblin", "", "Goblin", 30, 1);
            new CardReps().addCard("4", "Dragon", "", "Dragon", 20, 2);
            new CardReps().addCard("5", "WaterSpell", "Water", "Spell", 20, 1);
            new CardReps().addCard("6", "FireSpell", "Fire", "Spell", 25, 2);
            //new CardReps().addCard("7", "Knights", "", "Knights", 20, 1);
            //new CardReps().addCard("8", "WaterSpells", "Water", "Spell", 25, 2);
        }

        public static void configeDeck()
        {
            new CardReps().updateUsernameOfCards("user1", 1);
            new CardReps().updateUsernameOfCards("user2", 2);
        }
    }
}

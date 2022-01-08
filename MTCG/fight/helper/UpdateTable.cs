using MTCG.repository;
using MTCG.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Play
{
    public class UpdateTable
    {
        public void update_Stat_Table(Card card1, Card card2)
        {
            try
            {
                new StatReps().updateStatDrawByUsernameAfterPlay(card1.username);
                new StatReps().updateStatDrawByUsernameAfterPlay(card2.username);
            }
            catch (Exception exc )
            {
                Console.WriteLine("| Error: " + exc.Message);
            } 
        }

        public void update_Deck_Card_Stat_Table(Card card1, Card card2)
        {
            try
            {
                new CardReps().updateDeckByUsernameAfterPlay(card1.id, card1.username);
                new CardReps().updateCardByUsername(card2.id, card1.username);
                new StatReps().updateStatWinnerByUsernameAfterPlay(card1.username);
                new StatReps().updateStatLoserByUsernameAfterPlay(card2.username);
            }
            catch (Exception exc)
            {
                Console.WriteLine("| Error: " + exc.Message);
            }
        }
    }
}

using MTCG.endpoints.battle;
using MTCG.repository.entity;
using System;


namespace MTCG.Play.play
{
    public class TwoMonsterCards : IFights
    {
        MonsterType monsterType = new MonsterType();
        public bool canProcrss(Card card1, Card card2)
        {
            return (!card1.card_type.Equals("Spell")) && (!card2.card_type.Equals("Spell"));
        }
        public bool handleBattle(Card card1, Card card2)
        {
            try
            {
                int checkMonster = monsterType.checkMonsterType(monsterType.getMonsterType(card1.card_type), monsterType.getMonsterType(card2.card_type));
                switch (checkMonster)
                {
                    case 1:
                        {
                            new UpdateTable().update_Deck_Card_Stat_Table(card1, card2);
                            break;
                        }
                     
                    case 2:
                        if(card1.element_type.Equals("Fire"))
                        {
                            new UpdateTable().update_Deck_Card_Stat_Table(card1, card2);
                            break;
                        }
                        goto default;

                    case -1:
                        {
                            new UpdateTable().update_Deck_Card_Stat_Table(card2, card1);
                            break;
                        } 
                    case -2:
                        if (card2.element_type.Equals("Fire"))
                        {
                            new UpdateTable().update_Deck_Card_Stat_Table(card2, card1);
                            break;
                        }
                        goto default;
                    default:
                        {
                            if (card1.damage > card2.damage)
                                new UpdateTable().update_Deck_Card_Stat_Table(card1, card2); 
                            else if (card1.damage < card2.damage)
                                new UpdateTable().update_Deck_Card_Stat_Table(card2, card1);
                            else
                            {
                                new UpdateTable().update_Stat_Table(card2, card1);
                                return false;
                            } 
                        }
                        break;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("| Error: " + exc.Message);
                return false;
            }
            return true;
        }
    }
}

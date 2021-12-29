using MTCG.endpoints.battle;
using MTCG.endpoints.battle.play;
using MTCG.repository.entity;
using System;

namespace MTCG.Play.play
{
    public class TwoSpellCards : IFights
    {
        ElementType elementType = new ElementType();
        public bool canProcrss(Card card1, Card card2)
        {
            return card1.card_type.Equals("Spell") && card2.card_type.Equals("Spell");
        }
        public bool handleBattle(Card card1, Card card2)
        {
            try
            {
                int checkElement = elementType.checkElementType(elementType.getElementType(card1.element_type), elementType.getElementType(card2.element_type));
                switch (checkElement)
                {
                    case 1:
                        {
                            if (new Damage().checkDamage(card1.damage, card2.damage) == 1)
                                new UpdateTable().update_Deck_Card_Stat_Table(card1, card2);
                            else if(new Damage().checkDamage(card1.damage, card2.damage) == -1)
                                new UpdateTable().update_Deck_Card_Stat_Table(card2, card1);
                            else
                            {
                                new UpdateTable().update_Stat_Table(card2, card1);
                                return false;
                            }
                            break;
                        }

                    case -1:
                        {
                            if (new Damage().checkDamage(card2.damage, card1.damage) == 1)
                                new UpdateTable().update_Deck_Card_Stat_Table(card2, card1);
                            else if (new Damage().checkDamage(card2.damage, card1.damage) == -1)
                                new UpdateTable().update_Deck_Card_Stat_Table(card1, card2);
                            else
                            {
                                new UpdateTable().update_Stat_Table(card2, card1);
                                return false;
                            }
                            break;
                        }
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
                            break;
                        }
                }
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("| Error: " + exc.Message);
                return false;
            }
        }
    }
}

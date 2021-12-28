using MTCG.endpoints.battle;
using MTCG.endpoints.battle.play;
using MTCG.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Play.play
{
    public class OneMonsterOneSpellCards : IFights
    {

        public bool canProcrss(Card card1, Card card2)
        {
            return ((card1.card_type.Equals("Spell") && !card2.card_type.Equals("Spell")) || 
                    (!card1.card_type.Equals("Spell") && card2.card_type.Equals("Spell")));
        }

        public bool handleBattle(Card card1, Card card2)
        {
            try
            {
                if (!isSpell(card1))
                {
                    Card change = card1;
                    card1 = card2;
                    card2 = change;
                }
                // Kraken Monster card -> Kraken gewinnt
                if (new MonsterType().getMonsterType(card2.card_type).Equals(MonsterType.Monster_Type.Kraken))
                {
                    new UpdateTable().update_Deck_Card_Stat_Table(card2, card1);
                }
                // Knights Monster card And water Spell Card-> waterSpells Card gewinnt
                else if(new MonsterType().getMonsterType(card2.card_type).Equals(MonsterType.Monster_Type.Knight) &&
                    new ElementType().getElementType(card1.element_type).Equals(ElementType.Element_Type.Water))
                {
                        new UpdateTable().update_Deck_Card_Stat_Table(card1, card2);
                }
                //pure Monster card -> abhängig von Damage 
                else if(String.IsNullOrEmpty(card2.element_type))
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
                else
                {
                    return new TwoSpellCards().handleBattle(card1, card2);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("| Error: " + exc.Message);
                return false;
            }
            
            return true;
        }

        public bool isSpell(Card card1)
        {
            if (card1.card_type.Equals("Spell"))
                return true;
            return false;
        }
    }
}

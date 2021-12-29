using MTCG.Play.play;
using MTCG.repository.entity;
using System.Collections.Generic;


namespace MTCG.Play
{
    public class FightController
    {
        List<IFights> battleList = new List<IFights>();
        public FightController()
        {
            battleList.Add(new TwoSpellCards());
            battleList.Add(new TwoMonsterCards());
            battleList.Add(new OneMonsterOneSpellCards());
        }

        public bool getResult(Card card1, Card card2 )
        {
            foreach (IFights battle in battleList)
            {  
                if (battle.canProcrss(card1,card2))
                {
                    return battle.handleBattle(card1, card2);
                }
            }
            return false;
        }
    }
}

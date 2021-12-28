using MTCG.repository.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Play
{
    public interface IFights
    {
        public bool canProcrss(Card card1, Card card2);
        public bool handleBattle(Card card1, Card card2);
    }
}

using MTCG.repository.entity;

namespace MTCG.Play
{
    public interface IFights
    {
        public bool canProcrss(Card card1, Card card2);
        public bool handleBattle(Card card1, Card card2);
    }
}

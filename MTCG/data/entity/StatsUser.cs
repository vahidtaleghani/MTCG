using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.data.entity
{
    public class StatsUser
    {
        public String username { get; private set; }
        public String name { get; private set; }
        public String bio { get; private set; }
        public String image { get; private set; }
        public int coin { get; private set; }
        public int elo { get; private set; }
        public int win { get; private set; }
        public int lose { get; private set; }
        public int draw { get; private set; }
        public int sumPlay { get; private set; }

        public StatsUser(String username, String name, String bio, String image, int coin, int elo, int win, int lose, int draw, int sumPlay)
        {
            this.username = username;
            this.name = name;
            this.bio = bio;
            this.image = image;
            this.coin = coin;
            this.elo = elo;
            this.win = win;
            this.lose = lose;
            this.draw = draw;
            this.sumPlay = sumPlay;
        }
    }
}

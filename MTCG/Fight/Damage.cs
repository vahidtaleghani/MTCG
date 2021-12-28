using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.endpoints.battle.play
{
    public class Damage
    {
        public int checkDamage(double Player1, double Player2)
        {
            if ((Player1 * 2) > (Player2 / 2))
                return 1;
            else if ((Player1 * 2) < (Player2 / 2))
                return -1;
            else
                return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.data.entity
{
    public class Trade
    {
        public String username { get; private set; }
        public String id { get; private set; }
        public String card_trade_id { get; private set; }
        public String card_type { get; private set; }
        public double min_damage { get; private set; }
        public bool was_stored { get; private set; }


        public Trade(String username, String id, String card_trade_id, String card_type, double min_damage, bool was_stored)
        {
            this.username = username;
            this.id = id;
            this.card_trade_id = card_trade_id;
            this.card_type = card_type;
            this.min_damage = min_damage;
            this.was_stored = was_stored;
        }
    }
}

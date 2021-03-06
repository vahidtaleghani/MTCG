using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.repository.entity
{
    public class Card
    {
        public String id { get; private set; }
        public String name { get; private set; }
        public String element_type { get; private set; }
        public String card_type { get; private set; }
        public double damage { get; private set; }
        public int packageId { get; private set; }
        public String username { get; private set; }
        public bool deck { get; private set; }

        public Card(String id, String name, String element_type, String card_type,double damage, int packageId, String username, bool deck)
        {
            this.id = id;
            this.name = name;
            this.element_type = element_type;
            this.card_type = card_type;
            this.damage = damage;
            this.packageId = packageId;
            this.username = username;
            this.deck = deck;
        }
    }
}

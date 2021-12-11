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
        public int damage { get; private set; }
        public String packageId { get; private set; }
        public String username { get; private set; }
        public bool deck { get; private set; }


        public Card(String id, String name, int damage, String packageId, String username, bool deck)
        {
            this.id = id;
            this.name = name;
            this.damage = damage;
            this.packageId = packageId;
            this.username = username;
            this.deck = deck;
        }
    }
}

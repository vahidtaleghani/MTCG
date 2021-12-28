using System;

namespace MTCG.endpoints.battle
{
    public class MonsterType
    {
        public enum Monster_Type { Goblin, Dragon, Wizard, Ork, Knight, Kraken, Elf, Troll };
        public Monster_Type getMonsterType(String monsterType)
        {
            switch (monsterType)
            {
                case "Goblin":
                    return Monster_Type.Goblin;
                case "Dragon":
                    return Monster_Type.Dragon;
                case "Wizard":
                    return Monster_Type.Wizard;
                case "Ork":
                    return Monster_Type.Ork;
                case "Knight":
                    return Monster_Type.Knight;
                case "Kraken":
                    return Monster_Type.Kraken;
                case "Elf":
                    return Monster_Type.Elf;
                default:
                    return Monster_Type.Troll;
            }
        }
        public int checkMonsterType(Monster_Type player1, Monster_Type player2)
        {
            switch (player1)
            {
                case Monster_Type.Dragon:
                    {
                        if (player2.Equals(Monster_Type.Goblin))
                            return 1;
                        else if (player2.Equals(Monster_Type.Elf))
                            return -2;
                        return 0;
                    }
                case Monster_Type.Wizard:
                    {
                        if (player2.Equals(Monster_Type.Ork))
                            return 1;
                        return 0;
                    }
                case Monster_Type.Goblin:
                    {
                        if (player2.Equals(Monster_Type.Dragon))
                            return -1;
                        return 0;
                    }
                case Monster_Type.Ork:
                    {
                        if (player2.Equals(Monster_Type.Wizard))
                            return -1;
                        return 0;
                    }
                case Monster_Type.Elf:
                    {
                        if (player2.Equals(Monster_Type.Dragon))
                            return 2;
                        return 0;
                    }
                default:
                    return 0;
            }
        }
    }
}

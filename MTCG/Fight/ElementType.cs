using System;

namespace MTCG.endpoints.battle
{
    public class ElementType
    {
        public enum Element_Type { Water, Fire, Regular };
        public Element_Type getElementType(String elementType)
        {
            switch (elementType)
            {
                case "Water":
                    return Element_Type.Water;
                case "Fire":
                    return Element_Type.Fire;
                case "Regular":
                    return Element_Type.Regular;
                default:
                    return Element_Type.Regular;
            }
        }
        public int checkElementType(Element_Type player1, Element_Type player2)
        {
            switch (player1)
            {
                case Element_Type.Water:
                    {
                        if (player2.Equals(Element_Type.Fire))
                            return 1;
                        else if (player2.Equals(Element_Type.Water))
                            return 0;
                        return -1;
                    }
                case Element_Type.Fire:
                    {
                        if (player2.Equals(Element_Type.Regular))
                            return 1;
                        else if (player2.Equals(Element_Type.Fire))
                            return 0;
                        return -1;
                    }
                case Element_Type.Regular:
                    {
                        if (player2.Equals(Element_Type.Water))
                            return 1;
                        else if (player2.Equals(Element_Type.Regular))
                            return 0;
                        return -1;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
    }
}

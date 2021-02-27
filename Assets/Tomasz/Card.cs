using System;
[Serializable]
public class Card 
{

    public enum CardType { weapon, room, character }

    public CardType cT;
    public string n;

    public Card(CardType cardType, string name)
    {
        cT = cardType;
        n = name;
    }
    public CardType getCardType() {
        return cT;
    }
    public string getName() {
        return n;
    } 
}

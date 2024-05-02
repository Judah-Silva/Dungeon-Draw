using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class CardStats
{
    public String name;
    public CardType type;
    public CardRarity rarity;
    public int manaCost;
    public List<Effect> Effects;
    
    public CardStats(String name, CardType type, CardRarity rarity, int manaCost, List<Effect> effects)
    {
        this.name = name;
        this.type = type;
        this.rarity = rarity;
        this.manaCost = manaCost;
        Effects = effects;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Relic
{
    public Relic(int id, string imagePath, string name,  string description, int price, rarityValues rarity)
        { //For fully custom relic function
            this.id = id;
            if(imagePath!=null)
            {art = null;}else{art = null;}
    
            this.name = name;
            this.description = description;
            this.value = price;
            this.rarity = rarity;
            this.art = Resources.Load<Sprite>(imagePath);
            this.function = new relicFunction[1];
            this.function[0] = relicFunction.Custom;
        }
    public Relic(int id, string imagePath, string name,  string description, int price, rarityValues rarity, relicFunction function,relicAbility ability,  int intToUse)
    { //For one relic function
        this.id = id;
        if(imagePath!=null)
        {art = null;}else{art = null;}

        this.name = name;
        this.description = description;
        this.value = price;
        this.rarity = rarity;
        this.art = Resources.Load<Sprite>(imagePath);
        this.abilities = new relicAbility[1];
        this.function = new relicFunction[1];
        this.intToUse= new int[1];
        this.abilities[0] = ability;
        this.function[0] = function;
        this.intToUse[0] = intToUse;
    }
   public Relic(int id, string imagePath, string name, string description, int price, rarityValues rarity,relicFunction function, relicAbility ability, relicFunction function2, relicAbility ability2 ,int intToUse, int intToUse2)
       { //for two relic functions
           this.id = id;
           if(imagePath!=null)
           {art = null;}else{art = null;}
   
           this.name = name;
           this.description = description;
           this.value = price;
           this.rarity = rarity;
           this.art = Resources.Load<Sprite>(imagePath);
           this.abilities = new relicAbility[2];
           this.function = new relicFunction[2];
           this.intToUse= new int[2];
           this.abilities[0] = ability;
           this.abilities[1] = ability2;
           this.function[0] = function;
           this.function[1] = function2;
           this.intToUse[0] = intToUse;
           this.intToUse[1] = intToUse2;
       }

    public int id;
    public Sprite art;
    public string name;
    public string description;
    public int value;
    public rarityValues rarity;
    public relicAbility[] abilities;
    public relicFunction[] function;
    public int[] intToUse;

    public void PerformFunction()
    {
        for (int i = 0; i < function.Length; i++)
        {
            if (function[i] == relicFunction.Instant)
            {
                switch (abilities[i])
                {
                    case relicAbility.increaseMaxHp:
                        PlayerStats.MaxHealth += intToUse[i]; 
                        break;
                    case relicAbility.increaseMaxMana:
                        //
                        break;
                    case relicAbility.coinIncrease:
                        PlayerStats.Coins += intToUse[i];
                        break;
                    case relicAbility.Custom:
                        switch (id)
                        {
                            case 1: //Big Toe Relic - adds random card to player deck
                                PlayerStats.Deck.Add(Random.Range(0, CardDataBase.allCards.Count));
                                PlayerStats.TotalDeckSize++;
                                break;
                        }
                        break;
                }
            }
            else if (function[i] == relicFunction.Combat)
            {

            }
            else if (function[i] == relicFunction.Custom)
            {
                
            }
        }
    }

}

public enum relicAbility
{
    Custom, //Use for more specific function
    //Stat abilities
    increaseMaxHp,
    increaseMaxMana,
    coinIncrease,
    
    //Combat abilities
}

public enum relicFunction
{
    Custom,//Use for specific function
    Instant,
    Combat, //Relics that affect combat
    Card
}

public enum rarityValues
{
    common,
    uncommon,
    rare
}


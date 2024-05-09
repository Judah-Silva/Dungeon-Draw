using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDatabase : MonoBehaviour
{
    public static List<Relic> allRelics = new List<Relic>();
    // private static string imagePath = "RelicArt/";

    public static void prePopulate()
    {
        allRelics.Add(new Relic(0, "RelicArt/Strawberry", "Strawberry", "Increases Max HP by 7 \n- Stackable", 45, rarityValues.common,  relicFunction.Instant, relicAbility.increaseMaxHp,7)); //Single function relic
        allRelics.Add(new Relic(1, "RelicArt/BigToe", "Lucky Big Toe", "Increases Max Mana by 1 \n- Non-Stackable", 125, rarityValues.rare)); //
        allRelics.Add(new Relic(2, "RelicArt/Starfish", "Starfish", "A living breathing starfish... Raises Maximum HP by 14 \n- Stackable", 85, rarityValues.rare, relicFunction.Instant, relicAbility.increaseMaxHp, 14));
        allRelics.Add(new Relic(3, "RelicArt/TNT", "TNT", "Adds 2 random cards to you deck \n- Stackable", 50, rarityValues.common, relicFunction.Instant, relicAbility.Custom, -1));
        allRelics.Add(new Relic(4, "RelicArt/PurpleTurtle", "Purple Turtle", "Prevents the first time you would lose HP in combat", 80, rarityValues.rare));
        allRelics.Add(new Relic(5, "RelicArt/GreenLiquid", "Suspicious Green Liquid", "When an enemy dies, gain 1 mana and draw 1 card \n- Non-Stackable", 75, rarityValues.uncommon, relicFunction.Combat, relicAbility.Custom, 1));
        allRelics.Add(new Relic(6, "RelicArt/Cookie", "Chocolate Chip Cookie", "If you end your turn without block, gain 3 block \n- Non-Stackable", 60, rarityValues.common, relicFunction.Combat, relicAbility.Custom, 3));
        allRelics.Add(new Relic(7, "RelicArt/SandShovel", "Sand Shovel", "You can now dig for relics at rest sites", 125, rarityValues.rare));
        allRelics.Add(new Relic(8, "RelicArt/Penny", "Penny", "Cards in the shop now replenish", 80, rarityValues.uncommon));
        allRelics.Add(new Relic(9, "RelicArt/BallToy", "Ball and String Toy", "Heal 5 HP after winning a combat encounter \n- Non-Stackable", 100, rarityValues.rare));
        allRelics.Add(new Relic(10, "RelicArt/CellPhone", "Cellphone", "When you would die, heal to 33% HP instead \n- Stackable", 175, rarityValues.rare));
        allRelics.Add(new Relic(11, "RelicArt/GreenGlasses", "Slimy Glasses", "Whenever you enter a shop, heal 6 HP \n- Non-Stackable", 85, rarityValues.uncommon));
        allRelics.Add(new Relic(12, "RelicArt/Shell", "Shell", "Grants the user 1 shield every combat round \n- Non-Stackable", -1, rarityValues.unique));
    }

    public static Relic getRelic(int id)
    {
        return allRelics[id];
        
    }
   
}

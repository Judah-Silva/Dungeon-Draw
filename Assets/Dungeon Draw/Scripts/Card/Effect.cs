using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{

    // int representing the type of effect a given int is
    private int effectType;
    // string that holds the name of an effect for display
    private string effectName;
    // value of effect such as the 6 in damage 6
    private int effectVal;

    /*
     * 
     * Effect type list as of the moment: 
     * 
     * [ damage (0) , block (1), 
     * 
     */

    // Reminder: whenever adding a new 

    public Effect(int type, int val)
    {
        effectType = type;
        effectVal = val;
        setEffectName();
    }

    // Sets the effect name outside of the constructor
    public void setEffectName()
    {
        switch (effectType)
        {
            case 0:
                effectName = "Damage";
                break;
            case 1:
                effectName = "Shield";
                break;
            case 2:
                effectName = "Vulnerable";
                break;
            case 3: 
                effectName = "Weak";
                break;
            case 4:
                effectName = "Frail";
                break;
            case 5:
                effectName = "Artifact";
                break;
            case 6:
                effectName = "Double Slash";
                break;
            case 7:
                effectName = "Whirlwind Slash";
                break;
            default:
                effectName = "Damage";
                break;

        }
    }

    public int GetEffectType()
    {
        return effectType;
    }

    public int GetEffectVal()
    {
        return effectVal;
    }

    // Formats the effect so that it can be displayed on the card
    public string showEffect()
    {

        string tempFormat = effectName;

        if (hasVal())
        {
            tempFormat += " " + effectVal;
        }

        return tempFormat;

    }

    // Used by the show effect function to let the system know if the value need to be shown
    private bool hasVal()
    {
        if (effectType == 3)
        {
            return false;
        }

        return true;
    }

    // used by the block class to determine whether this effect is currently playable or not
    public bool isPlayable()
    {

        return effectType != 3;
    }

    // Function used by the block class that iterates through all effects to tell the system to use the various effects
    // Will primarily be a switch case function that leads into other functions

    // IN PROGRESS

    // Current effect case list
    // [ damage (0) , block (1) , 

    public void dealEffect(Entity origin, Entity target)
    {
        
        switch (effectType)
        {
            case 0:
                dealDamage(origin, target);
                break;
            case 1:
                giveShield(target);
                break;
            case 2:
                giveVul(target);
                break;
            case 3:
                giveWeak(target);
                break;
            case 4:
                giveFrail(target);
                break;
            case 5:
                giveArtifact(target);
                break;
            case 6:
                dealDoubleSlash(origin, target);
                break;
            case 7:
                dealWhirlWindSlash(origin, target);
                break;
        }

    }


    // lenghty function that goes through some checks before dealing damage
    private void dealDamage(Entity origin, Entity target)
    {
        // Modify damage value here
        int tempVal = effectVal;

        tempVal -= origin.getWeak();

        target.TakeDamage(tempVal);

        // Debug.Log($"{tempVal} damage dealt");
    }

    private void dealDoubleSlash(Entity origin, Entity target)
    {
        dealDamage(origin, target);
        dealDamage(origin, target);
    }

    private void dealWhirlWindSlash(Entity origin, Entity target)
    {
        int attackCount = Random.Range(2, 6);

        for (int i = 0; i < attackCount; i++)
        {
            dealDamage(origin, target);
        }

    }

    // Similiar function for block instead of damage
    private void giveShield(Entity target)
    {
        target.giveShield(effectVal);
    }

    private void giveArtifact(Entity target)
    {
        target.giveArtifact();
    }

    private void giveVul(Entity target)
    {
        if (!target.hasArtifact())
        {
            target.giveVulnerable(effectVal);
        }
        else
        {
            target.clearArtifact();
        }

    }

    private void giveWeak(Entity target)
    {
        if (!target.hasArtifact())
        {
            target.giveWeak(effectVal);
        }
        else
        {
            target.clearArtifact();
        }

    }

    private void giveFrail (Entity target)
    {
        if (!target.hasArtifact())
        {
            target.giveFrail(effectVal);
        }
        else
        {
            target.clearArtifact();
        }

        
    }

}

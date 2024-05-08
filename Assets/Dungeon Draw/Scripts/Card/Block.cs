using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public int condition;
    public List<Effect> effectList;
    public Block()
    {

    }

    public bool isPlayable()
    {
        
        foreach (Effect e in effectList)
        {
            if (!e.isPlayable())
            {
                return false;
            }
        }

        return true;
    }

    public void dealBlock(Entity origin, Entity target)
    {
        foreach (Effect e in effectList)
        {
            e.dealEffect(origin, target);
            // Debug.Log("Effect dealt");
        }
    }
    //This method is for merging cards
    public int[] copyBlock(int condtion, int cardID)
    {
        int[] ints = new int[1];
        return ints;
    }

    public Block addEffect(int type, int val)
    {
        if (effectList == null)
        {
            effectList = new List<Effect>();
        }
        effectList.Add(new Effect(type, val));

        return this;
    }

    public string showBlock()
    {
        if(effectList == null)
        {
            return " ";
        }

        string temp = "";

        bool first = true;

        foreach (Effect e in effectList)
        {

            if (first)
            {
                first = false;
            }
            else
            {
                temp += ", ";
            }

            temp += e.showEffect();
        }

        return temp;

    }
   
}

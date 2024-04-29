using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int condition;
    public List<Effect> effectList;
    public Block()
    {

    }

    public bool isPlayable()
    {

        bool temp = true;

        foreach (Effect e in effectList)
        {
            if (!e.isPlayable())
            {
                temp = false;
            }
        }

        return temp;
    }

    public void dealBlock(Entity target)
    {
        foreach (Effect e in effectList)
        {
            e.dealEffect(target);
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
        effectList.Add(new Effect(type, val));

        return this;
    }
}

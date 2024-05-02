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

    public void dealBlock(Entity origin, Entity target)
    {
        foreach (Effect e in effectList)
        {
            e.dealEffect(origin, target);
            // Debug.Log("Effect dealt");
        }
    }
    //This method is for merging cards
    //This doesnt do anything at the moment
    public int[] copyBlock(int cardID)
    {
        int[] ints = new int[1];
        return ints;
    }

    public Block addEffect(int type, int val)
    {
        effectList.Add(new Effect(type, val));

        return this;
    }
    public string showBlock()
    {

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

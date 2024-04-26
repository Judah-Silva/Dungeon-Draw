using System;
using UnityEngine;
using UnityEngine.Serialization;

//[Serializable]
public abstract class Effect : ICloneable
{
    public int NbTurns;
    protected Entity Entity = null;//nullable
    
    protected Effect(int nbTurns)
    {
        NbTurns = nbTurns;
    }

    protected Effect(int nbTurns, Entity entity)
    {
        NbTurns = nbTurns;
        Entity = entity;
        Entity.AddEffect(this);
    }
    
    public Effect CreateEffect(Entity entity)
    {
        Effect effect = (Clone() as Effect)!;
        effect.Entity = entity;
        return effect;
    }
    
    public object Clone()
    {
        return MemberwiseClone();
    }
    
    public void Update()
    {
        if (Entity == null)
        {
            Debug.LogError("Entity is null");
            return;
        }
        PlayEffect();
        NbTurns--;
    }

    protected abstract void PlayEffect();
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public String name;
    public int weight;
    public List<GameObject> enemies;
}

using System;
using UnityEngine;

[Serializable]
public class Area
{
    public string name;
    public int cost;
    public bool unlocked = false;

    public Area(string name, int cost, bool unlocked)
    {
        this.name = name;
        this.cost = cost;
        this.unlocked = unlocked;
    }
}

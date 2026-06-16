using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Fish
{
    public string name;
    public int value;
    public string rarity;
    public int rarityWeight;
    public bool caught = false;
    public Fish(string name, int value, string rarity, int rarityWeight)
    {
        this.name = name;
        this.value = value;
        this.rarity = rarity;
        this.rarityWeight = rarityWeight;
    }
}

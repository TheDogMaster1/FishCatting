using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public List<Fish> lakeFishList;
    public List<Fish> seaFishList;
    public List<Fish> forestFishList;
    public List<Area> unlockedAreas;
    public string locatedLocation;
    public List<GameObject> skins;
    public GameObject currentSkin;
}
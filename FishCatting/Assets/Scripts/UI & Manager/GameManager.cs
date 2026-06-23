using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerDataManager))]
[RequireComponent(typeof(CatchFishes))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI fishTextTest;
    public int money;
    public List<Fish> lakeFishList = new();
    public List<Fish> seaFishList = new();
    public List<Fish> forestFishList = new();
    public List<Area> unlockedAreas = new();
    public string locatedLocation;
    [HideInInspector]
    public GameObject createdFish;
    [HideInInspector]
    public bool seenCutscene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        seenCutscene = PlayerDataManager.instance.LoadGame();
    }
    public void ChangeLocation(int locationID)
    {
        if (unlockedAreas != null && locationID < unlockedAreas.Count)
        {
            if (unlockedAreas[locationID].unlocked == true)
            {
                locatedLocation = unlockedAreas[locationID].name;
                if (fishTextTest != null)
                {
                    fishTextTest.text = "already own area!";
                }
            }
            else if (Buy(unlockedAreas[locationID].cost))
            {
                locatedLocation = unlockedAreas[locationID].name;
                unlockedAreas[locationID].unlocked = true;
                if (fishTextTest != null)
                {
                    fishTextTest.text = $"bought new area: {unlockedAreas[locationID].name}!";
                }
                PlayerDataManager.instance.SaveGame();
            }
            else
            {
                if (fishTextTest != null)
                {
                    fishTextTest.text = "area too expensive!";
                }
            }
        }
        else
        {
            Debug.LogWarning("area does not exist, please check if the id used in changing area is also the same as the array index of unlocked areas list in the inspector!");
        }
    }
    public void ChangeStringArea(string AreaName)
    {
        foreach (var area in unlockedAreas)
        {
            if (area.name == AreaName)
            {
                locatedLocation = area.name;
                return;
            }
        }
    }
    public bool Buy(int moneyCost)
    {
        if (moneyCost > money)
        {
            return false;
        }
        else
        {
            money -= moneyCost;
            return true;
        }
    }
    public void RemoveCreatedFish()
    {
        Destroy(createdFish);
    }
    public List<Fish> GetAllFish()
    {
        List<Fish> allFishes = new();
        foreach (var lakeFish in GameManager.instance.lakeFishList)
        {
            allFishes.Add(lakeFish);
        }
        foreach (var seaFish in GameManager.instance.seaFishList)
        {
            allFishes.Add(seaFish);
        }
        foreach (var forestFish in GameManager.instance.forestFishList)
        {
            allFishes.Add(forestFish);
        }
        return allFishes;
    }
}

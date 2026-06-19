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
    PlayerDataManager playerDataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        playerDataManager = GetComponent<PlayerDataManager>();
        playerDataManager.LoadGame();
    }
    public void ChangeLocation(int locationID)
    {
        if (unlockedAreas != null && locationID < unlockedAreas.Count)
        {
            if (unlockedAreas[locationID].unlocked == true)
            {
                locatedLocation = unlockedAreas[locationID].name;
            }
            else if (Buy(unlockedAreas[locationID].cost))
            {
                locatedLocation = unlockedAreas[locationID].name;
                unlockedAreas[locationID].unlocked = true;
                fishTextTest.text = $"bought new area: {unlockedAreas[locationID].name}!";
            }
            else
            {
                fishTextTest.text = "area too expensive!";
            }
        }
        else
        {
            Debug.LogWarning("area does not exist, please check if the id used in changing area is also the same as the array index of unlocked areas list in the inspector!");
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
}

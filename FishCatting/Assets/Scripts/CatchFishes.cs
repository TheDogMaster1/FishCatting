using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(PlayerDataManager))]
public class CatchFishes : MonoBehaviour
{
    public enum FishLocation
    {
        lake,
        sea,
        forest
    }
    public FishLocation fishLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    //catch a fish!
    public void BeginFishing()
    {
        Fish currentfish = CatchFish();
        if (currentfish.caught == true)
        {
            Debug.Log($"caught an {currentfish.name} with value of {currentfish.value} and rarity of {currentfish.rarity} (duplicate)");
        }
        else
        {
            Debug.Log($"caught an new fish called {currentfish.name} with value of {currentfish.value} and rarity of {currentfish.rarity}");
            currentfish.caught = true;
        }
        GameManager.instance.money += currentfish.value;
    }

    public Fish CatchFish()
    {
        //highestnumber = all fish rarityweight combined
        int highestnumber = 1;
        //see at what location we are at for fish
        List<Fish> fishList;
        switch (fishLocation)
        {
            case FishLocation.lake:
                fishList = GameManager.instance.lakeFishList;
                break;

            case FishLocation.sea:
                fishList = GameManager.instance.seaFishList;
                break;

            case FishLocation.forest:
                fishList = GameManager.instance.forestFishList;
                break;

            default:
                fishList = GameManager.instance.lakeFishList;
                break;
        }
        foreach (var fish in fishList)
        {
            highestnumber += fish.rarityWeight;
        }
        //pick a number to see which fish is caught
        int fishNumber = Random.Range(1, highestnumber);
        int currentNumber = 0;
        //see which fish belongs to the number
        foreach (var fish in fishList)
        {
            currentNumber += fish.rarityWeight;
            if (fishNumber <= currentNumber)
            {
                return fish;
            }
        }
        //shouldn't ever happen but is needed for the code to compile
        return null;
    }
    public void ChangeLocation(int locationID)
    {
        if (locationID == 1)
        {
            fishLocation = FishLocation.lake;
        }
        else if (locationID == 2 && GameManager.instance.unlockedAreas.Contains("Sea"))
        {
            fishLocation = FishLocation.sea;
        }
        else if (locationID == 3 && GameManager.instance.unlockedAreas.Contains("Forest"))
        {
            fishLocation =  FishLocation.forest;
        }
        else
        {
            Debug.Log("area not unlocked >:3");
        }
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(PlayerDataManager))]
public class CatchFishes : MonoBehaviour
{
    //catch a fish!
    public void BeginFishing()
    {
        Fish currentfish = CatchFish();
        if (currentfish.caught == true)
        {
            GameManager.instance.fishTextTest.text = $"caught an {currentfish.name} with value of {currentfish.value} and rarity of {currentfish.rarity} (duplicate)";
        }
        else
        {
            GameManager.instance.fishTextTest.text = $"caught an new fish called {currentfish.name} with value of {currentfish.value} and rarity of {currentfish.rarity}";
            currentfish.caught = true;
        }
        GameManager.instance.money += currentfish.value;
    }

    public Fish CatchFish()
    {
        //highestnumber = all fish rarityweight combined, is 1 cause max is exclusive
        int highestnumber = 1;
        //see at what location we are at for fish
        List<Fish> fishList;
        switch (GameManager.instance.locatedLocation)
        {
            case "Lake":
                fishList = GameManager.instance.lakeFishList;
                break;

            case "Sea":
                fishList = GameManager.instance.seaFishList;
                break;

            case "Forest":
                fishList = GameManager.instance.forestFishList;
                break;

            default:
                Debug.LogWarning("it seems that this area is not defined yet, maybe check with the programmers to see if they could add it. or you made a spelling mistake defining the area! defaulting to lake for now.");
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
}

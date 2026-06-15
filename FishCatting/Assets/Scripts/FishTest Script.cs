using System.Collections.Generic;
using UnityEngine;

public class FishTestScript : MonoBehaviour
{
    public List<Fish> fishList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //new() is not done at the top so you can change this later for potential saving/loading
        fishList = new()
        {
            //test fish that are only here for testing, make this in a different script when out of testing and we know what fish to make
            new Fish(name: "CommonFish", value: 50, rarity: "Common", rarityWeight: 100),
            new Fish("UncommonFish", 75, "Uncommon", 50),
            new Fish("RareFish", 200, "Rare", 25),
            new Fish("LegendaryFish", 500, "Legendary", 5)
        };
        //catch a fish!
        Fish currentfish = CatchFish();
        Debug.Log(currentfish.name);
        Debug.Log(currentfish.value);
        Debug.Log(currentfish.rarity);
        Debug.Log(currentfish.rarityWeight);
    }
    Fish CatchFish()
    {
        //highestnumber = all fish rarityweight combined
        int highestnumber = 1;
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
            if(fishNumber <= currentNumber)
            {
                return fish;
            }
        }
        //shouldn't ever happen but is needed for the code to compile
        return null;
    }
}

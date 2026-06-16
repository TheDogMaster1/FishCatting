using System.Collections.Generic;
using UnityEngine;

public class FishTestScript : MonoBehaviour
{
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
        Debug.Log(GameManager.instance.money);
        foreach (var fish in GameManager.instance.fishList)
        {
            highestnumber += fish.rarityWeight;
        }
        //pick a number to see which fish is caught
        int fishNumber = Random.Range(1, highestnumber);
        int currentNumber = 0;
        //see which fish belongs to the number
        foreach (var fish in GameManager.instance.fishList)
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

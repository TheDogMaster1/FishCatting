using System.IO;
using UnityEngine;
[RequireComponent(typeof(GameManager))]
public class PlayerDataManager : MonoBehaviour
{
    public void SaveGame()
    {
        //we put the data into a json called playerData.json for loading next time the user plays.
        PlayerData playerData = new()
        {
            money = GameManager.instance.money,
            lakeFishList = GameManager.instance.lakeFishList,
            seaFishList = GameManager.instance.seaFishList,
            forestFishList = GameManager.instance.forestFishList,
            unlockedAreas = GameManager.instance.unlockedAreas
        };
        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData.json";
        File.WriteAllText(path, json);
    }
    public void LoadGame()
    {
        //we load the data from the json file called playerData.json unless it's not found, in which case we do a debug log.
        //not having the save file only means the user hasn't played before or deleted it! this should NOT stop you from playing.
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);
            GameManager.instance.money = loadedData.money;
            if (loadedData.lakeFishList != null && GameManager.instance.lakeFishList.Count < 1)
            {
                GameManager.instance.lakeFishList = loadedData.lakeFishList;
            }
            if (loadedData.seaFishList != null && GameManager.instance.seaFishList.Count < 1)
            {
                GameManager.instance.seaFishList = loadedData.seaFishList;
            }
            if (loadedData.forestFishList != null && GameManager.instance.forestFishList.Count < 1)
            {
                GameManager.instance.forestFishList = loadedData.forestFishList;
            }
            LoadFishes();
            GameManager.instance.unlockedAreas = loadedData.unlockedAreas;
        }
        else
        {
            LoadFishes();
            Debug.Log("File not found!");
        }
    }
    public void NewGame()
    {
        foreach (var fish in GameManager.instance.lakeFishList)
        {
            fish.caught = false;
        }
        foreach (var fish in GameManager.instance.seaFishList)
        {
            fish.caught = false;
        }
        foreach (var fish in GameManager.instance.forestFishList)
        {
            fish.caught = false;
        }
        PlayerData playerData = new()
        {
            money = 0,
            lakeFishList = null,
            seaFishList = null,
            forestFishList = null,
            unlockedAreas = new()
        };
        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData.json";
        File.WriteAllText(path, json);
        LoadGame();
        SaveGame();
    }
    public void LoadFishes()
    {
        //eventually this entire class shouldn't be in the script anymore when we don't have test fish
        Debug.Log("loading fishies :3");
        if (GameManager.instance.lakeFishList == null || GameManager.instance.lakeFishList.Count < 1)
        {
            GameManager.instance.lakeFishList = new()
            {
                //test fish list change this to any fish you want
                new Fish(name: "CommonFish", value: 50, rarity: "Common", rarityWeight: 100),
                new Fish("UncommonFish", 75, "Uncommon", 50),
                new Fish("RareFish", 200, "Rare", 25),
                new Fish("LegendaryFish", 500, "Legendary", 5)
            };
        }
        if (GameManager.instance.seaFishList == null || GameManager.instance.seaFishList.Count < 1)
        {
            GameManager.instance.seaFishList = new()
            {
                //test fish list change this to any fish you want
                new Fish(name: "CommonFishSea", value: 50, rarity: "Common", rarityWeight: 100),
                new Fish("UncommonFishSea", 75, "Uncommon", 50),
                new Fish("RareFishSea", 200, "Rare", 25),
                new Fish("LegendaryFishSea", 500, "Legendary", 5)
            };
        }
        if (GameManager.instance.forestFishList == null || GameManager.instance.forestFishList.Count < 1)
        {
            GameManager.instance.forestFishList = new()
            {
                //test fish list change this to any fish you want
                new Fish(name: "CommonFishForest", value: 50, rarity: "Common", rarityWeight: 100),
                new Fish("UncommonFishForest", 75, "Uncommon", 50),
                new Fish("RareFishForest", 200, "Rare", 25),
                new Fish("LegendaryFishForest", 500, "Legendary", 5)
            };
        }

    }
}

using System.Data.SqlTypes;
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
            fishList = GameManager.instance.fishList,
            unlockedAreas = GameManager.instance.unlockedAreas
        };
        Debug.Log(playerData.fishList);
        foreach (var fish in playerData.fishList)
        {
            Debug.Log(fish.name);
            Debug.Log(fish.caught);
        }
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
            if (loadedData.fishList != null && loadedData.fishList.Count > 0)
            {
                GameManager.instance.fishList = loadedData.fishList;
            }
            else
            {
                Debug.Log("loading fishies :3");
                GameManager.instance.fishList = new()
                {
                    //test fish list change this to any fish you want
                    new Fish(name: "CommonFish", value: 50, rarity: "Common", rarityWeight: 100),
                    new Fish("UncommonFish", 75, "Uncommon", 50),
                    new Fish("RareFish", 200, "Rare", 25),
                    new Fish("LegendaryFish", 500, "Legendary", 5)
                };
            }
            GameManager.instance.unlockedAreas = loadedData.unlockedAreas;
        }
        else
        {
            Debug.Log("File not found!");
        }
    }
    public void NewGame()
    {
        PlayerData playerData = new()
        {
            money = 0,
            fishList = null,
            unlockedAreas = new()
        };
        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData.json";
        File.WriteAllText(path, json);
        LoadGame();
    }
}

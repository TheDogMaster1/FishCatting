using System.IO;
using UnityEngine;
[RequireComponent(typeof(GameManager))]
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;
    void Awake()
    {
        instance = this;
    }
    public void SaveGame()
    {
        //we put the data into a json called playerData.json for loading next time the user plays.
        PlayerData playerData = new()
        {
            money = GameManager.instance.money,
            lakeFishList = GameManager.instance.lakeFishList,
            seaFishList = GameManager.instance.seaFishList,
            forestFishList = GameManager.instance.forestFishList,
            unlockedAreas = GameManager.instance.unlockedAreas,
            locatedLocation = GameManager.instance.locatedLocation,
            customSkins = GameManager.instance.customSkins,
            currentSkin = GameManager.instance.currentSkin
        };
        string json = JsonUtility.ToJson(playerData);
#if (UNITY_WEBGL && !UNITY_EDITOR)
        string path = System.IO.Path.Combine("idbfs", Application.productName);
        if(!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = System.IO.Path.Combine(path, "saveDataFishing");
#else
        string path = Application.persistentDataPath + "/playerData.json";
#endif
        File.WriteAllText(path, json);
    }
    public bool LoadGame()
    {
        //we load the data from the json file called playerData.json unless it's not found, in which case we do a debug log.
        //not having the save file only means the user hasn't played before or deleted it! this should NOT stop you from playing.
#if (UNITY_WEBGL && !UNITY_EDITOR)
        string path = System.IO.Path.Combine("idbfs", Application.productName);
        path = System.IO.Path.Combine(path, "saveDataFishing");
#else
        string path = Application.persistentDataPath + "/playerData.json";
#endif
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);
            GameManager.instance.money = loadedData.money;
            if (loadedData.lakeFishList != null && loadedData.lakeFishList.Count != 0)
            {
                GameManager.instance.lakeFishList = loadedData.lakeFishList;
            }
            if (loadedData.seaFishList != null && loadedData.seaFishList.Count != 0)
            {
                GameManager.instance.seaFishList = loadedData.seaFishList;
            }
            if (loadedData.forestFishList != null && loadedData.forestFishList.Count != 0)
            {
                GameManager.instance.forestFishList = loadedData.forestFishList;
            }
            if (loadedData.unlockedAreas != null && loadedData.unlockedAreas.Count != 0)
            {
                GameManager.instance.unlockedAreas = loadedData.unlockedAreas;
            }
            if (loadedData.locatedLocation == null)
            {
                GameManager.instance.locatedLocation = "Lake";
            }
            else
            {
                GameManager.instance.locatedLocation = loadedData.locatedLocation;
            }
            if (loadedData.customSkins != null)
            {
                GameManager.instance.customSkins = loadedData.customSkins;
            }
            if (loadedData.currentSkin != null)
            {
                GameManager.instance.currentSkin = loadedData.currentSkin;
            }
            return true;
        }
        else
        {
            Debug.Log("File not found!");
            return false;
        }
    }
    public void NewGame()
    {
        //reset all savedata
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
        foreach (var area in GameManager.instance.unlockedAreas)
        {
            if (area.name != "Lake")
            {
                area.unlocked = false;
            }
        }
        PlayerData playerData = new()
        {
            money = 0,
            lakeFishList = null,
            seaFishList = null,
            forestFishList = null,
            unlockedAreas = null,
            locatedLocation = "Lake",
            customSkins = GameManager.instance.customSkins,
            currentSkin = GameManager.instance.customSkins[0]
        };
        if (playerData.customSkins != null)
        {
            foreach (var customskin in playerData.customSkins)
            {
                customskin.isUnlocked = false;
            }
            playerData.customSkins[0].isUnlocked = true;
        }
        string json = JsonUtility.ToJson(playerData);
#if (UNITY_WEBGL && !UNITY_EDITOR)
        string path = System.IO.Path.Combine("idbfs", Application.productName);
                if(!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = System.IO.Path.Combine(path, "saveDataFishing");
#else
        string path = Application.persistentDataPath + "/playerData.json";
#endif
        File.WriteAllText(path, json);
        LoadGame();
        SaveGame();
    }
}

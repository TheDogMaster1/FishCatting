using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDataManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int money;
    public List<Fish> lakeFishList = new();
    public List<Fish> seaFishList = new();
    public List<Fish> forestFishList = new();
    public List<string> unlockedAreas = new();
    PlayerDataManager playerDataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        playerDataManager = GetComponent<PlayerDataManager>();
        //unlocking all areas for testing by id Lake should always be unlocked but doesn't break anything if not unlocked
        unlockedAreas.Add("Lake");
        unlockedAreas.Add("Sea");
        unlockedAreas.Add("Forest");
    }
    void Start()
    {
        playerDataManager.LoadGame();
        if (lakeFishList == null || lakeFishList.Count == 0)
        {
            Debug.Log("no fishies >:(");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

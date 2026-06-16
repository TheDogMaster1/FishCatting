using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDataManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int money;
    public List<Fish> fishList = new();
    public List<int> unlockedAreas = new();
    PlayerDataManager playerDataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        playerDataManager = GetComponent<PlayerDataManager>();
    }
    void Start()
    {
        playerDataManager.LoadGame();
        if (fishList == null || fishList.Count == 0)
        {
            Debug.Log("no fishies >:(");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

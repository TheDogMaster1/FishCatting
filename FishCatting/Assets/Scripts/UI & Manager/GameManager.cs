using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerDataManager))]
[RequireComponent(typeof(CatchFishes))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI fishTextTest;
    public TextMeshProUGUI coinText;
    public int money;
    public List<Fish> lakeFishList = new();
    public List<Fish> seaFishList = new();
    public List<Fish> forestFishList = new();
    public List<Area> unlockedAreas = new();
    public string locatedLocation;
    public List<GameObject> skins;
    public GameObject startingSkin;
    [HideInInspector]
    public GameObject currentSkin;
    private Transform morgana;
    [HideInInspector]
    public GameObject createdFish;
    [HideInInspector]
    public bool seenCutscene;
    private PlayerDataManager playerDataManager;
    private CastingLine castingLine;
    private FishCatching catching;
    private FishingMinigame fishingMinigame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        playerDataManager = GetComponent<PlayerDataManager>();
        seenCutscene = playerDataManager.LoadGame();
        castingLine = GetComponent<CastingLine>();
        catching = GetComponent<FishCatching>();
        fishingMinigame = GetComponent<FishingMinigame>();
    }
    private void Start()
    {
        if (currentSkin != null && FindAnyObjectByType<Morgana>() != null)
        {
            morgana = FindAnyObjectByType<Morgana>().transform;
            var morganaSkin = Instantiate(currentSkin, Vector3.zero, Quaternion.identity, morgana);
            morganaSkin.transform.localPosition = Vector3.zero;
            Animator catAni = morganaSkin.GetComponent<Animator>();
            castingLine.GetAnimator(catAni);
            catching.GetCatAni(catAni);
            fishingMinigame.GetCatAni(catAni);

        }
    }
    void Update()
    {
        if (coinText != null)
        {
            coinText.text = $"money: {money}";
        }
    }
    public void ChangeLocation(int locationID)
    {
        if (unlockedAreas != null && locationID < unlockedAreas.Count)
        {
            if (unlockedAreas[locationID].unlocked == true)
            {
                locatedLocation = unlockedAreas[locationID].name;
                playerDataManager.SaveGame();
                SceneManager.LoadScene(unlockedAreas[locationID].name);
            }
            else if (Buy(unlockedAreas[locationID].cost))
            {
                locatedLocation = unlockedAreas[locationID].name;
                unlockedAreas[locationID].unlocked = true;
                if (fishTextTest != null)
                {
                    fishTextTest.text = $"bought new area: {unlockedAreas[locationID].name}!";
                }
                playerDataManager.SaveGame();
                SceneManager.LoadScene(unlockedAreas[locationID].name);
            }
            else
            {
                if (fishTextTest != null)
                {
                    fishTextTest.text = "area too expensive!";
                }
            }
        }
        else
        {
            Debug.LogWarning("area does not exist, please check if the id used in changing area is also the same as the array index of unlocked areas list in the inspector!");
        }
    }
    public void ChangeStringArea(string AreaName)
    {
        foreach (var area in unlockedAreas)
        {
            if (area.name == AreaName)
            {
                locatedLocation = area.name;
                return;
            }
        }
        Debug.Log("area not found");
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
    public List<Fish> GetAllFish()
    {
        List<Fish> allFishes = new();
        foreach (var lakeFish in GameManager.instance.lakeFishList)
        {
            allFishes.Add(lakeFish);
        }
        foreach (var seaFish in GameManager.instance.seaFishList)
        {
            allFishes.Add(seaFish);
        }
        foreach (var forestFish in GameManager.instance.forestFishList)
        {
            allFishes.Add(forestFish);
        }
        return allFishes;
    }
}

using UnityEngine;

public class BobAnimationEvents : MonoBehaviour
{
    private FishCatching FishCatching;
    private FishingMinigame fishingMinigame;

    private void Start()
    {
        fishingMinigame = FindAnyObjectByType<FishingMinigame>();
        FishCatching = FindAnyObjectByType<FishCatching>();
    }
    public void FailedFish()
    {
        if (fishingMinigame.BoolMiniGame()) return;
        FishCatching.SetBittenBool(false);
        FishCatching.SetFishTime();
        StartCoroutine(FishCatching.StartFishing());
        Debug.Log("Fail :(");
    }

    public void StopTheCorountines()
    {
        StopAllCoroutines();
    }
}

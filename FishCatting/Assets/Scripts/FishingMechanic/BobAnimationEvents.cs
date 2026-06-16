using UnityEngine;

public class BobAnimationEvents : MonoBehaviour
{
    private FishCatching FishCatching;

    private void Start()
    {
        FishCatching = FindAnyObjectByType<FishCatching>();
    }
    public void FailedFish()
    {
        FishCatching.SetBittenBool(false);
        FishCatching.SetFishTime();
        StartCoroutine(FishCatching.CastingLine());
        Debug.Log("Fail :(");
    }
}

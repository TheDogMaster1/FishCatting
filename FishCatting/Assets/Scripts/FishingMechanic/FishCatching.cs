using System.Collections;
using TMPro;
using UnityEngine;

public class FishCatching : MonoBehaviour
{
    private CastingLine casting;
    private CatchFishes catchFish;
    private FishingMinigame minigame;
    private GameObject bobber;
    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI debugText;
    private int fakebitesHappened;

    [Header("Cast Wait times")]
    [SerializeField]
    private int minWait = 3;
    [SerializeField]
    private int maxWait = 10;

    [Header("FakeBobWait")]
    [SerializeField]
    private int minFakeWait = 1;
    [SerializeField]
    private int maxFakeWait = 10;

    private int fakeBitesAmount;

    private bool fishBitten = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        casting = GetComponent<CastingLine>();
        bobber = casting.GetBobber();
        animator = bobber.GetComponentInChildren<Animator>();
        catchFish = GetComponent<CatchFishes>();
        minigame = GetComponent<FishingMinigame>();
    }

    private void Update()
    {
        if (debugText != null) debugText.text = $"amount of fakebites: {fakeBitesAmount}  fakebites already happened: {fakebitesHappened}";
    }

    public IEnumerator CastingLine()
    {
        while (!fishBitten)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait + 1));
            for (int i = 0; i < fakeBitesAmount; i++)
            {
                yield return FakeBob();
            }
            FishBite();
            fishBitten = true;
        }
    }

    private IEnumerator FakeBob()
    {
        animator.SetTrigger("FakeBite");
        fakebitesHappened++;
        yield return new WaitForSeconds(Random.Range(minFakeWait, maxFakeWait + 1));
    }

    public void ReelIn()
    {
        fakebitesHappened = 0;
        if (fishBitten)
        {
            Debug.Log("Yay Yippee you did it yaayayayayay");
            //start minigame
            minigame.StartMiniGame();
            //catchFish.BeginFishing();
            fishBitten = false;
        }
        else
        {
            StartCoroutine(casting.ThrowReel(casting.GetUnCassed().position, casting.GetBobber().transform.position, casting.GetReelInSpeed(), casting.GetReelInAngle()));
            Debug.Log("no fish lol");
        }
    }

    private void FishBite()
    {
        animator.SetTrigger("RealBite");
    }

    public void SetFishTime()
    {
        fakeBitesAmount = Random.Range(0, 6);
    }

    public void SetBittenBool(bool pBool)
    {
        fishBitten = pBool;
    }
}

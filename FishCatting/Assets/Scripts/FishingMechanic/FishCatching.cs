using System.Collections;
using TMPro;
using UnityEngine;

public class FishCatching : MonoBehaviour
{
    private CastingLine casting;
    private CatchFishes catchFish;
    private FishingMinigame minigame;
    private GameObject bobber;
    private Animator bobAnimator;
    private Animator catAnimator;
    [SerializeField]
    private BobAnimationEvents bobAnimationEvents;

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
    [SerializeField]
    private int maxFakeBites = 6;

    private int fakeBitesAmount;

    private bool fishBitten = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        casting = GetComponent<CastingLine>();
        //catAnimator = casting.GetAnimator();
        bobber = casting.GetBobber();
        bobAnimator = bobber.GetComponentInChildren<Animator>();
        catchFish = GetComponent<CatchFishes>();
        minigame = GetComponent<FishingMinigame>();
    }

    private void Update()
    {
        if (debugText != null) debugText.text = $"amount of fakebites: {fakeBitesAmount}  fakebites already happened: {fakebitesHappened}";
    }

    public IEnumerator StartFishing()
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
        bobAnimator.SetTrigger("FakeBite");
        fakebitesHappened++;
        yield return new WaitForSeconds(Random.Range(minFakeWait, maxFakeWait + 1));
    }

    public void ReelIn()
    {
        bobAnimationEvents.StopTheCorountines();
        fakebitesHappened = 0;
        if (fishBitten)
        {
            casting.SetCastingbool(true);
            Debug.Log("Yay Yippee you did it yaayayayayay");
            //start minigame
            catAnimator.SetTrigger("CaughtFish");
            minigame.StartMiniGame();
            //catchFish.BeginFishing();
            fishBitten = false;
        }
        else
        {
            StartCoroutine(ThrowCastBack());
        }
    }

    private IEnumerator ThrowCastBack()
    {
        catAnimator.SetTrigger("Casting");
        Vector3 uncasPos = casting.GetUnCassed().position;
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(casting.ThrowReel(uncasPos, casting.GetBobber().transform.position, casting.GetReelInSpeed(), casting.GetReelInAngle()));
        Debug.Log("no fish lol");
    }

    private void FishBite()
    {
        bobAnimator.SetTrigger("RealBite");
    }

    public void SetFishTime()
    {
        fakebitesHappened = 0;
        fakeBitesAmount = Random.Range(0, maxFakeBites);
    }

    public void SetBittenBool(bool pBool)
    {
        fishBitten = pBool;
    }
    public void GetCatAni(Animator pAni)
    {
        catAnimator = pAni;
    }

    public bool GetFishBitten()
    {
        return fishBitten;
    }
}

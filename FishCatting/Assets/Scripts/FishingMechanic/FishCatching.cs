using System.Collections;
using UnityEngine;

public class FishCatching : MonoBehaviour
{
    private CastingLine casting;
    private FishTestScript fishTest;
    private GameObject bobber;
    private Animator animator;


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
        fishTest = GetComponent<FishTestScript>();
    }

    public IEnumerator CastingLine()
    {
        Debug.Log("Cast!!!");
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
        Debug.Log("boo");
        yield return new WaitForSeconds(Random.Range(minFakeWait, maxFakeWait + 1));
    }

    public void ReelIn()
    {
        StopAllCoroutines();
        if (fishBitten)
        {
            Debug.Log("Yay Yippee you did it yaayayayayay");
            //start minigame
            fishTest.BeginFishing();
            ResetBobber();
            fishBitten = false;
        }
        else
        {
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
        Debug.Log(fakeBitesAmount);
    }

    public void SetBittenBool(bool pBool)
    {
        fishBitten = pBool;
    }

    private void ResetBobber()
    {
        foreach (Transform child in bobber.transform)
        {
            child.position = Vector3.zero;
        }
    }
}

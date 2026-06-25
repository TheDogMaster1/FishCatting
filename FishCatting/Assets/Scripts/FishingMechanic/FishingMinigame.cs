using UnityEngine;
using UnityEngine.UI;

public class FishingMinigame : MonoBehaviour
{
    [SerializeField]
    private Slider playerSlider;
    [SerializeField]
    private Slider fishslider;
    [SerializeField]
    private Slider timerSlider;
    [SerializeField]
    private GameObject miniGame;
    [SerializeField]
    private Transform fishGetTransform;
    private FishGetAnimation fishGetRotate;
    private Animator fishGetAnimator;

    [Header("Player settings")]
    [SerializeField]
    private float upSpeedChange = 1;
    [SerializeField]
    private float downSpeedChange = 1;

    [SerializeField]
    private float maxUpSpeed = 1;
    [SerializeField]
    private float maxDownSpeed = 1;

    private float usedSpeed = 0;

    [Header("Fish slider settings")]
    [SerializeField]
    private RectTransform fishWinArea;

    [SerializeField]
    private FishingMinigameSettings[] miniGameSettings;

    private FishingMinigameSettings usedSettings;

    private float timerToBegin = 0;

    private float destinationValue = 0;
    private float fishTimer = 0;
    private float fishSwitchTime = 0;

    private bool inMiniGame = false;
    private bool inGetAnimation = false;

    private CatchFishes catchFish;
    private CastingLine casting;
    [SerializeField]
    private Animator catAnimator;
    private Fish fish = new("newfish", 0, Fish.Rarity.Common, 100);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        catchFish = GetComponent<CatchFishes>();
        casting = GetComponent<CastingLine>();
        //catAnimator = casting.GetAnimator();
        if (fishGetTransform != null)
        {
            fishGetRotate = fishGetTransform.GetComponent<FishGetAnimation>();
            fishGetAnimator = fishGetTransform.GetComponent<Animator>();
        }
        else Debug.LogWarning("You forgot the fishGetAnimation Transform");
        //StartMiniGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inMiniGame) return;
        timerToBegin += Time.deltaTime;
        PlayerSlider();
        FishSlider();
        if (timerToBegin > 0.5) TimerSlider();
    }

    public void StartMiniGame()
    {
        ResetMinigame();
        miniGame.SetActive(true);
        inMiniGame = true;
    }

    private void PlayerSlider()
    {
        if (Input.touchCount > 0)
        {
            usedSpeed += upSpeedChange * Time.deltaTime;
        }
        else if (Input.GetMouseButton(0) && Input.touchCount == 0)
        {
            usedSpeed += upSpeedChange * Time.deltaTime;
        }
        else
        {
            usedSpeed -= downSpeedChange * Time.deltaTime;
        }

        if (playerSlider.value == playerSlider.maxValue) usedSpeed = Mathf.Clamp(usedSpeed, -maxDownSpeed, 0);
        else if (playerSlider.value == playerSlider.minValue) usedSpeed = Mathf.Clamp(usedSpeed, 0, maxUpSpeed);
        else usedSpeed = Mathf.Clamp(usedSpeed, -maxDownSpeed, maxUpSpeed);
        //Debug.Log(usedSpeed);
        playerSlider.value += usedSpeed * Time.deltaTime;
    }

    private void FishSlider()
    {
        fishTimer += Time.deltaTime;
        if (fishTimer > fishSwitchTime || Input.GetKeyDown(KeyCode.Space))
        {
            destinationValue = Random.Range(fishslider.minValue + (float)usedSettings.fishWinSize / 200, fishslider.maxValue - (float)usedSettings.fishWinSize / 200);
            fishSwitchTime = Random.Range(usedSettings.minTimeChange, usedSettings.maxTimeChange);
            fishTimer = 0;
            //Debug.Log(destinationValue);
            //Debug.Log(new Vector2(fishslider.minValue + (float)fishWinSize / 200, fishslider.maxValue - (float)fishWinSize / 200));
        }
        if (Mathf.Abs(destinationValue - fishslider.value) > 0.01) fishslider.value += usedSettings.fishSpeed * Mathf.Sign(destinationValue - fishslider.value) * Time.deltaTime;
    }

    private void TimerSlider()
    {
        if (playerSlider.value + 0.05f > fishslider.value - (float)usedSettings.fishWinSize / 200 && playerSlider.value - 0.05f < fishslider.value + (float)usedSettings.fishWinSize / 200)
        {
            timerSlider.value += usedSettings.upTimerSpeed * Time.deltaTime;
            //Debug.Log("winning");
        }
        else timerSlider.value -= usedSettings.downTimerSpeed * Time.deltaTime;
        if (timerSlider.value == 0)
        {
            FinishMiniGame(false);
        }
        else if (timerSlider.value == 1)
        {
            FinishMiniGame(true);
        }
    }

    private void ResetMinigame()
    {
        fish = catchFish.CatchFish();
        SetSettings();
        fishWinArea.sizeDelta = new Vector2(0, usedSettings.fishWinSize);
        playerSlider.value = 0;
        usedSpeed = 0;
        timerToBegin = 0;
        fishTimer = 0;
        fishSwitchTime = 1f;
        destinationValue = fishslider.minValue + (float)usedSettings.fishWinSize / 200;
        fishslider.value = destinationValue;
        timerSlider.value = usedSettings.beginValue;
    }

    private void SetSettings()
    {
        foreach (var settings in miniGameSettings)
        {
            if (settings.rarity == fish.rarity)
            {
                usedSettings = settings;
                return;
            }
        }
    }

    private void FinishMiniGame(bool ifWon)
    {
        inMiniGame = false;
        if (ifWon)
        {
            //Debug.Log("Win :D");
            if (!fish.caught && fishGetTransform != null && fish.fishModel != null)
            {
                Instantiate(fish.fishModel, fishGetTransform.position, Quaternion.identity, fishGetTransform);
                fishGetRotate.GetChild();
                fishGetAnimator.SetTrigger("Start");
                inGetAnimation = true;
            }
            catAnimator.SetTrigger("Won");
            catchFish.FishCatched(fish);
        }
        else
        {
            catAnimator.SetTrigger("Lost");
            Debug.Log("Lose D:"); // TODO: add something to show that you lost
        }
        StartCoroutine(casting.ThrowReel(casting.GetUnCassed().position, casting.GetBobber().transform.position, casting.GetReelInSpeed(), casting.GetReelInAngle()));
        casting.SetCastingbool(false);
        miniGame.SetActive(false);
    }

    public bool BoolMiniGame()
    {
        return inMiniGame;
    }

    public void SetGetAnimationBool(bool pBool)
    {
        inGetAnimation = pBool;
    }

    public void GetCatAni(Animator pAni)
    {
        catAnimator = pAni;
    }

    public bool BoolGetAnimation()
    {
        return inGetAnimation;
    }
}

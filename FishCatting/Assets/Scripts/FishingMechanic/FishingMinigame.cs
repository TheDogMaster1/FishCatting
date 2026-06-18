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
    public float testSetSettings;

    private float timerToBegin = 0;

    private float destinationValue = 0;
    private float fishTimer = 0;
    private float fishSwitchTime = 0;

    [SerializeField]
    private bool inMiniGame = false;

    private CatchFishes catchFish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        catchFish = GetComponent<CatchFishes>();
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
        SetSettings();
        fishWinArea.sizeDelta = new Vector2(0, usedSettings.fishWinSize);
        playerSlider.value = 0;
        timerToBegin = 0;
        fishTimer = 0;
        fishSwitchTime = 1f;
        destinationValue = fishslider.minValue + (float)usedSettings.fishWinSize / 200;
        fishslider.value = destinationValue;
        timerSlider.value = usedSettings.beginValue;
    }

    private void SetSettings()
    {
        switch (testSetSettings)
        {
            case 0:
                usedSettings = miniGameSettings[0];
                break;
        }
    }

    private void FinishMiniGame(bool ifWon)
    {
        ResetMinigame();
        inMiniGame = false;
        if (ifWon)
        {
            //Debug.Log("Win :D");
            catchFish.BeginFishing();
        }
        else Debug.Log("Lose D:"); // TODO: add something to show that you lost
        miniGame.SetActive(false);
    }

    public bool BoolMiniGame()
    {
        return inMiniGame;
    }
}

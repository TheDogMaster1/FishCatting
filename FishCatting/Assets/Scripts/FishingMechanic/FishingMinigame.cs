using UnityEngine;
using UnityEngine.UI;

public class FishingMinigame : MonoBehaviour
{
    [SerializeField]
    private Slider playerSlider;
    [SerializeField]
    private Slider fishslider;

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
    private float fishSpeed = 1;
    [SerializeField]
    private int minTimeChange = 10;
    [SerializeField]
    private int maxTimeChange = 30;

    private float destinationValue = 0;
    private float fishTimer = 0;
    private float fishSwitchTime = 0;

    [SerializeField]
    private bool inMiniGame = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerSlider();
        FishSlider();
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
            destinationValue = Random.Range(fishslider.minValue, fishslider.maxValue);
            fishSwitchTime = Random.Range(minTimeChange, maxTimeChange);
            fishTimer = 0;
            Debug.Log(destinationValue);
        }
        if (Mathf.Abs(destinationValue - fishslider.value) > 0.01) fishslider.value += fishSpeed * Mathf.Sign(destinationValue - fishslider.value) * Time.deltaTime;
    }

    public bool BoolMiniGame()
    {
        return inMiniGame;
    }
}

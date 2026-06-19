using UnityEngine;

public class CastingLine : MonoBehaviour
{
    [SerializeField]
    private GameObject bobber;
    [SerializeField]
    private GameObject bobberModel;

    private GetClickPosition clickPosition;
    private FishCatching fishCatching;

    private Touch tap;

    private bool bobberCasted = false;

    private FishingMinigame minigame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickPosition = new();
        fishCatching = GetComponent<FishCatching>();
        minigame = GetComponent<FishingMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (minigame.BoolMiniGame()) return;
        if (Input.touchCount > 0)
        {
            tap = Input.GetTouch(0);
            if (tap.phase == TouchPhase.Ended)
            {
                CastBobber();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CastBobber();
        }
    }

    private void CastBobber()
    {
        if (clickPosition.GetTapPos("AllowCast") == Vector3.zero) return;
        bobber.SetActive(!bobber.activeSelf);
        bobberCasted = !bobberCasted;
        if (!bobber.activeSelf)
        {
            bobber.transform.position = Vector3.zero;
            bobberModel.transform.position = Vector3.zero;
            StopAllCoroutines();
            fishCatching.ReelIn();
            return;
        }
        fishCatching.SetFishTime();
        StartCoroutine(fishCatching.CastingLine());
        bobber.transform.position = clickPosition.GetTapPos("AllowCast");
        Debug.Log("Position " + clickPosition.GetTapPos("AllowCast"));
    }

    public bool GetCastingbool()
    {
        return bobberCasted;
    }

    public GameObject GetBobber()
    {
        return bobber;
    }
}
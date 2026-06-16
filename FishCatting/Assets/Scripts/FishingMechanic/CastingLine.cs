using UnityEngine;

public class CastingLine : MonoBehaviour
{
    [SerializeField]
    private GameObject bobber;

    private GetClickPosition clickPosition;
    private FishCatching fishCatching;

    private Touch tap;

    private bool bobberCasted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickPosition = new();
        fishCatching = GetComponent<FishCatching>();
    }

    // Update is called once per frame
    void Update()
    {
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
        bobber.SetActive(!bobber.activeSelf);
        bobberCasted = !bobberCasted;
        fishCatching.SetFishTime();
        if (!bobber.activeSelf)
        {
            bobber.transform.position = Vector3.zero;
            fishCatching.ReelIn();
            return;
        }
        StartCoroutine(fishCatching.CastingLine());
        bobber.transform.position = clickPosition.GetTapPos();
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
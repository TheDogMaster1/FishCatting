using UnityEngine;

public class PosTest : MonoBehaviour
{
    [SerializeField]
    private GameObject testCubePrefab;

    private GetClickPosition clickPosition;

    private Touch tap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickPosition = new();
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
        testCubePrefab.SetActive(!testCubePrefab.activeSelf);
        if (!testCubePrefab.activeSelf)
        {
            testCubePrefab.transform.position = Vector3.zero;
            return;
        }
        testCubePrefab.transform.position = clickPosition.GetTapPos();
    }
}
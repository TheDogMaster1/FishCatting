using UnityEngine;

public class FishingLine : MonoBehaviour
{
    [SerializeField]
    private Transform Bobber;

    private LineRenderer fishLineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishLineRenderer = GetComponent<LineRenderer>();
        Bobber = FindAnyObjectByType<BobAnimationEvents>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Bobber != null)
        {
            fishLineRenderer.SetPosition(0, transform.position);
            fishLineRenderer.SetPosition(1, Bobber.position);
        }
    }
}

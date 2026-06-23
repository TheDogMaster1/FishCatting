using UnityEngine;

public class FishGetAnimation : MonoBehaviour
{
    [SerializeField]
    private float Speed = 1f;

    private FishingMinigame minigame;
    private GameObject child;
    void Start()
    {
        minigame = FindAnyObjectByType<FishingMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (child != null)
        {
            child.transform.Rotate(new Vector3(0, Speed, 0) * Time.deltaTime);
        }
    }

    public void GetChild()
    {
        child = transform.GetChild(0).gameObject;
        Debug.Log(child.name);
    }

    public void StopAnimation()
    {
        Destroy(child);
        minigame.SetGetAnimationBool(false);
    }
}

using Unity.Mathematics;
using UnityEngine;

public class Inspectable : MonoBehaviour
{
    private Camera camera;

    void Start()
    {
        camera = FindFirstObjectByType<Camera>();
    }
    public void showFish(GameObject fish)
    {
        Instantiate(fish, camera.transform.position, transform.rotation);
    }
}

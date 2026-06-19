using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Inspectable : MonoBehaviour, IPointerDownHandler
{
    private Camera mainCamera;
    private List<Fish> allFishes = new();
    public GameObject closeButton;
    void Start()
    {
        mainCamera = FindFirstObjectByType<Camera>();
        AddPhysicsRaycaster();
        allFishes = GameManager.instance.GetAllFish();
    }

    public GameObject ShowFish(Fish fish)
    {
        if (fish.fishModel != null)
        {
            closeButton.SetActive(true);
            return Instantiate(fish.fishModel, mainCamera.transform.position + new Vector3(0, 0, 4), transform.rotation);
        }
        else
        {
            Debug.LogWarning("fish model not made yet! please add it in the inspector, ok thanks :3");
            return null;
        }
    }
    private void AddPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = FindFirstObjectByType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var fish in allFishes)
        {
            if (gameObject.name == fish.name && GameManager.instance.createdFish == null)
            {
                if((GameManager.instance.createdFish = ShowFish(fish)) != null)
                {
                    GameManager.instance.createdFish.AddComponent<RotateInspection>();
                }
                return;
            }
            else if (GameManager.instance.createdFish != null)
            {
                return;
            }
        }
        Debug.LogWarning("found no fish >:( please ensure that the objectname is the same as one of the fishes in gamemanager");
        return;
    }

}

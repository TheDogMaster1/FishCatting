using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CheckCaught : MonoBehaviour
{
    private List<Fish> allFish;
    private Image image;
    public Sprite unknownImage;
    public Sprite fishImage;
    public GameObject closeButton;
    public GameObject darkener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        allFish = GameManager.instance.GetAllFish();
    }
    void Update()
    {
        //keep this in update just in case for problems with start

        if (IsCaught(this.name))
        {
            image.sprite = fishImage;
            if (GetComponent<Inspectable>() == null)
            {
                this.AddComponent<Inspectable>();
                GetComponent<Inspectable>().closeButton = closeButton;
                GetComponent<Inspectable>().darkener = darkener;
            }
        }
        else
        {
            image.sprite = unknownImage;
            if (GetComponent<Inspectable>() != null)
            {
                Destroy(GetComponent<Inspectable>());
            }
        }
    }
    public bool IsCaught(string fishNameToCheck)
    {
        bool foundFish = false;
        foreach (var fish in allFish)
        {
            if (fishNameToCheck == fish.name)
            {
                foundFish = true;
                break;
            }
        }
        if (foundFish && allFish.Find((x) => x.name == fishNameToCheck).caught == true)
        {
            return true;
        }
        return false;
    }
}

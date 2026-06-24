using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LockCheck : MonoBehaviour
{
    public string areaToCheck;
    public Sprite locationIcon;
    private Image image;
    private AreaChecker areaChecker;
    void Start()
    {
        image = GetComponent<Image>();
        areaChecker = FindFirstObjectByType<AreaChecker>();
        ShowLock(areaToCheck);
    }
    public void ShowLock(string locationName)
    {
        List<bool> areaCheck = areaChecker.CheckArea(locationName);
        if (areaCheck[0] && areaCheck.Count < 3)
        {
            this.gameObject.SetActive(false);
        }
        else if(areaCheck.Count >= 3)
        {
            if (locationIcon != null)
            {
                image.sprite = locationIcon;
            }
        }
    }
}

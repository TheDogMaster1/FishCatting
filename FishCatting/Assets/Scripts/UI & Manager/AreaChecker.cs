using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaChecker : MonoBehaviour
{
    public void PopUpCheck(GameObject PopUp)
    {
        List<bool> areaCheck = CheckArea(this.name);
        //first one is if area is unlocked, second one is if area has been found
        if (areaCheck[0])
        {
            GameManager.instance.ChangeStringArea(this.name);
            PlayerDataManager.instance.SaveGame();
            SceneManager.LoadScene(this.name);
        }
        else if (areaCheck[1])
        {
            PopUp.SetActive(true);
        }
        else
        {
            Debug.LogWarning("area not found!");
        }
    }
    public List<bool> CheckArea(string areaToCheck)
    {
        List<bool> boolList = new();
        bool areafound = false;
        foreach (var area in GameManager.instance.unlockedAreas)
        {
            if (area.name == areaToCheck)
            {
                if (area.unlocked == true)
                {
                    boolList.Add(true);
                    boolList.Add(true);
                    if(GameManager.instance.locatedLocation == area.name)
                    {
                        boolList.Add(true);
                    }
                }
                else
                {
                    boolList.Add(false);
                    boolList.Add(true);
                }
                return boolList;
            }
        }
        if (!areafound)
        {
            boolList.Add(false);
            boolList.Add(false);
        }
        return boolList;
    }
}

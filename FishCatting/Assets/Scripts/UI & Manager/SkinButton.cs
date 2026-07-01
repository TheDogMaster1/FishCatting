using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private CustomSkins.Skins skin;
    [SerializeField]
    private int Cost = 10;
    [SerializeField]
    private Image skinShowcase;

    private bool BoughtSkin = false;

    private void Start()
    {
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        CheckBoughtSkin();
    }

    public void CheckBoughtSkin()
    {
        CustomSkins thisSkin = new();
        foreach (var customskin in GameManager.instance.customSkins)
        {
            if (customskin.customskinName == skin)
            {
                thisSkin = customskin;
                break;
            }
        }
        if (thisSkin.isUnlocked)
        {
            buttonText.text = GameManager.instance.currentSkin.customskinName == skin ? "Equipped" : "Equip";
            skinShowcase.color = Color.white;
            SkinManager.Instance.AddBoughtButton(this);
            BoughtSkin = true;
        }
        else
        {
            buttonText.text = "Buy   Cost: " + Cost;
            skinShowcase.color = Color.black;
            BoughtSkin = false;
        }
    }

    public void UseSkinButton()
    {
        if (BoughtSkin)
        {
            StopAllCoroutines();
            StartCoroutine(SkinManager.Instance.EquipSkin(skin, this));
        }
        else
        {
            StartCoroutine(BuySkin(skin));
        }
    }

    private IEnumerator BuySkin(CustomSkins.Skins skinToBuy)
    {
        if (GameManager.instance.Buy(Cost))
        {
            SkinManager.Instance.AddBoughtButton(this);
            foreach (var customSkin in GameManager.instance.customSkins)
            {
                if (customSkin.customskinName == skinToBuy)
                {
                    customSkin.isUnlocked = true;
                    BoughtSkin = true;
                    StartCoroutine(SkinManager.Instance.EquipSkin(skin, this));
                    skinShowcase.color = Color.white;
                    yield break;
                }
            }
            Debug.LogWarning("Couldn't find any skins");
        }
        else
        {
            buttonText.text = "Too Expensive!!";
            yield return new WaitForSeconds(2);
            buttonText.text = "Buy   Cost: " + Cost;
        }
    }

    public CustomSkins.Skins ReturnSkin()
    {
        return skin;
    }
}

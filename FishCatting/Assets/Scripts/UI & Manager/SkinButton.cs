using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private GameObject skin;
    [SerializeField]
    private int Cost = 10;
    [SerializeField]
    private Sprite boughtTexture;
    [SerializeField]
    private Sprite unBoughtTexture;
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
        if (skin == null) return;
        if (GameManager.instance.skins.Contains(skin))
        {
            buttonText.text = GameManager.instance.currentSkin == skin ? "Equipped" : "Equip";
            skinShowcase.sprite = boughtTexture;
            SkinManager.Instance.AddBoughtButton(this);
            BoughtSkin = true;
        }
        else
        {
            buttonText.text = "Buy";
            skinShowcase.sprite = unBoughtTexture;
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
            StartCoroutine(BuySkin());
        }
    }

    private IEnumerator BuySkin()
    {
        if (GameManager.instance.Buy(Cost))
        {
            SkinManager.Instance.AddBoughtButton(this);
            GameManager.instance.skins.Add(skin);
            BoughtSkin = true;
            StartCoroutine(SkinManager.Instance.EquipSkin(skin, this));
            skinShowcase.sprite = boughtTexture;
        }
        else
        {
            buttonText.text = "Too Expensive!!";
            yield return new WaitForSeconds(2);
            buttonText.text = "Buy";
        }
    }

    public GameObject ReturnSkin()
    {
        return skin;
    }
}

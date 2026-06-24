using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;

    [SerializeField]
    private List<SkinButton> boughtButtons = new();

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator EquipSkin(GameObject pSkin, SkinButton pSelectedButton)
    {
        TextMeshProUGUI buttonText = pSelectedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (pSkin != null && GameManager.instance.currentSkin != pSkin)
        {
            GameManager.instance.currentSkin = pSkin;
            foreach (var button in boughtButtons)
            {
                TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
                text.text = "Equip";

            }
            buttonText.text = "Equipped";
            PlayerDataManager.instance.SaveGame();
        }
        else
        {
            buttonText.text = "Skin Already Equipped";
            yield return new WaitForSeconds(2f);
            buttonText.text = GameManager.instance.currentSkin == pSelectedButton.ReturnSkin() ? "Equipped" : "Equip";
        }
    }

    public void AddBoughtButton(SkinButton button)
    {
        boughtButtons.Add(button);
    }
}

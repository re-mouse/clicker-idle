using System;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeTab : MonoBehaviour
{
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private UpgradeType currentType;

    [SerializeField]
    private Image coinImage;
    [SerializeField]
    private Image upgradeTabImage;

    [SerializeField]
    private Color activeUpgradeColor;
    [SerializeField]
    private Sprite activeUpgradeTabSprite;
    [SerializeField]
    private Sprite activeCoinSprite;

    [SerializeField]
    private Color unactiveUpgradeColor;
    [SerializeField]
    private Sprite unactiveUpgradeTabSprite;
    [SerializeField]
    private Sprite unactiveCoinSprite;

    [SerializeField]
    private Text upgradeName;
    [SerializeField]
    private Text damageText;
    [SerializeField]
    private Text upgradeLvl;
    [SerializeField]
    private Text upgradeCost;
    [SerializeField]
    private Text upgradeLvlDamage;

    private void Awake()
    {
        Player.OnPlayerInfoUpdate.AddListener(UpdateTabInfo);
    }

    private void UpdateTabInfo()
    {
        Upgrade currentUpgrade = Array.Find(Player.GetPlayerInfo().boughUpgrades, x => x.GetUpgradeType() == currentType);
        if (currentUpgrade == null)
            return;

        upgradeName.text = currentUpgrade.GetName();
        damageText.text = currentUpgrade.GetDamageText();
        upgradeLvl.text = PlayerInfo.GetAdaptedInt(currentUpgrade.lvl) + " lvl";
        upgradeCost.text = PlayerInfo.GetAdaptedInt(currentUpgrade.GetUpgradeCost());
        upgradeLvlDamage.text = $"+{currentUpgrade.GetNextLvlUpgradeDamage()}";

        bool isTabActive = Player.GetPlayerInfo().gold > currentUpgrade.GetUpgradeCost();
        if (isTabActive)
            ActivateTab();
        else
            DisableTab();
    }

    private void ActivateTab()
    {
        coinImage.sprite = activeCoinSprite;
        upgradeTabImage.sprite = activeUpgradeTabSprite;
        upgradeCost.color = activeUpgradeColor;
        upgradeButton.interactable = true;
    }

    private void DisableTab()
    {
        coinImage.sprite = unactiveCoinSprite;
        upgradeTabImage.sprite = unactiveUpgradeTabSprite;
        upgradeCost.color = unactiveUpgradeColor;
        upgradeButton.interactable = false;
    }

    public void UpgradeEvent()
    {
        Player.BuyUpgrade(currentType);
    }
}

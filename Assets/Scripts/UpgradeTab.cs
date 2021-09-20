using System;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeTab : MonoBehaviour
{
    [SerializeField]
    private UpgradeType currentType;

    [SerializeField]
    private Text upgradeName;
    [SerializeField]
    private Text upgradeDescription;
    [SerializeField]
    private Text upgradeLvl;
    [SerializeField]
    private Text upgradeCost;
    [SerializeField]
    private Image upgradeIcon;

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
        upgradeDescription.text = currentUpgrade.GetDescription();
        upgradeLvl.text = PlayerInfo.GetAdaptedInt(currentUpgrade.lvl) + " lvl";
        upgradeCost.text = PlayerInfo.GetAdaptedInt(currentUpgrade.GetUpgradeCost());
    }

    public void UpgradeEvent()
    {
        Player.BuyUpgrade(currentType);
    }
}

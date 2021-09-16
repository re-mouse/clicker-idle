using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


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

    private void Start()
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
        upgradeLvl.text = currentUpgrade.lvl.ToString();
        upgradeCost.text = currentUpgrade.GetUpgradeCost().ToString();
    }

    public void UpgradeEvent()
    {

    }
}

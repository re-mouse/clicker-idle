using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.baseDamage += Mathf.RoundToInt(10f * Mathf.Pow(1.07f, lvl));
    }
    public override int GetUpgradeCost() => Mathf.RoundToInt(100f * Mathf.Pow(1.07f, lvl));
    public override UpgradeType GetUpgradeType() => UpgradeType.Sword;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.arrowDamage += GetDamage();
    }

    private int GetDamage() => lvl > 0 ? Mathf.RoundToInt(10f * Mathf.Pow(1.07f, lvl)) : 0;

    public override string GetDescription() => $"Shoot arrow with {GetDamage()} dmg every 1.5 seconds";

    public override string GetName() => "Bow";

    //public override int GetUpgradeCost() => Mathf.RoundToInt(200f * Mathf.Pow(1.07f, lvl));
    public override int GetUpgradeCost() => 1;


    public override UpgradeType GetUpgradeType() => UpgradeType.Arrow;
}

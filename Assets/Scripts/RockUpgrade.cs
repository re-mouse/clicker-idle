using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.rockDamage += GetDamage();
    }

    private int GetDamage() => lvl > 0 ? Mathf.RoundToInt(10f * Mathf.Pow(1.07f, lvl)) : 0;

    public override string GetDescription() => $"Shoot rock with {GetDamage()} dmg every 3 seconds";

    public override string GetName() => "Rock";

    //public override int GetUpgradeCost() => Mathf.RoundToInt(200f * Mathf.Pow(1.07f, lvl));
    public override int GetUpgradeCost() => 1;


    public override UpgradeType GetUpgradeType() => UpgradeType.Arrow;
}

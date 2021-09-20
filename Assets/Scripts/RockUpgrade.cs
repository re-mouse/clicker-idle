using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.rockDamage += GetDamage();
    }

    private long GetDamage() => lvl > 0 ? Mathf.RoundToInt(50000f * Mathf.Pow(1.07f, lvl)) : 0;

    public override string GetDescription() => $"Shoot rock with {PlayerInfo.GetAdaptedInt(GetDamage())} dmg every 3 seconds";

    public override string GetName() => "Rock";

    public override long GetUpgradeCost() => Mathf.RoundToInt(500000f * Mathf.Pow(1.07f, lvl));
    //public override int GetUpgradeCost() => 1;


    public override UpgradeType GetUpgradeType() => UpgradeType.Rock;
}

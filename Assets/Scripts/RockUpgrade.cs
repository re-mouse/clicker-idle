using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.rockDamage += GetDamage(lvl);
    }

    private ulong GetDamage(ulong lvl) => lvl > 0 ? Convert.ToUInt64(Math.Round(50000d * Math.Pow(1.07d, lvl))) : 0;

    public override string GetDamageText() => PlayerInfo.GetAdaptedInt(GetDamage(lvl));

    public override string GetName() => "Rock";

    public override ulong GetUpgradeCost() => Convert.ToUInt64(Math.Round(500000d * Math.Pow(1.07d, lvl)));
    //public override int GetUpgradeCost() => 1;


    public override UpgradeType GetUpgradeType() => UpgradeType.Rock;

    public override string GetNextLvlUpgradeDamage() => PlayerInfo.GetAdaptedInt(GetDamage(lvl + 1) - GetDamage(lvl));
}

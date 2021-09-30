using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleaxeUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.baseDamage += GetDamage(lvl);
    }

    private ulong GetDamage(ulong lvl) => lvl > 0 ? Convert.ToUInt64(Math.Round(200000d * Math.Pow(1.07d, lvl))) : 0;

    public override ulong GetUpgradeCost() => Convert.ToUInt64(Math.Round(500000d * Math.Pow(1.07d, lvl)));
    public override UpgradeType GetUpgradeType() => UpgradeType.Poleaxe;
    public override string GetDamageText() => PlayerInfo.GetAdaptedInt(GetDamage(lvl));

    public override string GetName() => "Poleaxe";
    public override string GetNextLvlUpgradeDamage() => PlayerInfo.GetAdaptedInt(GetDamage(lvl + 1) - GetDamage(lvl));
}

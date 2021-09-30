using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.baseDamage += GetDamage(lvl);
    }

    private ulong GetDamage(ulong lvl) => Convert.ToUInt64(Math.Round(10d * Math.Pow(1.07d, lvl)));

    public override ulong GetUpgradeCost() => Convert.ToUInt64(Math.Round(50d * Math.Pow(1.07d, lvl)));
    public override UpgradeType GetUpgradeType() => UpgradeType.Sword;
    public override string GetDamageText() => PlayerInfo.GetAdaptedInt(GetDamage(lvl));

    public override string GetName() => "Sword";
    public override string GetNextLvlUpgradeDamage() => PlayerInfo.GetAdaptedInt(GetDamage(lvl + 1) - GetDamage(lvl));
}

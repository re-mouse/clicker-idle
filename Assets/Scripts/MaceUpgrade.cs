using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.baseDamage += GetDamage(lvl);
    }

    private ulong GetDamage(ulong lvl) => lvl > 0 ? Convert.ToUInt64(Math.Round(500d * Math.Pow(1.07d, lvl))) : 0;

    public override ulong GetUpgradeCost() => Convert.ToUInt64(Math.Round(1000d * Math.Pow(1.07d, lvl)));
    public override UpgradeType GetUpgradeType() => UpgradeType.Mace;
    public override string GetDamageText() => PlayerInfo.GetAdaptedInt(GetDamage(lvl));

    public override string GetName() => "Mace";

    public override string GetNextLvlUpgradeDamage() => PlayerInfo.GetAdaptedInt(GetDamage(lvl + 1) - GetDamage(lvl));
}
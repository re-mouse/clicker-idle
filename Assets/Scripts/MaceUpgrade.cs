using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceUpgrade : Upgrade
{
    public override void ApplyUpgrade(Player upgrader, PlayerStats stats)
    {
        stats.baseDamage += GetDamage();
    }

    private int GetDamage() => lvl > 0 ? Mathf.RoundToInt(20000f * Mathf.Pow(1.07f, lvl)) : 0;

    public override long GetUpgradeCost() => Mathf.RoundToInt(80000f * Mathf.Pow(1.07f, lvl));
    public override UpgradeType GetUpgradeType() => UpgradeType.Mace;
    public override string GetDescription() => $"Add {PlayerInfo.GetAdaptedInt(GetDamage())} damage to your attack";

    public override string GetName() => "Mace";
}
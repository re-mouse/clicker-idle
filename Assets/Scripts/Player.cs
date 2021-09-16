using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStats stats;

    public void SetPlayerInfo(PlayerInfo info)
    {
        SetUpgrades(info.boughUpgrades);
    }

    private void SetUpgrades(Upgrade[] upgrades)
    {
        stats = new PlayerStats();

        foreach (Upgrade upgrade in upgrades)
            upgrade.ApplyUpgrade(this, stats);
    }
}

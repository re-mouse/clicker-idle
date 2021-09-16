using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStats stats;
    private PlayerInfo playerInfo;

    private static Player i;

    private void Awake()
    {
        if (i != null && i != this)
            Destroy(this);
        else
            i = this;
    }

    public static void SetPlayerInfo(PlayerInfo info)
    {
        i.playerInfo = info;
        i.SetUpgrades(info.boughUpgrades);
    }

    public static PlayerInfo GetPlayerInfo() => i.playerInfo;

    private void SetUpgrades(Upgrade[] upgrades)
    {
        stats = new PlayerStats();

        foreach (Upgrade upgrade in upgrades)
            upgrade.ApplyUpgrade(this, stats);
    }
}

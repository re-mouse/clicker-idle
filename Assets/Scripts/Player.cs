using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static UnityEvent OnPlayerInfoUpdate {get; private set;} = new UnityEvent(); 
    private PlayerStats stats;
    private PlayerInfo playerInfo;
    private static Player i;

    private void Awake()
    {
        if (i != null && i != this)
            Destroy(this);
        else
            i = this;

        OnPlayerInfoUpdate.AddListener(UpdateUpgrades);
    }

    public static void SetPlayerInfo(PlayerInfo info)
    {
        i.playerInfo = info;
        OnPlayerInfoUpdate.Invoke();
    }

    public static PlayerInfo GetPlayerInfo() => i.playerInfo;
    public static PlayerStats GetPlayerStats() => i.stats;


    private void UpdateUpgrades()
    {
        print(i.playerInfo.Serialize().ToString());
        stats = new PlayerStats();

        foreach (Upgrade upgrade in playerInfo.boughUpgrades)
            upgrade.ApplyUpgrade(this, stats);
    }

    public static void BuyUpgrade(UpgradeType type)
    {
        Upgrade requirmentUpgrade = Array.Find(i.playerInfo.boughUpgrades, x => x.GetUpgradeType() == type);
        if (i.playerInfo.gold >= requirmentUpgrade.GetUpgradeCost()) 
        {
            i.playerInfo.gold -= requirmentUpgrade.GetUpgradeCost();
            requirmentUpgrade.lvl++;
        }
        OnPlayerInfoUpdate.Invoke();
    }

    public static void DamageCurrentEntity(long damage)
    {
        EntitySpawner.CurrentEntity?.TakeDamage(damage);
    }

    public static void AddGold(int goldQuantity)
    {
        i.playerInfo.gold += goldQuantity;
        OnPlayerInfoUpdate.Invoke();
    }

    public static void AddMobOnCount()
    {
        i.playerInfo.killedMobs++;
        OnPlayerInfoUpdate.Invoke();
    }

    public static long GetMobCount() => i.playerInfo.killedMobs;
}

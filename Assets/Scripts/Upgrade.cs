using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public enum UpgradeType{Sword, Arrow, Rock, Mace};

public abstract class Upgrade
{
    public long lvl;

    public virtual JSONObject Serialize()
    {
        JSONObject json = new JSONObject();

        json["type"] = GetUpgradeType().ToString();
        json["lvl"] = lvl;

        return json;
    }

    public abstract string GetName();
    public abstract string GetDescription();
    public abstract long GetUpgradeCost();
    public abstract UpgradeType GetUpgradeType();
    public abstract void ApplyUpgrade(Player upgrader, PlayerStats stats);
}

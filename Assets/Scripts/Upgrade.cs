using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public enum UpgradeType{Sword};

public abstract class Upgrade
{
    public int lvl;

    public virtual JSONObject Serialize()
    {
        JSONObject json = new JSONObject();

        json["type"] = GetUpgradeType().ToString();
        json["lvl"] = lvl;

        return json;
    }

    public abstract int UnlockCost();
    public abstract string GetName();
    public abstract string GetDescription();
    public abstract int GetUpgradeCost();
    public abstract UpgradeType GetUpgradeType();
    public abstract void ApplyUpgrade(Player upgrader, PlayerStats stats);
}

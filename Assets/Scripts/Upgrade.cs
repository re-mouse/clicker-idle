using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public enum UpgradeType{Sword};

public abstract class Upgrade
{
    public UpgradeType type;
    public int lvl; 
    public virtual JSONObject Serialize()
    {
        JSONObject json = new JSONObject();

        json["type"] = type.ToString();
        json["lvl"] = lvl;

        return json;
    } 
    public abstract UpgradeType GetUpgradeType();
    public abstract void ApplyUpgrade(Player upgrader, PlayerStats stats);
}

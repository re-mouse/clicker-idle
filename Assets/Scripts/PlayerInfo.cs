using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

public class PlayerInfo
{
    public int gold;
    public int killedMobs;
    public Upgrade[] boughUpgrades;
    public PlayerInfo() 
    {
        boughUpgrades = new Upgrade[] {new SwordUpgrade()};
    }

    private static Upgrade[] upgradePool = new Upgrade[]{ new SwordUpgrade() };
    public static Upgrade GetUpgrade(UpgradeType type) => Array.Find(upgradePool, x => x.GetUpgradeType() == type);


    public PlayerInfo(JSONNode json)
    {
        gold = json["gold"].AsInt;
        killedMobs = json["mobs"].AsInt;
        JSONNode upgradesJson = json["upgrades"];
        List<Upgrade> upgrades = new List<Upgrade>();

        foreach (JSONNode upgradeJson in upgradesJson)
        {
            UpgradeType upgradeType = (UpgradeType)Enum.Parse(typeof(UpgradeType), upgradeJson["type"]);
            var upgrade = GetUpgrade(upgradeType);
            upgrade.lvl = upgradeJson["lvl"];
            upgrades.Add(upgrade);
        }

        boughUpgrades = upgrades.ToArray();
    }

    public JSONObject Serialize()
    {
        JSONObject json = new JSONObject();

        json["gold"] = gold;
        json["mobs"] = killedMobs;

        JSONArray upgrades = new JSONArray();
        foreach (Upgrade upgrade in boughUpgrades)
            upgrades.Add(upgrade.Serialize());

        json["upgrades"] = upgrades;

        return json;
    }
}

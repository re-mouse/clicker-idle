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
        boughUpgrades = new Upgrade[] {new SwordUpgrade(), new ArrowsUpgrade(), new RockUpgrade(), new MaceUpgrade()};
    }

    private static Upgrade[] upgradePool = new Upgrade[]{ new SwordUpgrade(), new ArrowsUpgrade(), new RockUpgrade(), new MaceUpgrade() };
    public static Upgrade GetUpgrade(UpgradeType type) => Array.Find(upgradePool, x => x.GetUpgradeType() == type);


    public static string GetAdaptedInt(int requireInt)
    {
        float intToAdapt = requireInt;
        if (intToAdapt > Mathf.Pow(10, 12)) 
        {
            return $"{Mathf.FloorToInt(intToAdapt / Mathf.Pow(10, 12))}Q";
        }
        else if (intToAdapt > Mathf.Pow(10, 9))
        {
            return $"{Mathf.FloorToInt(intToAdapt / Mathf.Pow(10, 9))}B";
        }
        else if (intToAdapt > Mathf.Pow(10, 6))
        {
            return $"{Mathf.FloorToInt(intToAdapt / Mathf.Pow(10, 6))}M";
        }
        else if (intToAdapt > Mathf.Pow(10, 3))
        {
            Debug.Log(intToAdapt / Mathf.Pow(10, 3));
            return $"{Mathf.FloorToInt(intToAdapt / Mathf.Pow(10, 3))}K";
        }
        return intToAdapt.ToString();
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

public class PlayerInfo
{
    public ulong gold;
    public ulong killedMobs;
    public Upgrade[] boughUpgrades;
    public PlayerInfo() 
    {
        boughUpgrades = new Upgrade[] {new SwordUpgrade(), new BowUpgrade(), new RockUpgrade(), new MaceUpgrade(), new AxeUpgrade(), new CrossbowUpgrade(), new PoleaxeUpgrade(), new CatapultUpgrade()};
    }

    private static Upgrade[] upgradePool = new Upgrade[]{ new SwordUpgrade(), new BowUpgrade(), new RockUpgrade(), new MaceUpgrade(), new AxeUpgrade(), new CrossbowUpgrade(), new PoleaxeUpgrade(), new CatapultUpgrade() };
    public static Upgrade GetUpgrade(UpgradeType type) => Array.Find(upgradePool, x => x.GetUpgradeType() == type);

    private static ulong GetPowULong(ulong requiredULong, int pow)
    {
        ulong originalULong = requiredULong;
        requiredULong = 1;
        for (int i = 0; i < pow; i++)
        {
            requiredULong *= originalULong;
        }

        return requiredULong;
    }

    public static string GetAdaptedInt(ulong intToAdapt)
    {
        if (intToAdapt > GetPowULong(10, 19))
        {
            return $"{intToAdapt / GetPowULong(10, 18)}Z";
        }
        if (intToAdapt > GetPowULong(10, 16))
        {
            return $"{intToAdapt / GetPowULong(10, 15)}A";
        }
        if (intToAdapt > GetPowULong(10, 13)) 
        {
            return $"{intToAdapt / GetPowULong(10, 12)}Q";
        }
        else if (intToAdapt > GetPowULong(10, 10))
        {
            return $"{intToAdapt / GetPowULong(10, 9)}B";
        }
        else if (intToAdapt > GetPowULong(10, 7))
        {
            return $"{intToAdapt / GetPowULong(10, 6)}M";
        }
        else if (intToAdapt > GetPowULong(10, 4))
        {
            return $"{intToAdapt / GetPowULong(10, 3)}K";
        }
        return intToAdapt.ToString();
    }

    public PlayerInfo(JSONNode json)
    {
        gold = json["gold"].AsULong;
        killedMobs = json["mobs"].AsULong;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class PlayerInfo
{
    public int gold;
    public List<Upgrade> boughUpgrades;

    public PlayerInfo(JSONNode node)
    {
        JSONNode json = JSON.Parse(PlayerPrefs.GetString("player"));

        gold = json["gold"].AsInt;
        JSONNode upgradesJson = json["upgrades"];
        
    }

    public JSONObject Serialize()
    {
        JSONObject json = new JSONObject();

        json["gold"] = gold;

        JSONArray upgrades = new JSONArray();
        foreach (Upgrade upgrade in boughUpgrades)
            upgrades.Add(upgrade.Serialize());

        json["upgrades"] = upgrades;

        return json;
    }
}

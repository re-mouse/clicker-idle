using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
        InvokeRepeating("UpdatePlayerData", 3f, 3f);

        string json = PlayerPrefs.GetString("save");
        
        if (json != "")
            Player.SetPlayerInfo(new PlayerInfo(SimpleJSON.JSON.Parse(json)));
        else
            Player.SetPlayerInfo(new PlayerInfo());
    }

    private void UpdatePlayerData()
    {
        var playerInfo = Player.GetPlayerInfo();
        PlayerPrefs.SetString("save", playerInfo.Serialize().ToString());
    }

    private void OnDestroy()
    {
        UpdatePlayerData();
    }
}

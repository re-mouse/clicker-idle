using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();

        string json = PlayerPrefs.GetString("data");
        
        if (json != null)
            Player.SetPlayerInfo(new PlayerInfo(json));
        else
            Player.SetPlayerInfo(new PlayerInfo());
    }

    private void UpdatePlayerSave(PlayerInfo playerInfo)
    {
        PlayerPrefs.SetString("data", playerInfo.Serialize());
    }
}

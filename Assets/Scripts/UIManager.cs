using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text gold;
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Text health;

    private void Awake()
    {
        EntitySpawner.OnEntitySpawn.AddListener(UpdateEntityHealthBar);
        EntitySpawner.OnEntityTakeDamage.AddListener(UpdateEntityHealthBar);
        Player.OnPlayerInfoUpdate.AddListener(UpdateGold);
    }

    private void UpdateEntityHealthBar()
    {
        if (EntitySpawner.CurrentEntity == null)
        {
            healthBar.value = 0;
            health.text = "0/0";
            return;
        }

        healthBar.value = 1.0f * EntitySpawner.CurrentEntity.GetHealth() / EntitySpawner.CurrentEntity.GetMaxHealth();
        health.text = $"{PlayerInfo.GetAdaptedInt(EntitySpawner.CurrentEntity.GetHealth())}/{PlayerInfo.GetAdaptedInt(EntitySpawner.CurrentEntity.GetMaxHealth())}";
    }

    private void UpdateGold()
    {
        gold.text = PlayerInfo.GetAdaptedInt(Player.GetPlayerInfo().gold);
    }
}

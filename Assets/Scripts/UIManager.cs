using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject battleDamagePrefab;
    [SerializeField]
    private RectTransform battleDamageSpawnTransform;
    [SerializeField]
    private Text gold;
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Text health;

    private void Awake()
    {
        EntitySpawner.OnEntitySpawn.AddListener(UpdateEntityHealthBar);
        EntitySpawner.OnEntityTakeDamage.AddListener(x => UpdateEntityHealthBar());
        EntitySpawner.OnEntityTakeDamage.AddListener(ShowDamageText);
        EntitySpawner.OnEntityTakeDamage.AddListener(x => MMVibrationManager.Haptic(HapticTypes.SoftImpact));
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

    private void ShowDamageText(long damage)
    {
        var damageTextObject = Instantiate(battleDamagePrefab, battleDamageSpawnTransform);
        var pos = damageTextObject.GetComponent<RectTransform>().anchoredPosition;
        pos.x += Random.Range(-160, 160);
        pos.y += Random.Range(-5, 150);
        damageTextObject.GetComponent<RectTransform>().anchoredPosition = pos;
        damageTextObject.GetComponent<Text>().text = $"-{PlayerInfo.GetAdaptedInt(damage)} HP";
    }

    private void UpdateGold()
    {
        gold.text = PlayerInfo.GetAdaptedInt(Player.GetPlayerInfo().gold);
    }
}

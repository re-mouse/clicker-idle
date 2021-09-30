using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldSpawner : MonoBehaviour
{
    private Coroutine shieldSwapper;
    [SerializeField]
    private Image[] shields;

    private void Awake()
    {
        EntitySpawner.OnBossSpawn.AddListener(() => shieldSwapper = StartCoroutine(ShowShield()));
        EntitySpawner.OnBossDeath.AddListener(() => StopCoroutine(shieldSwapper));
        EntitySpawner.OnBossDeath.AddListener(DisableAllShields);
    }

    private ulong GetBossLvl()
    {
        return Player.GetMobCount() / 3;
    }

    private IEnumerator ShowShield()
    {
        float passedTime = 0f;
        
        float timeToSwapShields;
        if (GetBossLvl() <= 10)
            timeToSwapShields = 10f * Mathf.Pow(1.20f, -1f * GetBossLvl());
        else if (GetBossLvl() > 10 && GetBossLvl() <= 20)
            timeToSwapShields = 10f * Mathf.Pow(1.20f, -1f * GetBossLvl());
        else
            timeToSwapShields = 10f * Mathf.Pow(1.07f, -1f * GetBossLvl());

        int shieldCount;
        if (GetBossLvl() <= 10)
            shieldCount = 1;
        else if (GetBossLvl() > 10 && GetBossLvl() <= 20)
            shieldCount = 2;
        else
            shieldCount = 3;

        while (passedTime < EntitySpawner.timeToKillBoss)
        {
            DisableAllShields();
            EnableShields(shieldCount);
            yield return new WaitForSeconds(timeToSwapShields);
            passedTime += timeToSwapShields;
        }
        DisableAllShields();
    }

    private void DisableAllShields()
    {
        foreach (Image shield in shields)
            shield.gameObject.SetActive(false);
    }

    private void EnableShields(int count)
    {
        int shieldCount = shields.Length;

        List<int> shieldsToEnable = new List<int>();

        if (count > shieldCount) 
        {
            Debug.LogError("WTF?");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            int shieldToEnable = Random.Range(0, shieldCount);  shieldsToEnable.Add(Random.Range(0, shieldCount));
            while (shieldsToEnable.Contains(shieldToEnable))
                shieldToEnable = Random.Range(0, shieldCount); shieldsToEnable.Add(Random.Range(0, shieldCount));

            shieldsToEnable.Add(shieldToEnable);
        }

        foreach (int shieldIndex in shieldsToEnable)
            shields[shieldIndex].gameObject.SetActive(true);
    }
}

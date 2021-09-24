using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeDamageEvent : UnityEvent<ulong>
{

}


public class EntitySpawner : MonoBehaviour
{
    public readonly static int timeToKillBoss = 15;
    public readonly static ulong periodicity = 3;

    private static EntitySpawner i;

    [SerializeField]
    private float entityRespawnDelay;

    public static Entity CurrentEntity => i.currentEntity;
    public static UnityEvent OnBossSpawn { get; private set; } = new UnityEvent();
    public static UnityEvent OnBossDeath { get; private set; } = new UnityEvent();
    public static TakeDamageEvent OnEntityTakeDamage { get; private set; } = new TakeDamageEvent();
    public static UnityEvent OnEntitySpawn { get; private set; } = new UnityEvent();
    private Entity currentEntity;

    [SerializeField]
    Vector3 newEntitySpawnPoint;

    [SerializeField]
    private List<GameObject> entitySpawnPool;

    private Coroutine currentBossKillTimeEvent;

    private void Awake()
    {
        if (i != null && i != this)
            Destroy(this);
        else
            i = this;

        OnBossSpawn.AddListener(() => currentBossKillTimeEvent = StartCoroutine(BossKillTimeEvent()));
        OnBossDeath.AddListener(() => StopCoroutine(currentBossKillTimeEvent));
    }


    private void Start()
    {
        Invoke("SpawnNewEntity", 0.1f);
    }

    private void EntityKill()
    {
        GetKillReward();

        Player.AddMobOnCount();
        
        currentEntity = null;
        Invoke("SpawnNewEntity", entityRespawnDelay);
    }

    private void GetKillReward()
    {
        Player.AddGold(currentEntity.GetGoldReward());
    }

    private IEnumerator BossKillTimeEvent()
    {
        float remainingTime = timeToKillBoss;
        while (remainingTime > 0)
        {
            remainingTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(currentEntity.gameObject);
        currentEntity = null;
        Invoke("SpawnNewEntity", entityRespawnDelay);
        OnEntityTakeDamage.Invoke(0);
        Player.ReduceMobCount(periodicity - 1);
    }

    private void SpawnNewEntity()
    {

        GameObject newEntityPrefab = entitySpawnPool[UnityEngine.Random.Range(0, entitySpawnPool.Count)];

        GameObject spawnedEntityObject = Instantiate(newEntityPrefab, transform);

        Entity entity = spawnedEntityObject.GetComponent<Entity>();

        entity.SetHealth(Convert.ToUInt64(Math.Round(100d * Math.Pow(1.15d, Player.GetMobCount()))));

        entity.OnDeathEvent.AddListener(EntityKill);

        currentEntity = entity;

        if (Player.GetMobCount() != 0 && Player.GetMobCount() % periodicity == 0)
        {
            OnBossSpawn.Invoke();
            currentEntity.OnDeathEvent.AddListener(() => OnBossDeath.Invoke());
        }

        OnEntitySpawn.Invoke();
    }

}

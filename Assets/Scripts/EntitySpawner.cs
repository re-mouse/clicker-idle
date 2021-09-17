using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
    private static EntitySpawner i;

    [SerializeField]
    private float entityRespawnDelay;

    public static Entity CurrentEntity => i.currentEntity;
    public static UnityEvent OnEntityTakeDamage { get; private set; } = new UnityEvent();
    public static UnityEvent OnEntitySpawn { get; private set; } = new UnityEvent();
    private Entity currentEntity;

    [SerializeField]
    Vector3 newEntitySpawnPoint;

    [SerializeField]
    private List<GameObject> entitySpawnPool;

    private void Awake()
    {
        if (i != null && i != this)
            Destroy(this);
        else
            i = this; 
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

    private void SpawnNewEntity()
    {
        GameObject newEntityPrefab = entitySpawnPool[Random.Range(0, entitySpawnPool.Count)];

        GameObject spawnedEntityObject = Instantiate(newEntityPrefab, transform);

        Entity entity = spawnedEntityObject.GetComponent<Entity>();

        entity.OnDeathEvent.AddListener(EntityKill);

        currentEntity = entity;

        OnEntitySpawn.Invoke();
    }

}

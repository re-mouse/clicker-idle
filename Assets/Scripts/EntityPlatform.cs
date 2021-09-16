using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlatform : MonoBehaviour
{
    [SerializeField]
    private int gold;

    [SerializeField]
    private int currentDamage;

    [SerializeField]
    private float entityRespawnDelay;

    private Entity currentEntity;

    [SerializeField]
    Vector3 newEntitySpawnPoint;

    [SerializeField]
    private List<GameObject> entitySpawnPool;

    private void Start()
    {
        Invoke("SpawnNewEntity", 0.1f);
    }

    private void OnMouseDown()
    {
        currentEntity?.TakeDamage(currentDamage);
    }

    private void EntityKill()
    {
        currentEntity = null;

        GetKillReward();
        Invoke("SpawnNewEntity", entityRespawnDelay);
    }

    private void GetKillReward()
    {
        gold += 1;
    }

    private void SpawnNewEntity()
    {
        GameObject newEntityPrefab = entitySpawnPool[Random.Range(0, entitySpawnPool.Count)];

        GameObject spawnedEntityObject = Instantiate(newEntityPrefab);

        Entity entity = spawnedEntityObject.GetComponent<Entity>();

        entity.OnDeathEvent.AddListener(EntityKill);

        currentEntity = entity;
    }
}

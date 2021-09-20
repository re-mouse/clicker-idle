using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private List<Vector3> arrowSpawnpoints;
    [SerializeField]
    private List<Vector3> arrowEulers;

    private void Start()
    {
        InvokeRepeating("ShootArrow", 1.5f, 1.5f);
        InvokeRepeating("ShootRock", 2.25f, 3f);
    }

    private void ShootArrow()
    {
        if (Player.GetPlayerStats().arrowDamage == 0)
            return;
        ShootObject(arrowPrefab);
    }

    private void ShootRock()
    {
        if (Player.GetPlayerStats().rockDamage == 0)
            return;
        ShootObject(rockPrefab);
    }

    private void ShootObject(GameObject prefab)
    {
        if (EntitySpawner.CurrentEntity == null)
            return;

        int arrowIndex = Random.Range(0, arrowSpawnpoints.Count);
        Vector3 arrowSpawnPosition = arrowSpawnpoints[arrowIndex];

        var arrow = Instantiate(prefab);
        arrow.transform.position = arrowSpawnPosition;
        arrow.transform.LookAt(EntitySpawner.CurrentEntity.transform);
        arrow.transform.eulerAngles = arrowEulers[arrowIndex];
    }
}

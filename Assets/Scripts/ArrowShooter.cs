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
    }

    private void ShootArrow()
    {
        if (EntitySpawner.CurrentEntity == null || Player.GetPlayerStats().arrowDamage == 0)
            return;

        int arrowIndex = Random.Range(0, arrowSpawnpoints.Count);
        Vector3 arrowSpawnPosition = arrowSpawnpoints[arrowIndex];

        var arrow = Instantiate(arrowPrefab);
        arrow.transform.position = arrowSpawnPosition;
        arrow.transform.LookAt(EntitySpawner.CurrentEntity.transform);
        arrow.transform.eulerAngles = arrowEulers[arrowIndex];
    }
}

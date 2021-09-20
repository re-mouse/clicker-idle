using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.fixedDeltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Entity"))
            return;

        Player.DamageCurrentEntity(Player.GetPlayerStats().rockDamage);
        Destroy(gameObject);
    }
}

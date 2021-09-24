using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent OnDeathEvent {get; private set;} = new UnityEvent();
    [SerializeField]
    private ulong health;
    private ulong maxHealth;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        OnDeathEvent.AddListener(DropDown);
        maxHealth = health;
        rigidBody = GetComponent<Rigidbody2D>(); 
    }

    public void SetHealth(ulong health)
    {
        this.health = health;
        maxHealth = health;
    }

    public ulong GetHealth() => health;
    public ulong GetMaxHealth() => maxHealth;

    public void TakeDamage(ulong damage)
    {
        if (health == 0)
            return;

        if (health > damage)
            health -= damage;
        else
            OnDeathEvent.Invoke();

        EntitySpawner.OnEntityTakeDamage.Invoke(damage);
    }

    private void DropDown()
    {
        rigidBody.AddForce(Vector3.up * 10, ForceMode2D.Force);
        
        Destroy(GetComponent<Collider2D>());

        StartCoroutine(SelfDestroy());
        IEnumerator SelfDestroy()
        {
            while (transform.position.y > -10f)
                yield return null;

            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        Player.DamageCurrentEntity(Player.GetPlayerStats().baseDamage);
    }

    public ulong GetGoldReward()
    {
        return Convert.ToUInt64(Math.Round(1.0f * maxHealth * 0.15f));
    }

    private void OnDestroy()
    {
        OnDeathEvent.RemoveAllListeners();
    }
}

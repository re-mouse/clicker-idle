using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent OnDeathEvent {get; private set;} = new UnityEvent();
    [SerializeField]
    private int health;
    private int maxHealth;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        OnDeathEvent.AddListener(DropDown);
        health = Mathf.RoundToInt(100f * Mathf.Pow(1.15f, Player.GetMobCount()));
        maxHealth = health;
        rigidBody = GetComponent<Rigidbody2D>(); 
    }

    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        if (health <= 0)
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

    public int GetGoldReward()
    {
        return Mathf.RoundToInt(1.0f * maxHealth * 0.15f);
    }

    private void OnDestroy()
    {
        OnDeathEvent.RemoveAllListeners();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        var animator = GetComponent<Animator>();
        EntitySpawner.OnEntityTakeDamage.AddListener(HitAnimationTrigger);
    }

    private void HitAnimationTrigger(ulong damage) => GetComponent<Animator>().SetTrigger("Hit");

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

    public static bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Where(r => r.gameObject.layer == 5).Count() > 0;
    }

    private void OnMouseDown()
    {
        if (IsPointerOverUIElement())
            return;
        Player.DamageCurrentEntity(Player.GetPlayerStats().baseDamage);
    }

    public ulong GetGoldReward()
    {
        return Convert.ToUInt64(Math.Round(1.0f * maxHealth * 0.15f));
    }

    private void OnDestroy()
    {
        EntitySpawner.OnEntityTakeDamage.RemoveListener(HitAnimationTrigger);
        OnDeathEvent.RemoveAllListeners();
    }
}

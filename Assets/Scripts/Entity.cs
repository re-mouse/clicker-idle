using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent OnDeathEvent {get; private set;} = new UnityEvent();
    [SerializeField]
    private int health;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        OnDeathEvent.AddListener(DropDown);   
        rigidBody = GetComponent<Rigidbody2D>(); 
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        if (health <= 0)
            OnDeathEvent.Invoke();
    }

    private void DropDown()
    {
        rigidBody.AddForce(Vector3.up * 4, ForceMode2D.Force);
        
        Destroy(GetComponent<Collider2D>());

        StartCoroutine(SelfDestroy());
        IEnumerator SelfDestroy()
        {
            while (transform.position.y > -10f)
                yield return null;

            Destroy(gameObject);
        }
    }

    public int GetGoldReward()
    {
        return 10;
    }

    private void OnDestroy()
    {
        OnDeathEvent.RemoveAllListeners();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject DeathPS;

    public void OnHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            Destroy(gameObject);
            Instantiate(DeathPS, transform.position, Quaternion.identity);
        }
    }
}

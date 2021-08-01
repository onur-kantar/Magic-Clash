using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject deathPS;
    [HideInInspector] public EnemyCreator ownerEnemyCreator;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnHit(string deathStyle)
    {
        if (deathStyle == "Death") //TODO: --kötü bir yöntem
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<EnemyHealth>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            animator.SetBool("Death", true);
        }
        else if (deathStyle == "Explosion")
        {
            Instantiate(deathPS, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        ownerEnemyCreator.ownerEnemyController.ReduceEnemyCount();
    }
}

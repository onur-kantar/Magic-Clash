using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float overlapRadius;

    GameObject mainPlayer;
    NavMeshAgent navMeshAgent;
    Animator animator;
    Transform nearestEnemy;

    void Start()
    {
        mainPlayer = GameObject.Find("MainPlayer");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (mainPlayer.transform.childCount != 0)
        {
            FindClosestPlayer();
            if (nearestEnemy == null)
            {
                navMeshAgent.SetDestination(mainPlayer.transform.position);
            }
            else
            {
                navMeshAgent.SetDestination(nearestEnemy.position);
            }
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
        }
        else
        {
            navMeshAgent.ResetPath();
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Walk");
        }
    }
    void FindClosestPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius);
        float minimumDistance = Mathf.Infinity;
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Player")
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = collider.transform;
                }
            }
        }
    }
}

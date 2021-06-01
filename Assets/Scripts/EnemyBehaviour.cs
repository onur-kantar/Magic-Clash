using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float OverlapRadius;
    GameObject mainPlayer;
    Transform nearestEnemy;
    NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;
    [SerializeField] int health;
    private void Start()
    {
        mainPlayer = GameObject.Find("MainPlayer");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
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
            WalkAnimation();
        }
        else
        {
            navMeshAgent.ResetPath();
            IdleAnimation();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;

            collision.gameObject.GetComponent<PlayerBehaviour>().LeaveTeam();

            GetComponent<CapsuleCollider>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;

            animator.SetBool("Death", true);
            collision.gameObject.GetComponent<PlayerBehaviour>().animator.SetBool("Death", true);

            GetComponent<EnemyBehaviour>().enabled = false;
            collision.gameObject.GetComponent<PlayerBehaviour>().enabled = false;

            GetComponent<NavMeshAgent>().enabled = false;
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;


        }
        else if (collision.gameObject.CompareTag("MagicBall"))
        {
            health -= collision.gameObject.GetComponent<MagicBallBehaviour>().damage;
            if (health <= 0)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<EnemyBehaviour>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<EnemyBehaviour>().animator.SetBool("Death", true);
            }
        }
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }

    void FindClosestPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, OverlapRadius);
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
    void WalkAnimation()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);
    }
    void IdleAnimation()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWalk : StateMachineBehaviour
{
    [SerializeField] float overlapRadius;
    [SerializeField] float attackRange;

    GameObject mainPlayer;
    NavMeshAgent navMeshAgent;
    Transform nearestEnemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainPlayer = GameObject.Find("MainPlayer");
        navMeshAgent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mainPlayer.transform.childCount > 0)
        {
            FindClosestPlayer(animator);
            if (nearestEnemy == null)
            {
                navMeshAgent.SetDestination(mainPlayer.transform.position);
            }
            else
            {
                if (Vector3.Distance(animator.transform.position, nearestEnemy.position) < attackRange)
                {
                    navMeshAgent.enabled = false;
                    animator.SetTrigger("Attack");
                }
                else
                {
                    if (!animator.GetBool("Attack"))
                    {

                        navMeshAgent.enabled = true;
                        navMeshAgent.SetDestination(nearestEnemy.position);
                    }
                    else
                    {
                        navMeshAgent.enabled = false;
                    }
                }
            }
        }
        else
        {
            navMeshAgent.enabled = true;
            navMeshAgent.ResetPath();
            animator.SetTrigger("Idle");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Walk");
        //animator.ResetTrigger("Attack");
        //animator.ResetTrigger("Idle");
    }

    void FindClosestPlayer(Animator animator)
    {
        Collider[] hitColliders = Physics.OverlapSphere(animator.transform.position, overlapRadius);
        float minimumDistance = Mathf.Infinity;
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Player")
            {
                float distance = Vector3.Distance(animator.transform.position, collider.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = collider.transform;
                }
            }
        }
    }
    
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

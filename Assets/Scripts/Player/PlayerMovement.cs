using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    PlayerJoinTeam playerJoinTeam;
    NavMeshAgent navMeshAgent;
    GameObject mainPlayer;
    MainPlayerMovement mainPlayerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerJoinTeam = GetComponent<PlayerJoinTeam>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        mainPlayer = GameObject.Find("MainPlayer");
        mainPlayerMovement = mainPlayer.GetComponent<MainPlayerMovement>();
    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector3 yEqualZero = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        if (Mathf.Abs(Vector3.Distance(yEqualZero, playerJoinTeam.target)) > 0.1f)
        {
            navMeshAgent.SetDestination(mainPlayer.transform.position + playerJoinTeam.target);
            if (navMeshAgent.hasPath)
            {
                transform.position = ArrayLerp(navMeshAgent.path.corners, 0.05f/*, 0.1f*/);
                animator.SetTrigger("Walk");
                animator.ResetTrigger("Idle");
                transform.forward = (playerJoinTeam.target - yEqualZero).normalized;
            }
            else if (mainPlayerMovement.move != Vector3.zero)
            {
                animator.SetTrigger("Walk");
                animator.ResetTrigger("Idle");
            }
            else
            {
                animator.SetTrigger("Idle");
                animator.ResetTrigger("Walk");
            }
        }
        else
        {
            if (navMeshAgent.hasPath)
            {
                navMeshAgent.ResetPath();
            }
            if (mainPlayerMovement.move != Vector3.zero)
            {
                transform.forward = Vector3.Lerp(transform.forward, mainPlayerMovement.move, 10 * Time.deltaTime);
                animator.SetTrigger("Walk");
                animator.ResetTrigger("Idle");
            }
            else
            {
                animator.SetTrigger("Idle");
                animator.ResetTrigger("Walk");
            }
        }
    }
    public void StopMovement()
    {
        navMeshAgent.ResetPath();
        navMeshAgent.enabled = false;
        this.enabled = false;
    }
    Vector3 ArrayLerp(Vector3[] v, float t/*, float tolerance*/)
    {
        if (t <= 0f) return v[0];
        if (t >= 1f) return v[v.Length - 1];
        //if (Mathf.Abs(Vector3.Distance(v[v.Length - 1], v[0])) < tolerance) return v[v.Length - 1];

        float len = 0f;
        for (int i = 1; i < v.Length; i++) len += Vector3.Distance(v[i], v[i - 1]);

        float dt = len * t;
        float d = 0f;
        for (int i = 1; i < v.Length; i++)
        {
            float distanceBetweenPoints = d + Vector3.Distance(v[i], v[i - 1]);
            if (dt < d + distanceBetweenPoints)
            {
                t = (dt - d) / distanceBetweenPoints; //get the percentage distance between v[i-1] and v[i]
                return Vector3.Lerp(v[i - 1], v[i], t);
            }
            d += distanceBetweenPoints;
        }
        return v[v.Length - 1];
    }


}

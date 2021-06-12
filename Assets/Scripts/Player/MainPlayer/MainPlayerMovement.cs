using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainPlayerMovement : MonoBehaviour
{
    [SerializeField] VariableJoystick variableJoystick;
    [HideInInspector] public Vector3 move;
    public float speed;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        move = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical).normalized;
        navMeshAgent.Move(speed * move * Time.fixedDeltaTime);
    }
    public void StopMovement()
    {
        navMeshAgent.ResetPath();
        navMeshAgent.enabled = false;
        this.enabled = false;
    }
}

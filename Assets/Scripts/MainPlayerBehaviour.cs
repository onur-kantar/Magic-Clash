using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainPlayerBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [HideInInspector] public Vector3 move;
    NavMeshAgent navMeshAgent;
    public List<TeammatePoint> teammatePoints;
    void Awake()
    {
        teammatePoints = new List<TeammatePoint>();
        int i = 2;
        for (int x = -i; x <= i; x++)
        {
            for (int z = -i; z <= i; z++)
            {
                TeammatePoint newTeammatePoint = new TeammatePoint();
                newTeammatePoint.Point = new Vector3(x, 0, z);
                newTeammatePoint.id = Mathf.Abs(x) > Mathf.Abs(z) ? Mathf.Abs(x) : Mathf.Abs(z);
                //GameObject go = Instantiate(Player, transform.position + newTeammatePoint.Point, Quaternion.identity);
                //go.transform.parent = transform;
                //ewTeammatePoint.teamMate = go;
                teammatePoints.Add(newTeammatePoint);
            }
        }
        teammatePoints.Sort(delegate (TeammatePoint x, TeammatePoint y)
        {
            return x.id.CompareTo(y.id);
        });
    }
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        navMeshAgent.Move(speed * move * Time.fixedDeltaTime);
    }
}

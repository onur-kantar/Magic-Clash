using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Material material;
    Vector3 target;
    NavMeshAgent navMeshAgent;
    GameObject mainPlayer;
    [HideInInspector] public Animator animator;
    MainPlayerBehaviour mainPlayerBehaviour;
    Vector3 yEqualZero;

    // silah ile ilgili değişkenler
    [SerializeField] float overlapRadius;
    Vector3 nearestEnemy;
    public StaffScriptableObject staffScriptableObject;
    [SerializeField] Transform staffHand;
    Transform shootPoint;
    GameObject staff;
    float coolDown;
    float timeStamp;
    void Start()
    {
        mainPlayer = GameObject.Find("MainPlayer");
        JoinTeam();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        mainPlayerBehaviour = mainPlayer.GetComponent<MainPlayerBehaviour>();

        // Silah eline alma
        staff = Instantiate(staffScriptableObject.gameObject);
        
        staff.transform.SetParent(staffHand);
        staff.transform.localPosition = Vector3.zero;
        staff.transform.localRotation = Quaternion.Euler(Vector3.zero);
        shootPoint = staff.transform.GetChild(0);
        coolDown = staffScriptableObject.coolDown;
    }
    void Update()
    {
        Move();
        Shoot();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Teammate"))
        {
            other.gameObject.GetComponent<PlayerBehaviour>().enabled = true;
            other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
            other.gameObject.layer = LayerMask.NameToLayer("IgnoreEachOther");
            other.gameObject.tag = "Player";
            other.transform.parent = transform.parent;
        }
    }
    void Move()
    {
        yEqualZero = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        if (Mathf.Abs(Vector3.Distance(yEqualZero, target)) > 0.1f)
        {
            navMeshAgent.SetDestination(mainPlayer.transform.position + target);
            if (navMeshAgent.hasPath)
            {
                transform.position = ArrayLerp(navMeshAgent.path.corners, 0.05f/*, 0.1f*/);
                WalkAnimation();
                transform.forward = (target - yEqualZero).normalized;
            }
            else if (mainPlayerBehaviour.move != Vector3.zero)
            {
                WalkAnimation();
            }
            else
            {
                IdleAnimation();
            }
        }
        else
        {
            if (navMeshAgent.hasPath)
            {
                navMeshAgent.ResetPath();
            }
            if (mainPlayerBehaviour.move != Vector3.zero)
            {
                transform.forward = Vector3.Lerp(transform.forward, mainPlayerBehaviour.move, 10 * Time.deltaTime);
                WalkAnimation();
            }
            else
            {
                IdleAnimation();
            }
        }
        //for (int i = 0; i < navMeshAgent.path.corners.Length - 1; i++)
        //    Debug.DrawLine(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1], Color.red);
    }
    int teamId=0;
    void JoinTeam()
    {
        List<TeammatePoint> teammatePoints = mainPlayer.GetComponent<MainPlayerBehaviour>().teammatePoints;
        foreach (TeammatePoint teammatePoint in teammatePoints)
        {
            if (!teammatePoint.isFill)
            {
                teammatePoint.isFill = true;
                target = teammatePoint.Point;
                break;
            }
            teamId++;
        }
    }
    public void LeaveTeam() // TODO: public olmasa
    {
        mainPlayer.GetComponent<MainPlayerBehaviour>().teammatePoints[teamId].isFill = false;
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
    public void OnDeath()
    {
        Destroy(gameObject);
    }

    bool hasEnemy;
    void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius);
        float minimumDistance = Mathf.Infinity;
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Enemy")
            {
                float distance = Vector3.Distance(transform.position, collider.bounds.center);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = collider.bounds.center;
                    if (!hasEnemy)
                    {
                        hasEnemy = true;
                    }
                }
            }
        }
    }
    void Shoot()
    {
        FindClosestEnemy();
        if (hasEnemy)
        {
            if (timeStamp <= Time.time)
            {
                timeStamp = Time.time + coolDown;
                //Debug.LogError(nearestEnemy);
                shootPoint.LookAt(nearestEnemy);
                //Instantiate(staffScriptableObject.magicBall, shootPoint.position, shootPoint.rotation);
            }
            hasEnemy = false;
            //Debug.DrawLine(shootPoint.position, (nearestEnemy.position - shootPoint.position).normalized);
            //Debug.LogError((nearestEnemy.position - shootPoint.position).normalized);
        }
    }
}

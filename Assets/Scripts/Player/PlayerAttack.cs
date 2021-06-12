using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float overlapRadius;
    [SerializeField] Transform staffHand;

    public StaffScriptableObject staffScriptableObject;

    bool hasEnemy;
    float timeStamp;
    float coolDown;
    Transform shootPoint;
    GameObject staff;
    Vector3 nearestEnemy;

    void Start()
    {
        staff = Instantiate(staffScriptableObject.gameObject);
        staff.transform.SetParent(staffHand);
        staff.transform.localPosition = Vector3.zero;
        staff.transform.localRotation = Quaternion.Euler(Vector3.zero);

        shootPoint = staff.transform.GetChild(0);
        coolDown = staffScriptableObject.coolDown;
    }

    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        FindClosestEnemy();
        if (hasEnemy)
        {
            if (timeStamp <= Time.time)
            {
                timeStamp = Time.time + coolDown + Random.Range(-0.1f, 0.1f);
                //Debug.LogError(n
                //earestEnemy);
                shootPoint.LookAt(nearestEnemy);
                Instantiate(staffScriptableObject.magicBall, shootPoint.position, shootPoint.rotation);
            }
            hasEnemy = false;
            //Debug.DrawLine(shootPoint.position, (nearestEnemy.position - shootPoint.position).normalized);
            //Debug.LogError((nearestEnemy.position - shootPoint.position).normalized);
        }
    }
    void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius);
        float minimumDistance = Mathf.Infinity;
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Enemy" || collider.tag == "Boss")
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
}

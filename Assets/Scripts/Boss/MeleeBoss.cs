using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoss : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] int deathToll;

    public void Attack() 
    {
        Collider[] hitColliders = Physics.OverlapBox(attackPoint.position, attackPoint.localScale / 2, Quaternion.Euler(90,0,0));
        int i = deathToll;

        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = attackPoint.position;
        //cube.transform.localScale = attackPoint.localScale;
        //cube.transform.rotation = Quaternion.Euler(90, 0, 0);
        //Destroy(cube,2);

        foreach (Collider collider in hitColliders)
        {
            Debug.Log(i);
            if (i > 0)
            {
                if (collider.tag == "Player")
                {
                    collider.GetComponent<PlayerHealth>().OnHit("Explosion");
                    i--;
                }
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(attackPoint.position, attackPoint.localScale);
    //}
}

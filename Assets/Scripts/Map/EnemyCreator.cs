using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] Transform creationPoint;
    [SerializeField] GameObject enemy;
    [SerializeField] float coolDown, count;
    [HideInInspector] public EnemyController ownerEnemyController;
    public int totalCount;
    float timeStamp, currentCount;
    private void Update()
    {
        if (currentCount < totalCount)
        {
            if (timeStamp <= Time.time)
            {
                timeStamp = Time.time + coolDown + Random.Range(-0.1f, 0.1f);
                for (int i = 0; i < count; i++)
                {
                    Vector3 vector3 = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
                    GameObject go = Instantiate(enemy, creationPoint.position + vector3, creationPoint.rotation);
                    go.GetComponent<EnemyHealth>().ownerEnemyCreator = this;
                    go.transform.parent = gameObject.transform;
                    currentCount++;
                }
            }
        }
    }
}

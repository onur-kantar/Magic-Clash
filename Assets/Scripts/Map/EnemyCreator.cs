using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{

    [SerializeField] Transform creationPoint;
    [SerializeField] GameObject enemy;
    [SerializeField] float cooldown, randomCooldownRange, count, randomDistance;
    [HideInInspector] public EnemyController ownerEnemyController;
    public int totalEnemyCount;
    float timeStamp, currentEnemyCount;

    private void Update()
    {
        if (currentEnemyCount < totalEnemyCount)
        {
            if (timeStamp <= Time.time)
            {
                timeStamp = Time.time + cooldown + Random.Range(-randomCooldownRange, randomCooldownRange);
                for (int i = 0; i < count; i++)
                {
                    Vector3 distance = new Vector3(Random.Range(-randomDistance, randomDistance), 0, Random.Range(-randomDistance, randomDistance));
                    GameObject go = Instantiate(enemy, creationPoint.position + distance, creationPoint.rotation);
                    go.GetComponent<EnemyHealth>().ownerEnemyCreator = this;
                    go.transform.parent = gameObject.transform;
                    currentEnemyCount++;
                }
            }
        }
    }
}

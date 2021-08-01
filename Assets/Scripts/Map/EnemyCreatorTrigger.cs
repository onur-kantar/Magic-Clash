using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatorTrigger : MonoBehaviour
{
    EnemyCreator[] enemyCreators;
    private void Start()
    {
        enemyCreators = GetComponentsInChildren<EnemyCreator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (EnemyCreator enemyCreator in enemyCreators)
            {
                enemyCreator.enabled = true;
            }
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}

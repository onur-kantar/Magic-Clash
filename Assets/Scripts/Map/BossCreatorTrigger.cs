using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreatorTrigger : MonoBehaviour
{
    EnemyCreator[] enemyCreators;
    [SerializeField] Animation bossDoor;
    private void Start()
    {
        enemyCreators = GetComponentsInChildren<EnemyCreator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Debug.Log("boss bölümüne geçildi");
            bossDoor.Play("CloseDoor");
            foreach (EnemyCreator enemyCreator in enemyCreators)
            {
                enemyCreator.enabled = true;
            }
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}

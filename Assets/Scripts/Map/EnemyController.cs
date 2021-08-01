using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] bool hasBoss;

    [SerializeField] InGameManager gameManager;
    [SerializeField] EnemyCreator[] enemyCreators;
    [SerializeField] Animation bossDoor;
    int totalEnemy;

    void Start()
    {
        //bossCreators = GetComponentsInChildren<BossCreator>();
        //enemyCreators = GetComponentsInChildren<EnemyCreator>();
        foreach (EnemyCreator enemyCreator in enemyCreators)
        {
            enemyCreator.ownerEnemyController = this;
            totalEnemy += enemyCreator.totalEnemyCount;
        }
    }
    public void StopEnemyCreation()
    {
        foreach (EnemyCreator enemyCreator in enemyCreators)
        {
            enemyCreator.enabled = false;
        }
    }

    public void ReduceEnemyCount()
    {
        totalEnemy--;
        if (totalEnemy < 1)
        {
            if (hasBoss)
            {
                Debug.Log("1. bölüm bitti");
                // boss kýsmý çalýþtýrýlýr
                bossDoor.Play("OpenDoor");

            }
            else
            {
                Debug.Log("bölüm bitti");
                gameManager.GameOver(InGameManager.GameOverType.Win);
            }
        }
    }
    public GameObject [] GetEnemys()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //for (int first = 0; first < transform.childCount; first++)
        //{
        //    Transform child = transform.GetChild(first);
        //    for (int second = 0; second < child.childCount; second++)
        //    {
        //        Transform grandson = child.GetChild(second);
        //        if (grandson.tag == "Enemy")
        //        {
        //            enemys.Add(grandson.gameObject);
        //        }
        //    }
        //}
        return enemys;
    }
}

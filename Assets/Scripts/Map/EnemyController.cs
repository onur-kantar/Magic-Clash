using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] InGameManager gameManager;
    List<GameObject> enemys;
    int totalEnemy;
    EnemyCreator[] enemyCreators;
    void Start()
    {
        enemys = new List<GameObject>();
        enemyCreators = GetComponentsInChildren<EnemyCreator>();
        foreach (EnemyCreator enemyCreator in enemyCreators)
        {
            enemyCreator.ownerEnemyController = this;
            totalEnemy += enemyCreator.totalCount;
        }
    }
    public void StartCreation()
    {
        foreach (EnemyCreator enemyCreator in enemyCreators)
        {
            enemyCreator.enabled = true;
        }
    }
    public void StopCreation()
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
            gameManager.GameOver(InGameManager.GameOverType.Win);
        }
    }
    public List<GameObject> GetEnemys()
    {
        for (int first = 0; first < transform.childCount; first++)
        {
            Transform child = transform.GetChild(first);
            for (int second = 0; second < child.childCount; second++)
            {
                Transform grandson = child.GetChild(second);
                if (grandson.tag == "Enemy")
                {
                    enemys.Add(grandson.gameObject);
                }
            }
        }
        return enemys;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InGameManager gameManager;

    int totalPlayer;
    private void Start()
    {
    }
    public void IncreasePlayer()
    {
        totalPlayer++;
    }
    public void DecreasePlayer()
    {
        totalPlayer--;
        if (totalPlayer < 1)
        {
            gameManager.GameOver(InGameManager.GameOverType.Lose);
        }
    }
    public GameObject [] GetPlayers()
    {
        GameObject [] players = GameObject.FindGameObjectsWithTag("Player");

        //foreach (Transform child in transform)
        //    players.Add(child.gameObject);
        return players;
    }
}

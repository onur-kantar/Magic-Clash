using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InGameManager gameManager;
    List<GameObject> players;

    int totalPlayer;
    private void Start()
    {
        players = new List<GameObject>();
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
    public List<GameObject> GetPlayers()
    {
        foreach (Transform child in transform)
            players.Add(child.gameObject);
        return players;
    }
}

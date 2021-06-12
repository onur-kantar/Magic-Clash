using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject deathPS;
    GameObject mainPlayer;
    PlayerJoinTeam playerJoinTeam;
    Animator animator;

    private void Start()
    {
        mainPlayer = GameObject.Find("MainPlayer");
        playerJoinTeam = GetComponent<PlayerJoinTeam>();
        animator = GetComponent<Animator>();
    }
    public void OnHit(string deathStyle)
    {
        if (deathStyle == "Death") //TODO: --kötü bir yöntem
        {
            GetComponent<CapsuleCollider>().enabled = false;
            mainPlayer.GetComponent<CreateTeamPoints>().teammatePoints[playerJoinTeam.teamId].isFill = false;
            GetComponent<PlayerHealth>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerJoinTeam>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.parent = null;
            animator.SetBool("Death", true);
        }
        else if (deathStyle == "Explosion")
        {
            Instantiate(deathPS, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        mainPlayer.GetComponent<PlayerController>().DecreasePlayer();

    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
}

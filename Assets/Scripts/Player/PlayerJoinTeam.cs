using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinTeam : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] GameObject teammatePS;

    [HideInInspector] public Vector3 target;
    [HideInInspector] public int teamId = 0;
    
    GameObject mainPlayer;

    void Start()
    {
        mainPlayer = GameObject.Find("MainPlayer");
        JoinTeam();
    }

    void JoinTeam()
    {
        List<TeammatePoint> teammatePoints = mainPlayer.GetComponent<CreateTeamPoints>().teammatePoints;
        foreach (TeammatePoint teammatePoint in teammatePoints)
        {
            if (!teammatePoint.isFill)
            {
                teammatePoint.isFill = true;
                target = teammatePoint.Point;
                mainPlayer.GetComponent<PlayerController>().IncreasePlayer();
                Destroy(teammatePS);
                break;
            }
            teamId++;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Teammate"))
        {
            other.gameObject.GetComponent<PlayerJoinTeam>().enabled = true;
            other.gameObject.GetComponent<PlayerMovement>().enabled = true;
            other.gameObject.GetComponent<PlayerAttack>().enabled = true;
            other.gameObject.GetComponent<PlayerHealth>().enabled = true;
            other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
            other.gameObject.layer = LayerMask.NameToLayer("Player");
            other.gameObject.tag = "Player";
            other.transform.parent = transform.parent;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }
    void Update()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    } 
}

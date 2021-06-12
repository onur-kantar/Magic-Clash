using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    Animation anim;
    Vector3 offset;
    bool isPlaying;
    void Awake()
    {
        anim = GetComponent<Animation>();
        isPlaying = true;
        offset = transform.position - target.position;
    }
    void Update()
    {
        if (isPlaying)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    } 
    public void ZoomIn()
    {
        isPlaying = false;
        anim.Play("ZoomIn");
    }
    public void ZoomOut()
    {
        isPlaying = true;
        anim.Play("ZoomOut");
    }
}

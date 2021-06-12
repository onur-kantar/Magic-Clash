using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "Staff", menuName = "ScriptableObjects/Staff")]
public class StaffScriptableObject : ScriptableObject
{
    public float coolDown;
    public GameObject gameObject;
    public GameObject magicBall;
}
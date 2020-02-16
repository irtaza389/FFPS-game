using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Gun",menuName ="Gun")]
public class gun : ScriptableObject
{

    public string name;
    public GameObject prefab;
    public float FireRAte;
    public float aimspeed;
    public float bloom;
    public float recoil;
    public float kickback;
    public int damage;

}

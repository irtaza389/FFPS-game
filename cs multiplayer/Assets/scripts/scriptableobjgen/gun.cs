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
    public int ammo;
    public int magsize;
    public float ReloadTime;


    private int clip; // current clip
    private int stach; // current ammo


   public void Initalize()
    {
        stach = ammo;
        clip = magsize;
    }

    public bool FireBullet()
    {
        if (clip > 0)
        {
            clip -= 1;
            return true;
            Debug.Log("firing");
        }
        else return false;

    }
    public void Reload()
    {
        stach += clip;
        clip = Mathf.Min(magsize, stach);
        stach -= clip;
        Debug.Log("reloading");
    }
    public int GetStach() { return stach; }
    public int GetClip()
    {
        return clip;
    }

}

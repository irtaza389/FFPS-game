using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon: MonoBehaviourPunCallbacks 
{

    public gun[] loadout;
    private int currentIndex;
    public Transform weaponParent;
    private GameObject currentWeapon;
    public GameObject bulletholePrefab;
    public LayerMask canbeShoot;
    private float coolDown;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (photonView.IsMine && Input.GetKeyDown(KeyCode.Alpha1))

        {
            photonView.RPC("Equip", RpcTarget.All, 0);
        }

            if (currentWeapon != null)
            {
                if (photonView.IsMine)
                {



                    if (Input.GetMouseButton(1))
                        Aim(true);

                    if (Input.GetMouseButton(0) && coolDown < 0)
                    {
                        photonView.RPC("shoot", RpcTarget.All);
                    }
                    //cooldown
                    if (coolDown > 0)
                    {
                        coolDown -= Time.deltaTime;
                    }
                }

                    //weapon position elasticity 
                    currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
                   
                }

            }
        
    
    [PunRPC]
    void Equip(int w_id)
    {

        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }
        currentIndex = w_id;
        GameObject t_newequip = Instantiate(loadout[w_id].prefab, weaponParent.localPosition, weaponParent.rotation, weaponParent) as GameObject;
        t_newequip.transform.localPosition = Vector3.zero;
        t_newequip.transform.localEulerAngles = Vector3.zero;
        t_newequip.GetComponent<sway>().IsMine = photonView.IsMine;
        currentWeapon = t_newequip;
    }


    void Aim( bool p_isAiming)
    {
        Transform t_anchor = currentWeapon.transform.Find("Anchor");
        Transform t_state_ads = currentWeapon.transform.Find("state/ADS");
        Transform t_state_hip = currentWeapon.transform.Find("state/hip");
        if (p_isAiming)
        {
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimspeed);
        }
        else
        {
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimspeed);
        }

    }


    [PunRPC]
    void shoot()
    {

        
        
            Transform t_spawn = transform.Find("Camera/playercam");
            //bloom
            Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000;
            t_bloom += UnityEngine.Random.Range(loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.up;
            t_bloom += UnityEngine.Random.Range(loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.right;
            t_bloom -= t_spawn.position;
            t_bloom.Normalize();


            RaycastHit t_hit = new RaycastHit();
            if (Physics.Raycast(t_spawn.position, t_spawn.forward, out t_hit, 1000f, canbeShoot))
            {

                GameObject t_newhole = Instantiate(bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;


                t_newhole.transform.LookAt(t_hit.point + t_hit.normal);
                Destroy(t_newhole, 5f);
                //cool down
                coolDown = loadout[currentIndex].FireRAte;
            if (photonView.IsMine)
            {// shoting over net
                if (t_hit.collider.gameObject.layer == 11)
                {
                    //RPC CAll for Damage Player
                    t_hit.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, loadout[currentIndex].damage);

                }
            }
            
        }

        //gunfx 
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickback;
       

    }
    [PunRPC]
    private void TakeDamage( int p_damage)
    {
        GetComponent<Player>().TakeDamage(p_damage);
    }
     

}


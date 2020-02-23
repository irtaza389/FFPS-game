using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{

    public string name = "clark";
    public float damage = 10f;
    public float range = 200f;
    public LayerMask player;
    [SerializeField]
    private Camera cam;
    public Transform arms;

    

    private void Start()
    {
        
        if (cam== null)
        {
            Debug.Log("PlayShoot: NO shoot cam");
            this.enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }

       

    }

    void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward,out hit, range,player))
        {
            Debug.Log("we Hit " + hit.collider.name);
        }

    }

}

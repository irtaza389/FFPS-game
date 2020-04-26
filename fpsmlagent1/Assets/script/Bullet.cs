using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float aliveTime;
    public float damage;
    public float moveSpeed = 10f;
    public GameObject bulletspawn;
    private void Start()
    {
        bulletspawn = GameObject.Find("spawnpoint");
        this.transform.rotation = bulletspawn.transform.rotation;
    }
    private void Update()
    {
        aliveTime -= 1 * Time.deltaTime;
        if (aliveTime <= 0)
        {
            Destroy(this.gameObject);
        }


        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
   
}

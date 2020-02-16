using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public string player_prfab;
    public Transform[] spawn_point ; 

    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        Transform t_spawn = spawn_point[Random.Range(0, spawn_point.Length)];
        PhotonNetwork.Instantiate(player_prfab, t_spawn.position, t_spawn.rotation);
    }
}

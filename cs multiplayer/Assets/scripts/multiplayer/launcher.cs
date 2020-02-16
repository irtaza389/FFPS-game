using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class launcher : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connect to servers !!");
        OnJoin();
        base.OnConnectedToMaster();
    }

    public override void OnJoinedRoom()
    {
        StartGame();
       base.OnJoinedRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Create();
        base.OnJoinRandomFailed(returnCode, message);
    }
    public void Connect()
    {

        Debug.Log("trying to connect !!");
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }
    public void OnJoin()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
    public void Create()
    {
        PhotonNetwork.CreateRoom("");
    }
}

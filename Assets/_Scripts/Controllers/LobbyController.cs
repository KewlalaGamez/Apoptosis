﻿using UnityEngine;
using System.Collections;

public class LobbyController : Photon.PunBehaviour
{

    public UnityEngine.UI.Text ConnectionStatusText;
    public GameObject RandomJoinButton;

    // Use this for initialization
    void Start()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings("0.0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ConnectionStatusText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public void JoinRandomRoom()
    {
        base.OnJoinedLobby();
        if (PhotonNetwork.countOfRooms == 0)
        {
            PhotonNetwork.CreateRoom("Kewlala-SGP", new RoomOptions() { maxPlayers = 2 }, null);
        }
        else
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        RandomJoinButton.SetActive(true);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.room.playerCount >= 2)
        {
            PhotonNetwork.room.open = false;
        }
        PhotonNetwork.LoadLevel("Multiplayer_Level");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);
        PhotonNetwork.CreateRoom("Kewlala-SGP", new RoomOptions() { maxPlayers = 2 }, null);
    }
}

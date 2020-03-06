﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit.Forms;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] 
    private GameObject quickStartButton;

    [SerializeField] private GameObject quickCancelButton;

    [SerializeField] private int RoomSize = 2;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
    }

    public void QuickStart()
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Quick Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Victime");
        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, Int32.MaxValue);
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = (byte) RoomSize};
        PhotonNetwork.CreateRoom("Room " + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }

    public void QuickCancel()
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

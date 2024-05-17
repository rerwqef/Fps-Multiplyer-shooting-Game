using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class CreateandJoinRooms :  MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public GameObject currentPanel;
    public GameObject playerListPanel;
    private int numPlayers = 2;
    public TeamManger manager; // Reference to TeamManager script
    public GameObject st;

    void Start()
    {
        manager = TeamManger.Instance;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        st.SetActive(false);
    }

    public void CreateRoom()
    {
        string roomName = createInput.text;
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = numPlayers });
            // Add the room creator to a team
        
        }
        else
        {
            Debug.LogError("Cannot create room: Not connected to the Master Server.");
        }
    }
    public override void OnCreatedRoom()
    {
        manager.caller(PhotonNetwork.LocalPlayer);
        st.SetActive(true);
    }
    public void JoinRoom()
    {
        string roomName = joinInput.text;
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        currentPanel.SetActive(false);
        playerListPanel.SetActive(true);
    }
    public void LoadGameScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (manager != null)
        {
            manager.caller(newPlayer);
        }
        else
        {
            Debug.LogError("TeamManager reference is not set.");
        }
    }

    public void AdjustNumPlayers(int value)
    {
        numPlayers = (value * 2) + 2;
    }
}
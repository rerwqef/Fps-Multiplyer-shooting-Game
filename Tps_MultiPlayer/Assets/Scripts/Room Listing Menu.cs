using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
public static RoomListingMenu instance;
[SerializeField] private Transform content;
[SerializeField] private GameObject roomPrefab;
private List<RoomInfo> cachedRoomlist = new List<RoomInfo>();
public bool isFirstUpdate = false; // Flag to indicate if the room list has been updated at least once

private void Awake()
{
    instance = this;
}
    public void Start()
    {
       // PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
{
        Debug.Log("onRoomLiistUpdate called");
    cachedRoomlist.Clear();

    foreach (var room in roomList)
    {
        cachedRoomlist.Add(room);
        }

        // Set isFirstUpdate to true after the room list is updated for the first time
          

        // Update the UI with the new room list
        UpdateRoomList();
    }

    void UpdateRoomList()
    {
        // Clear the existing UI content
        foreach (Transform trans in content)
        {
            Destroy(trans.gameObject);
        }

        // Populate the UI with the updated room list
        foreach (var room in cachedRoomlist)
        {
            GameObject newRoom = Instantiate(roomPrefab, content);
            newRoom.GetComponent<Room>().SetRoomInfo(room);
        }
    }

public void JoinRoom(RoomInfo room)
{
    PhotonNetwork.JoinRoom(room.Name);
}
}

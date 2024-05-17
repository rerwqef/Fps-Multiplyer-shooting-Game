using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
public class Room : MonoBehaviour
{
public TextMeshProUGUI roomName;
public TextMeshProUGUI numberOfPlayers;
public RoomInfo info;
    
public void SetRoomInfo(RoomInfo roomInfo)
{
    info = roomInfo;
    numberOfPlayers.text = roomInfo.PlayerCount + "/"+roomInfo.MaxPlayers;
    roomName.text=roomInfo.Name;
}
public void onclick()
{
    RoomListingMenu.instance.JoinRoom(info);
}
}

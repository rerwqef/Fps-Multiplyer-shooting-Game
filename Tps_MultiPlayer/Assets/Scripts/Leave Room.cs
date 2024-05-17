using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
public class LeaveRoom : MonoBehaviourPunCallbacks
{

    public void ExitRoom()
    {
        Debug.Log("Leaving room...");
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Is Connected: " + PhotonNetwork.IsConnected);
            Debug.Log("In Room: " + PhotonNetwork.InRoom);
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            Debug.LogWarning("Not connected to Photon.");
        }
    }



    public override void OnLeftRoom()
    {
        Debug.Log("Room left. Loading next scene...");
        PhotonNetwork.JoinLobby();
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

}


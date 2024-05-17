using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_InputField inputField;
    public GameObject apnel;
   
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    // Update is called once per frame
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void StartTheGame()
    {
        PhotonNetwork.LocalPlayer.NickName = inputField.text;
        apnel.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }
}

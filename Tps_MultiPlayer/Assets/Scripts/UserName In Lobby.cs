using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
public class UserNameInLobby : MonoBehaviourPun
{
    // Start is called before the first frame update
  public  GameObject UserNameObj;
    TextMeshProUGUI userName;
    void Start()
    {
        userName = GetComponent<TextMeshProUGUI>();
        if (photonView.IsMine)
        {
          UserNameObj.SetActive(true);
            userName.text=PhotonNetwork.LocalPlayer.NickName;
        }
    }

}

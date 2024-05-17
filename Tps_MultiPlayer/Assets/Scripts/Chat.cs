using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Chat : MonoBehaviour
{
    public InputField inputField;
    public GameObject meesge;
    public GameObject content;

    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All,PhotonNetwork.LocalPlayer.NickName+" : "+inputField.text);
        inputField.text="";
    }
    [PunRPC]
    public void GetMessage(string msg)
    {
      
    GameObject msgIn=Instantiate(meesge,Vector3.zero,Quaternion.identity,content.transform);
       msgIn.GetComponent<Message>().msgText.text = msg;
    }
}

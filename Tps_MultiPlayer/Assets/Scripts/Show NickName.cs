using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class ShowNickName : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
   
    void Start()
    {
     gameObject.GetComponent<TextMeshProUGUI>().text = photonView.Owner.NickName;
    }

    public void Update()
    {
       // transform.LookAt(Camera.main.transform);
    }
}

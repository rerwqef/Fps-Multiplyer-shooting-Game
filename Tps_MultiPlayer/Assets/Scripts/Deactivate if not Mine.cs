using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DeactivateifnotMine : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
         
        }
    }

   
}

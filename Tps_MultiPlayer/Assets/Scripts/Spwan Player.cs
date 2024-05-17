using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



public class SpwanPlayer : MonoBehaviourPun
{
    public static SpwanPlayer Instance;
    public GameObject TeamAPrefab;
    public GameObject TeamBPrefab;

    public Transform[] spawnPoint;
    public TeamManger teamManger;
public void Awake()
{
    Instance = this;
}
private void Start()
{

        teamManger=TeamManger.Instance;
       
        Spwanplayer(PhotonNetwork.LocalPlayer.NickName);
}
    public void Spwanplayer(string localPlayerNickname)
    {

            Debug.Log(PhotonNetwork.LocalPlayer.NickName);
            // Get the local player's nickname
         
       
            // Check if the local player is in Team A
            if (teamManger.teamAPlayers.Exists(player => player.NickName == localPlayerNickname))
            {
                // Instantiate Team A prefab
                GameObject _player = PhotonNetwork.Instantiate(TeamAPrefab.name, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                _player.GetComponent<PlayerSetup>().IslocalPlayer();
           
                _player.GetComponent<PlayerHealth>().isLocalPlayer = true;
            _player.GetComponent<PlayerHealth>().PLayerHelthEnable();
        }
        else if (teamManger.teamBPlayers.Exists(player => player.NickName == localPlayerNickname))
        {
                // Instantiate Team B prefab
                GameObject _player = PhotonNetwork.Instantiate(TeamBPrefab.name, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                _player.GetComponent<PlayerSetup>().IslocalPlayer();
                _player.GetComponent<PlayerHealth>().isLocalPlayer = true;
            _player.GetComponent<PlayerHealth>().PLayerHelthEnable();
        }
        
          
        

    }
}



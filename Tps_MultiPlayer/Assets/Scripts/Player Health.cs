using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviourPunCallbacks
{ 

public int health;
public Slider playerHealthSlider;
public bool isLocalPlayer;

public TeamManger teamManger;

public TeamScore teamScore;
private void Awake()
{
    teamScore = FindAnyObjectByType<TeamScore>();
    teamManger = TeamManger.Instance;
        
}
public void PLayerHelthEnable()
{
if (isLocalPlayer)
{
    playerHealthSlider.gameObject.SetActive(true);
         
}
  
health = 100; // Assuming starting health is 100
playerHealthSlider.value = health;
}

[PunRPC]
public void TakeDamage(int _damage)
{
health -= _damage;
playerHealthSlider.value = health;
        if (health <= 0)
        {       
            Die();
    
        }
}

public void Die()
{



        /*
                    if (teamManger.teamAPlayers.Contains(photonView.Owner))
                    {
                        teamScore.IncreaseTeamBScore();
                    }
                    else if (teamManger.teamBPlayers.Contains(photonView.Owner))
                    {
                        teamScore.IncreaseTeamAScore();
                    }
                string localPlayerNickname = PhotonNetwork.LocalPlayer.NickName;
                //   Destroy(gameObject);
                PhotonNetwork.Destroy(gameObject);

                if (isLocalPlayer)
                {

                    SpwanPlayer.Instance.Spwanplayer(localPlayerNickname);
                }*/



        if (photonView.IsMine) // Check ownership
        {
            if (teamManger.teamAPlayers.Contains(photonView.Owner))
            {
                teamScore.IncreaseTeamBScore();
            }
            else if (teamManger.teamBPlayers.Contains(photonView.Owner))
            {
                teamScore.IncreaseTeamAScore();
            }

            string localPlayerNickname = PhotonNetwork.LocalPlayer.NickName;
            PhotonNetwork.Destroy(gameObject); // Destroy the player object

            if (isLocalPlayer)
            {
                // Respawn the local player
                SpwanPlayer.Instance.Spwanplayer(localPlayerNickname);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Helth"))
        {
            health = 100;
            Destroy(collision.gameObject);
            playerHealthSlider.value = health;
        }
    }

}

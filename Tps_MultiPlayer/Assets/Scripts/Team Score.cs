using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class TeamScore : MonoBehaviourPunCallbacks
{
    public Text teamAScoreText;
    public Text teamBScoreText;

    private int teamAScore = 0;
    private int teamBScore = 0;
    public GameObject WInPannel;
    public TextMeshProUGUI winedTeamText;
    public void Start()
    {
        UpdateAndSyncScore();
    }
    public void IncreaseTeamAScore()
    {
        teamAScore++;
        UpdateAndSyncScore();
    }

    public void IncreaseTeamBScore()
    {
        teamBScore++;
        UpdateAndSyncScore();
    }

    void UpdateAndSyncScore()
    {
        // Sync the score across all clients
        photonView.RPC("SyncScore", RpcTarget.AllBuffered, teamAScore, teamBScore);
    }

    [PunRPC]
    void SyncScore(int scoreA, int scoreB)
    {
        // Update the local score variables
        teamAScore = scoreA;
        teamBScore = scoreB;

     
        // Update the UI text elements, if they exist
        if (teamAScoreText != null)
        {
            teamAScoreText.text =  teamAScore.ToString();
        }

        if (teamBScoreText != null)
        {
            teamBScoreText.text = teamBScore.ToString();
        }
        if (teamAScore == 5)
        {

            photonView.RPC("ShowWin", RpcTarget.AllBuffered, "TEAMA");
        }
        else if (teamBScore == 5)
        {

            photonView.RPC("ShowWin", RpcTarget.AllBuffered, "TEAMB");
        }
    }

    [PunRPC]
    void ShowWin(string WinedTeam)
    {
        WInPannel.SetActive(true);
        winedTeamText.text = "Winner TEAM iS -" + WinedTeam;
    }
    
}
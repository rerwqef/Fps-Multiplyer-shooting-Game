using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;

public class TeamManger : MonoBehaviourPun
{
    public enum Team
    {
        TeamA,
        TeamB
    }

    public Transform teamAText;
    public Transform teamBText;
    public List<Player> teamAPlayers = new List<Player>();
    public List<Player> teamBPlayers = new List<Player>();
    public GameObject PLayerListPLayer;

    public static TeamManger Instance;

    private void Awake()
    {
// Instance = this;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void caller(Player newPlayer)
    {
        if (!IsPlayerInTeam(newPlayer))
        {
            Team team = GetRandomTeam();
     photonView.RPC("SetTeam", RpcTarget.AllBuffered, team, newPlayer.ActorNumber);
        }
    }

    public static Team GetRandomTeam()
    {
        RoomInfo currentRoom = PhotonNetwork.CurrentRoom;
        if (currentRoom != null)
        {
            int maxPlayers = currentRoom.MaxPlayers;
            int playerCount = currentRoom.PlayerCount;

            // Determine the team based on the current player count
            if (playerCount < maxPlayers / 2)
            {
                return Team.TeamA;
            }
            else
            {
                return Team.TeamB;
            }
        }
        else
        {
            Debug.LogError("No room available.");
            return Team.TeamA;
        }
    }

    [PunRPC]
    public void SetTeam(Team team, int actorNumber)
    {
        Player player = PhotonNetwork.CurrentRoom.GetPlayer(actorNumber);
        if (player == null)
        {
            Debug.LogWarning("Player not found for actor number: " + actorNumber);
            return;
        }

        // Check if the player is already in a team
        if (IsPlayerInTeam(player))
        {
            return;
        }

        // Check if adding the player will exceed the max player count for the team
        if (team == Team.TeamA && teamAPlayers.Count >= PhotonNetwork.CurrentRoom.MaxPlayers / 2)
        {
            team = Team.TeamB;
        }
        else if (team == Team.TeamB && teamBPlayers.Count >= PhotonNetwork.CurrentRoom.MaxPlayers / 2)
        {
            team = Team.TeamA;
        }

        // Update the team list and UI text
        if (team == Team.TeamA)
        {
            teamAPlayers.Add(player);
            UpdateUITeamA();
        }
        else
        {
            teamBPlayers.Add(player);
            UpdateUITeamB();
        }
    }

    bool IsPlayerInTeam(Player player)
    {
        return teamAPlayers.Contains(player) || teamBPlayers.Contains(player);
    }

    void UpdateUITeamA()
    {
        foreach (Player player in teamAPlayers)
        {
            GameObject PlA = Instantiate(PLayerListPLayer, teamAText);
            PlA.GetComponent<PlayerListPlayer>().playerName.text = player.NickName;
        }
    }

    void UpdateUITeamB()
    {
        foreach (Player player in teamBPlayers)
        {
            GameObject PlB = Instantiate(PLayerListPLayer, teamBText);
            PlB.GetComponent<PlayerListPlayer>().playerName.text = player.NickName;
        }
    }
}